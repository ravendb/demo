using System.Linq;
using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class CompaniesAndAddresses : AbstractIndexCreationTask<Company>
    {
        public class Result
        {
            public Company Company { get; set; }
            public Address Address { get; set; }
        }


        public CompaniesAndAddresses()
        {
            Map = companies => from company in companies
                               select new
                               {
                                   Company = company,
                                   Address = new[]
                                       {
                                           company.Address.Line1,
                                           company.Address.Line2,
                                           company.Address.City,
                                           company.Address.Country
                                       }
                               };

            Index(x => x.Address, FieldIndexing.Analyzed);
            Store(x => x.Address, FieldStorage.Yes);
            TermVector(x => x.Address, FieldTermVector.WithPositionsAndOffsets);

        }
    }
}
