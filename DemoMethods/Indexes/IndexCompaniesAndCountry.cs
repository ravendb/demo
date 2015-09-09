using System.Linq;
using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class IndexCompaniesAndCountry : AbstractIndexCreationTask<Company>
    {
        public class Result
        {
            public Company Company { get; set; }
            public string Address_Country { get; set; }
        }

        public IndexCompaniesAndCountry()
        {
            Map = companies =>
                from company in companies
                select new
                {
                    Company = company,
                    Address_Country = company.Address.Country
                };
        }
    }
}