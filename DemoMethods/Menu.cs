using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using DemoMethods.Entities;
using Raven.Client.Indexes;

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
                    list[i] = string.Format("{0}/{1}", DemoUtilities.ServerInfo, list[i].Replace("Controller", ""));
                // return DemoUtilities.Instance.ObjectToJson(list);
            }
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
                                                     .Select(x => string.Format("{0}/{1}", x.DeclaringType.Name, x.Name)); // in VS2015 :  $"{x.DeclaringType.Name}/{x.Name}"

            var result = allPublicMethods.ToList();
            result.Sort();
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
    }
}
