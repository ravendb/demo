using DemoMethods.Entities;
using Raven.Client;
using Raven.Client.Indexes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Raven.Abstractions.Indexing;
using System.Text;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {        
        [HttpGet]
        public object HighLights()
        {
            Store.ExecuteIndex(new Index_CompaniesAndAddresses());

            FieldHighlightings highlightings;

            using (var session = Store.OpenSession())
            {
                var results = session
                    .Advanced
                    .DocumentQuery<Company, Index_CompaniesAndAddresses>()
                    .Highlight("Address", 128, 1, out highlightings)
                    .Search("Address", "USA")
                    .ToList();

                StringBuilder builder = new StringBuilder()
                    .AppendLine("<ul>");

                foreach (Company result in results)
                {
                    string[] fragments = highlightings.GetFragments(result.Id);
                    builder.AppendLine(string.Format("<li>{0}</li>", fragments.First()));
                }

                string ul = builder
                    .AppendLine("</ul>")
                    .ToString();

                return ul;
            }
        }
    }
}