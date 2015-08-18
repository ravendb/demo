using System.Linq;
using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class IndexCategory : AbstractIndexCreationTask<Category>
    {
        public IndexCategory()
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