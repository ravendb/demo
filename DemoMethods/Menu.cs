using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Raven.Client.Indexes;
using ColorCode;

namespace DemoMethods
{
    public class MenuController : ApiController
    {
        private class MenuResults
        {
            public List<string> ListOfControllers { get; set; }
            public List<string> ListOfIndexes { get; set; }
        }

        public class FormattedMenuIndex
        {
            // public static object Format(List<string> list)
            public static void FormatControllerString(List<string> list)
            {
                for (int i = 0; i < list.Count; i++)
                    // list[i] = string.Format("{0}/{1}", DemoUtilities.ServerInfo, list[i].Replace("Controller", ""));
                    list[i] = string.Format("/{0}", list[i].Replace("Controller", ""));
                // return DemoUtilities.Instance.ObjectToJson(list);
            }
        }

        public int CustomCompare(string l, string r)
        {
            string[] wl = l.Split('/');
            string[] wr = l.Split('/');
            if (wl.Length > 1 && wr.Length > 1)
            {

            }


            return 42;
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
                                                     .Where(x => x.DeclaringType != null && !(x.DeclaringType.Name.Contains("Menu") && !x.Name.Contains("CreateIndexes")))
                                                     .Select(x => string.Format("{0}/{1}", x.DeclaringType.Name, x.Name)); // in VS2015 :  $"{x.DeclaringType.Name}/{x.Name}"

            var result = allPublicMethods.ToList();

            result.Sort(
                delegate(string s1, string s2)
                {
                    if (s1.Contains("Basic") && s2.Contains("Advanced"))
                        return -1;
                    if (s2.Contains("Basic") && s1.Contains("Advanced"))
                        return 1;
                    return string.Compare(s1, s2, StringComparison.Ordinal);
                }
                );

            FormattedMenuIndex.FormatControllerString(result);

            var container = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var indexesList = container.GetExportedValues<AbstractIndexCreationTask>().ToList();
            var multimapList = container.GetExportedValues<AbstractMultiMapIndexCreationTask>().ToList();
            var transformersList = container.GetExportedValues<AbstractTransformerCreationTask>().ToList();

            var allLists = new List<string>();
            allLists.AddRange(indexesList.Select(x => x.IndexName));
            allLists.AddRange(multimapList.Select(x => x.IndexName));
            allLists.AddRange(transformersList.Select(x => x.TransformerName));


            var resObj = new MenuResults()
            {
                ListOfControllers = result,
                ListOfIndexes = allLists
            };

            return (resObj);
        }

        [HttpGet]
        public object CreateIndexesAndTransformers()
        {
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), DocumentStoreHolder.Store);
            return ("Indexes were created successfully");
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
                        return string.Format("-Doc N/A-");
                    }
                }
                if (file == null)
                    return string.Format("No code found...");
                var path = Path.GetFullPath("../../../DemoMethods/" + file + ".cs");
                var lines = File.ReadAllText(path);

                string colorizedSourceCode = new CodeColorizer().Colorize(lines, Languages.CSharp);
                return colorizedSourceCode;
            }
            catch (Exception)
            {
                return string.Format("No code available for this demo");
            }
        }
    }
}
