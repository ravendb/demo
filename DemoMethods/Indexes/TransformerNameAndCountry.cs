using System.Linq;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class TransformerNameAndCountry : AbstractTransformerCreationTask<IndexNameAndCountry.Result>
    {
        public TransformerNameAndCountry()
        {
            TransformResults = results => from result in results
                                          select new
                                          {
                                              result.Name
                                          };
        }
    }

}