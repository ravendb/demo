using System;
using System.Collections.Generic;
using System.Web.Http;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        public class Result
        {
            public string Name;
        }
        [HttpGet]
        public object TransformerQuery(string country = "USA")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var namesList = new List<object>();

                var query =
                    session.Query<NameAndCountry.Result, NameAndCountry>()
                        .TransformWith<TransformerNameAndCountry, NameAndCountry.Result>()
                        .Search(x => x.Country, country);


                using (var enumerator = session.Advanced.Stream(query))
                {
                    while (enumerator.MoveNext())
                    {
                        var result = enumerator.Current.Document;
                        namesList.Add(new { result.Name });
                    }
                }
                return namesList;
            }
        }
    }
}