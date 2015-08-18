using System.Linq;
using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class Index_CompaniesAndCountry : AbstractIndexCreationTask<Company>
    {
        public class Result
        {
            public Company Company { get; set; }
            public string Country { get; set; }
        }


        public Index_CompaniesAndCountry()
        {
            Map = companies => from company in companies
                               let a = company.Address
                               select new
                               {
                                   Company = company,
                                   Country = a.Country
                               };

            Index(x => x.Address, FieldIndexing.Analyzed);
            Store(x => x.Address, FieldStorage.Yes);
            TermVector(x => x.Address, FieldTermVector.WithPositionsAndOffsets);

        }
    }
}