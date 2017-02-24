using System.Linq;
using DemoServer.Entities;
using Raven.Client.Documents.Transformers;

namespace DemoServer.Indexes
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