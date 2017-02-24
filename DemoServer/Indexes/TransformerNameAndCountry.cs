using System.Linq;
using Raven.Client.Documents.Transformers;

namespace DemoServer.Indexes
{
    public class TransformerNameAndCountry : AbstractTransformerCreationTask<NameAndCountry.Result>
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