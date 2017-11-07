using System;
using System.Linq;
using Orders;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Queries;

namespace HelloWorldRavenDB4
{
    //Install-Package RavenDB.Client -Source https://www.myget.org/F/ravendb/api/v3/index.json
    class Program
    {
        static void Main(string[] args)
        {
            //assuming Northwind database created on http://localhost:8080 local server
            using (var store = new DocumentStore
            {
                //if we are connecting to cluster, then Urls should contain
                //addresses of the cluster nodes
                Urls = new[] {"http://localhost:8080"}, 
                Database = "Northwind" //default database to connect
            })
            {
                //do additional configuration here
                store.Initialize(); //initialize the document store

                //since we specified 'Northwind' as default database, we do not need to specify database name
                using (var session = store.OpenSession())
                {
                    //simple linq query
                    foreach (var order in session.Query<Order>().Skip(3).Take(4))
                    {
                        Console.WriteLine(order.Id + "," + order.Company);
                    }

                    Console.WriteLine();

                    //more complex linq query
                    var complexLinqQuery =
                        from o in session.Query<Order>()
                        let TotalSpentOnOrder =
                            (Func<Order, decimal?>) (ord =>
                                ord.Lines.Sum(l => l.PricePerUnit * l.Quantity - l.Discount))
                        select new
                        {
                            CompanyId = o.Company,
                            TotalMoneySpent = TotalSpentOnOrder(o)
                        };

                    foreach (var item in complexLinqQuery)
                    {
                        Console.WriteLine($@"Order Id:{item.CompanyId},  
                                                         Total Spent:{item.TotalMoneySpent}");
                    }

                    var simpleRqlQuery = session.Advanced.RawQuery<Order>("FROM Orders").Take(4).ToList();
                    foreach (var order in simpleRqlQuery)
                    {
                        Console.WriteLine(order.Id + "," + order.Company);
                    }

                    var transformerRqlQuery = session.Advanced.RawQuery<dynamic>(@"
                    DECLARE function companyAndTotalSumSpent(o)
                    {
                        var totalSumInLines = 0;
                        for(var i = 0; i < o.Lines.length; i++)
                        {
                            var l = o.Lines[i];
                            totalSumInLines += l.PricePerUnit * l.Quantity - l.Discount;
                        }
                        return { OrderedAt: o.OrderedAt, TotalSumSpent: totalSumInLines };
                    }
                    FROM Orders as o 
                    WHERE o.OrderedAt.Year >= 1998 and o.Lines.length > 0
                    SELECT companyAndTotalSumSpent(o)");

                    var results = transformerRqlQuery.Take(10).ToList();

                    foreach (var item in results)
                    {
                        Console.WriteLine($@"Ordered At:{item.OrderedAt}, Total: {item.TotalSumSpent}");
                    }

                    var complexRqlQuery = session.Advanced.RawQuery<dynamic>(@"
                    DECLARE function companyAndTotalSumSpent(o,c)
                    {
                        var totalSumInLines = 0;
                        for(var i = 0; i < o.Lines.length; i++)
                        {
                            var l = o.Lines[i];
                            totalSumInLines += l.PricePerUnit * l.Quantity - l.Discount;
                        }
                        return { OrderedAt: o.OrderedAt, Company : c.Name, TotalSumSpent: totalSumInLines };
                    }
                    FROM Orders as o 
                    WHERE o.OrderedAt.Year >= 1995 and o.Lines.length > 0
                    LOAD o.Company as c
                    SELECT companyAndTotalSumSpent(o,c)");

                    results = complexRqlQuery.Take(10).ToList();

                    foreach (var item in results)
                    {
                        Console.WriteLine(
                            $@"Ordered At:{item.OrderedAt}, Company:{item.Company}, Total: {item.TotalSumSpent}");
                    }

                    var veryComplexQueryWithTransformation = session.Advanced.RawQuery<dynamic>(@"
                        DECLARE function lineItemPrice(l) {
                            return l.PricePerUnit * 
                                   l.Quantity * 
                                   (1 - l.Discount);
                        }
                        FROM Orders AS o
                        SELECT {
                            TopProducts: o.Lines
                                .sort((a, b) => 
                                    lineItemPrice(b) - 
                                        lineItemPrice(a) )
                                .map( x => x.ProductName)
                                .slice(0,2),
                            Total: o.Lines.reduce((acc, l) => 
                                    acc + lineItemPrice(l),
                                    0)
                        }

                    ");

                    foreach (var item in veryComplexQueryWithTransformation)
                    {
                        Console.WriteLine(
                            $@"Top products:{item.TopProducts}, Total: {item.Total}");
                    }
                    var queryWithIncludes = from order in session.Query<Order>()
                            .Include(o => o.Company)
                        select order;

                    //RQL : FROM Orders INCLUDE Company
                    foreach (var item in queryWithIncludes.Take(4).ToList())
                    {
                        Console.WriteLine($@"Ordered At:{item.OrderedAt}, Company:{item.Company}");

                        //this loads from local cache, not from remote server
                        var company = session.Load<Company>(item.Company);

                        Console.WriteLine(company.Name);
                    }
                }

                var evilToysCompany = new Company
                {
                    Name = "Evil Toys Inc."
                };
                using (var session = store.OpenSession()) //open tx
                {
                    //add new document
                    session.Store(new Company
                    {
                        Contact = new Contact
                        {
                            Name  = "John",
                            Title = "Evil Mastermind"
                        },
                        Address = new Address
                        {
                            Country = "USA",
                            City = "New York",
                            Line1 = "Sesame str. 123/45"
                        },
                        Name = "Acme Inc."
                    });
               
                    
                    session.Store(evilToysCompany);
                    session.SaveChanges(); //commit tx
                }

                using (var session = store.OpenSession())
                {
                    var companyToDelete = session.Query<Company>().FirstOrDefault(x => x.Name == "Evil Toys Inc.");//
                    session.Delete(companyToDelete);
                    session.SaveChanges();
                }

                using (var session = store.OpenSession()) //open tx
                {
                    //simple patch
                    var companyToUpdate = session.Query<Company>().FirstOrDefault(x => x.Name == "Acme Inc.");
                    session.Advanced.Patch(companyToUpdate, company => company.Name, "Not Acme Inc.");

                    //collection patch, add item to embedded collection
                    var order = session.Load<Order>("orders/1");
                    session.Advanced.Patch(order,
                        o => o.Lines,
                        orderLines => orderLines.Add(new OrderLine
                        {
                            PricePerUnit = (decimal)3.5,
                            Quantity = 1234,
                            ProductName = "FooBar!"
                        }));

                    session.SaveChanges(); //commit tx
                }

                //simple RQL patch
                // ReSharper disable once NotAccessedVariable
                // ReSharper disable once UnusedVariable
                store.Operations.Send(new PatchByQueryOperation(
                        @"FROM Companies  as c
                          WHERE Id(c) = '" + evilToysCompany.Id + @"'
                          UPDATE { this.Name = 'Evil John Inc.' }"))
                    .WaitForCompletion();

                //bulk delete
                store.Operations
                     .Send(new DeleteByQueryOperation(new IndexQuery
                     {
                         Query = "FROM Companies as c WHERE c.Name = 'Evil John Inc.'"
                     }))
                     .WaitForCompletion();
            }
        }
    }
}
