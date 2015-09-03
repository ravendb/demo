using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;

namespace DemoMethods
{
    public class DemoUtilities
    {
        public static string ServerInfo { get; set; }

        public static List<FacetsRangesResults> FormatRangeResults(
            Dictionary<string, FacetResult> results)
        {
            var lst = new List<FacetsRangesResults>();
            foreach (var key in results.Keys)
            {
                lst.AddRange(results[key].Values.Select(t => new FacetsRangesResults
                {
                    Key = key, Value = t.Range, Count = t.Count ?? 0
                }));
            }
            return lst;
        }

        public static void GetUserParameters(string query, NameValueCollection parameters)
        {
            var nvc = HttpUtility.ParseQueryString(query);
            foreach (var key in parameters.AllKeys)
            {
                parameters[key] = nvc[key] ?? parameters[key];
            }
        }
    }
}
