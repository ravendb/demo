using System.Linq;
using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class IndexNameAndCountry : AbstractMultiMapIndexCreationTask<IndexNameAndCountry.Result>
    {
        public class Result
        {
            public string Name { get; set; }
            public string Country { get; set; }
        }


        public IndexNameAndCountry()
        {
            AddMap<Employee>(emploees => from e in emploees
                                         select new Result
                                         {
                                             Name = e.FirstName + " " + e.LastName,
                                             Country = e.Address.Country
                                         });



            AddMap<Company>(companies => from c in companies
                                         select new Result
                                         {
                                             Name = c.Name,
                                             Country = c.Address.Country
                                         });

            AddMap<Supplier>(suppliers => from s in suppliers
                                          select new Result
                                          {
                                              Name = s.Name,
                                              Country = s.Address.Country
                                          });


            Index(x => x.Country, FieldIndexing.Analyzed);

            Store(x => x.Name, FieldStorage.Yes);
            Store(x => x.Country, FieldStorage.Yes);
        }
    }
}
