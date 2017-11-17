using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using DemoServer.Demos.Menu;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Raven.Client.Documents.Commands;

namespace DemoServer
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
                    Count = t.Count
                }));
            }
            return lst;
        }

        public static List<DemoParameterInformation> ExtractDemoParameters(MethodInfo demoMethod)
        {
            var results = new List<DemoParameterInformation>();

            var parameters = demoMethod.GetParameters();
            foreach (var parameter in parameters)
            {
                var parameterType = GetParameterType(parameter.ParameterType);
                var parameterName = parameter.Name;
                var parameterIsRequired = GetParameterIsRequired(parameter);

                results.Add(new DemoParameterInformation
                {
                    ParameterType = parameterType,
                    IsRequired = parameterIsRequired,
                    ParameterName = parameterName
                });
            }

            return results;
        }

        private static bool GetParameterIsRequired(ParameterInfo parameter)
        {
            var optionalAttribute = parameter.GetCustomAttribute<OptionalAttribute>();

            return optionalAttribute == null && parameter.HasDefaultValue == false;
        }

        private static string GetParameterType(Type type)
        {
            var numericalTypes = new List<Type>
            {
                typeof (decimal),
                typeof (int),
                typeof (long),
                typeof (short),
                typeof (float),
                typeof (double)
            };

            return numericalTypes.Any(numericalType => numericalType == type)
                ? "Number"
                : "String";
        }

        public static string ExtractDemoDisplayName(MethodInfo demoMethod)
        {
            var demoAttribute = demoMethod.GetCustomAttribute<DemoAttribute>();
            if (demoAttribute == null || string.IsNullOrWhiteSpace(demoAttribute.DisplayName))
                return demoMethod.Name;

            return demoAttribute.DisplayName;
        }

        public static DemoOutputType ExtractDemoOutputType(MethodInfo demoMethod)
        {
            var demoAttribute = demoMethod.GetCustomAttribute<DemoAttribute>();
            if (demoAttribute == null)
                return DemoOutputType.Standard;

            return demoAttribute.DemoOutputType;
        }

        public static int ExtractDemoOrder(MethodInfo demoMethod)
        {
            var demoAttribute = demoMethod.GetCustomAttribute<DemoAttribute>();
            if (demoAttribute == null)
                return DemoAttribute.DefaultDemoOrder;

            return demoAttribute.DemoOrder;
        }
    }
}
