using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
    }
}
