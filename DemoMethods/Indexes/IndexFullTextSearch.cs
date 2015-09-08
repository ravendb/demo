using System;
using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class IndexFullTextSearch : AbstractIndexCreationTask<LastFm>
    {
        public class Result
        {
            public string Query;
        }

        public IndexFullTextSearch()
        {
            Map = songs => from song in songs
                select new
                {
                    Query = String.Join(" ",song.Artist, song.TimeStamp, song.Tags.SelectMany(y => y), song.Title, song.Track_Id)
                };
                          
            Analyze("Query", "Lucene.Net.Analysis.Standard.StandardAnalyzer");            
        }
    }
}