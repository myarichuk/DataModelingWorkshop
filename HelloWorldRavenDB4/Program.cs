using System;
using System.Globalization;
using System.Linq;
using Orders;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;

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
                Urls = new[] {"http://localhost:8080"}, //what adresses to listen
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
                            OrderId = o.Id,
                            TotalMoneySpent = TotalSpentOnOrder(o)
                        };

                    foreach (var item in complexLinqQuery)
                    {
                        Console.WriteLine($@"Order Id:{item.OrderId},  
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

                    var queryWithIncludes = from order in session.Query<Order>()
                            .Include(o => o.Company)
                        select order;

                    //RQL : FROM Orders INCLUDE Company
                    foreach (var item in queryWithIncludes.Take(4))
                    {
                        Console.WriteLine($@"Ordered At:{item.OrderedAt}, Company:{item.Company}");

                        //this loads from local cache, not from remote server
                        var company = session.Load<Company>(item.Company);

                        Console.WriteLine(company.Name);
                    }
                }

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
                    session.SaveChanges(); //commit tx
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
                            ProductName = "Beer"
                        }));

                    session.SaveChanges(); //commit tx
                }

                //simple RQL patch
                var result = store.Operations.Send(new PatchByQueryOperation(
                        @"FROM Orders as o
                          WHERE o.Name = 'Not Acme Inc.'
                          UPDATE { this.Name = 'Evil John Inc.' }"))
                    .WaitForCompletion<BulkOperationResult>(TimeSpan.FromSeconds(15));

                //complex RQL patch
                result = store.Operations.Send(new PatchByQueryOperation(
                        @"  DECLARE function convertToLowercase(c){
                                return c.Name.toLowercase();
                            }
                            FROM index 'Orders/Totals' as i
                            WHERE i.Total > 10000
                            LOAd i.Company as c
                            UPDATE { 
                                i.LowerName = convertToLowercase(c);
                            }"))
                    .WaitForCompletion<BulkOperationResult>(TimeSpan.FromSeconds(15));
            }
        }
    }
}
