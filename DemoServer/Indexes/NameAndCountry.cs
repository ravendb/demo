using System.Linq;
using DemoServer.Entities;
using Raven.Client.Documents.Indexes;

namespace DemoServer.Indexes
{
    public class NameAndCountry : AbstractMultiMapIndexCreationTask<NameAndCountry.Result>
    {
        public class Result
        {
            public string Name { get; set; }
            public string Country { get; set; }
            public string Id { get; set; }
        }


        public NameAndCountry()
        {
            AddMap<Employee>(employees => from e in employees
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


            Index(x => x.Country, FieldIndexing.Search);

            Store(x => x.Name, FieldStorage.Yes);
            Store(x => x.Country, FieldStorage.Yes);
        }
    }

    public class NameAndCountry_JavaScript : AbstractJavaScriptIndexCreationTask
    {
        public class Result
        {
            public string Name { get; set; }
            public string Country { get; set; }
            public string Id { get; set; }
        }

        public NameAndCountry_JavaScript()
        {
            Maps.Add(
                @"map('Employees', (e) => {
                    return {
                        Name : e.FirstName + ' ' + e.LastName,
                        Country : e.Address.Country
                    };
                })"
            );

            Maps.Add(
                @"map('Companies', (c) => {
                    return {
                        Name : c.Name,
                        Country : c.Address.Country
                    };
                })"
            );

            Maps.Add(
                @"map('Suppliers', (s) => {
                    return {
                        Name : s.Name,
                        Country : s.Address.Country
                    };
                })"
            );

            Fields.Add("Country", new IndexFieldOptions
            {
                Indexing = FieldIndexing.Search,
                Storage = FieldStorage.Yes
            });

            Fields.Add("Name", new IndexFieldOptions
            {
                Storage = FieldStorage.Yes
            });
        }
    }
}
