using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using DemoMethods.Helpers;

namespace DemoMethods
{
    public partial class MenuController : ApiController
    {
        private class MenuResults
        {
            public List<DemoInformation> Demos { get; set; }
        }

        [HttpGet]
        public object Index()
        {
            var allControllerTypes = from type in GetType().Assembly.GetTypes()
                                     where type.BaseType.FullName.Contains("ApiController")
                                     select type;

            var allPublicMethods = allControllerTypes.SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                                                     .Where(x => x.CustomAttributes.Any(attr => attr.AttributeType == typeof(HttpGetAttribute)))
                                                     .Where(x => x.DeclaringType != null && x.DeclaringType.Name.Contains(x.Name) == false)
                                                     .Where(x => x.DeclaringType != null && !x.DeclaringType.Name.Contains("DemoStudio"))
                                                     .Where(x => x.DeclaringType != null && !(x.DeclaringType.Name.Contains("Menu") && !x.Name.Contains("CreateIndexes") && !x.Name.Contains("CreateLastFmDataset")))
                                                     .Select(x => new DemoInformation
                                                     {
                                                         ControllerName = x.DeclaringType.Name.Replace("Controller", string.Empty),
                                                         DemoName = x.Name,
                                                         DemoDisplayName = DemoUtilities.ExtractDemoDisplayName(x),
                                                         DemoParameters = DemoUtilities.ExtractDemoParameters(x),
                                                         DemoOrder = DemoUtilities.ExtractDemoOrder(x),
                                                         DemoOutputType = DemoUtilities.ExtractDemoOutputType(x).ToString()
                                                     });

            var result = allPublicMethods
                .OrderBy(x => x.ControllerName, new ControllerNameComparer())
                .ThenBy(x => x.DemoOrder)
                .ToList();

            var resObj = new MenuResults
            {
                Demos = result
            };

            return resObj;
        }

        [HttpGet]
        public object LoadCsFile()
        {
            try
            {
                var nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var file = nvc["FileName"];
                var explanation = nvc["Docname"];
                if (explanation != null)
                {
                    try
                    {
                        var docpath = Path.GetFullPath("../../../DemoMethods/" + explanation + ".html");
                        var doclines = File.ReadAllText(docpath);
                        return doclines;
                    }
                    catch (Exception)
                    {
                        return string.Empty;
                    }
                }

                if (file == null)
                    return "No code found...";

                var path = Path.GetFullPath("../../../DemoMethods/" + file + ".cs");

                return File.ReadAllText(path);
            }
            catch (Exception)
            {
                return "No code available for this demo";
            }
        }
    }

    public class ControllerNameComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == "Basic" && y == "Advanced")
                return -1;

            if (y == "Basic" && x == "Advanced")
                return 1;

            return string.Compare(x, y, StringComparison.Ordinal);
        }
    }

    public class DemoInformation
    {
        public DemoInformation()
        {
            DemoParameters = new List<DemoParameterInformation>();
        }

        public string ControllerName { get; set; }

        public string DemoName { get; set; }

        public string DemoDisplayName { get; set; }

        public string DemoOutputType { get; set; }

        public List<DemoParameterInformation> DemoParameters { get; set; }

        public int DemoOrder { get; set; }
    }

    public class DemoParameterInformation
    {
        public string ParameterName { get; set; }

        public string ParameterType { get; set; }

        public bool IsRequired { get; set; }
    }
}
