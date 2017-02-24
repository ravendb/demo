using System.Linq;
using DemoServer.Entities;
using Raven.Client.Documents.Indexes;

namespace DemoServer.Indexes
{
    public class CategoryDescription : AbstractIndexCreationTask<Category>
    {
        public CategoryDescription()
        {
            Map = categories => from category in categories
                                select new
                                {
                                    category.Description
                                };

            Stores.Add(x => x.Description, FieldStorage.Yes);
            Analyzers.Add(x => x.Description, "Lucene.Net.Analysis.Standard.StandardAnalyzer");
        }
    }
}