using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class TransformerLastFm : AbstractTransformerCreationTask<LastFm>
    {
        public TransformerLastFm()
        {
            TransformResults = results => from result in results
                                          select new
                                          {
                                              result.Artist,
                                              result.TimeStamp,
                                              result.Tags,
                                              result.TrackId,
                                              result.Title
                                          };
        }
    }

}