using System;
using System.Linq;
using System.Text;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {
        [HttpGet]
        [Demo("HighLights", DemoOutputType.String, demoOrder: 120)]
        public object HighLights(string searchTerm = "UK USA")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                FieldHighlightings highlightings;

                var results = session
                    .Advanced
                    .DocumentQuery<Company, CompaniesAndAddresses>()
                    .Highlight("Address", 128, 1, out highlightings)
                    .Search("Address", searchTerm)
                    .ToList();

                var builder = new StringBuilder()
                    .AppendLine("<ul>");

                foreach (var fragments in results.Select(result => highlightings.GetFragments(result.Id)))
                {
                    builder.AppendLine(string.Format("<li>{0}</li>", fragments.First()));
                }

                var ul = builder
                    .AppendLine("</ul>")
                    .ToString();

                return ul;
            }
        }
    }
}