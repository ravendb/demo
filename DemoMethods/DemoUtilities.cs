using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using DemoMethods.Indexes;
using Newtonsoft.Json;
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
                    Key = key,
                    Value = t.Range,
                    Count = t.Count ?? 0
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


        public static string Json2Csv(string JsonString)
        {
            
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + JsonString + "}}");


            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);
            var xmlReader = new XmlNodeReader(xmldoc);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlReader);
            var table = dataSet.Tables[0];
            var delimiter = ",";
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : delimiter);
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : delimiter);
                }
            }
            return result.ToString().TrimEnd(new char[] { '\r', '\n' });
        }
    }
}
