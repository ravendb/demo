using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMethods.Indexes
{
    public class Index_CompaniesAndAddresses : AbstractIndexCreationTask<Company>
    {
        public class Result
        {
            public Company Company { get; set; }
            public Address Address { get; set; }
        }


        public Index_CompaniesAndAddresses()
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
