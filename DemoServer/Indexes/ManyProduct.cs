using System.Linq;
using Raven.Client.Documents.Indexes;

namespace DemoServer.Indexes
{
    public class ManyProduct : AbstractIndexCreationTask<ProductName>
    {
        public ManyProduct()
        {
            Map = products => from product in products
                              select new
                              {
                                  product
                              };
        }
    }
}