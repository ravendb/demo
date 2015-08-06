using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        public class Index_NameAndCountry : AbstractMultiMapIndexCreationTask<Index_NameAndCountry.Result>
        {
            public class Result
            {
                public string Name { get; set; }
                public string Country { get; set; }
            }


            public Index_NameAndCountry()
            {
                //AddMap<Employee>(emploees => from e in emploees
                //                             select new Result
                //                             {
                //                                 Name = e.FirstName + " " + e.LastName,
                //                                 Country = e.Address.Country 
                //                             });



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

        [HttpGet]
        public object MultiMapIndexingQuery()
        {
            Store.ExecuteIndex(new Index_NameAndCountry());

            using (var session = Store.OpenSession())
            {
                /*
                var result = session.Query<Index_NameAndCountry.Result>()
                    .Search(x => x.Country, "USA").Customize(x=>x.WaitForNonStaleResults())
                    .Select(x =>
                        new
                        {
                            x.Name,
                            x.Country
                        })
                        .ToList();
            */
                var namesList = new List<string>();

                var query =
                    session.Query<Index_NameAndCountry.Result, Index_NameAndCountry>()
                    .ProjectFromIndexFieldsInto<Index_NameAndCountry.Result>()
                    .Search(x => x.Country, "USA");

                using (var enumerator = session.Advanced.Stream(query))
                {
                    while (enumerator.MoveNext())
                    {
                        var result = enumerator.Current.Document;
                        namesList.Add(result.Name);
                    }
                }



                return namesList;
            }
        }
    }
}