namespace ImportBeerDB
{
    //importing beerDB files (taken from https://openbeerdb.com/)
    class Program
    {
        static void Main(string[] args)
        {
            BeersDBInMemory.LoadFromCsv();
        }
    }
}
