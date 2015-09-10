using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class CompaniesAndCountry : AbstractIndexCreationTask<Company>
    {
        public class Result
        {
            public Company Company { get; set; }
            public string Address_Country { get; set; }
        }

        public CompaniesAndCountry()
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