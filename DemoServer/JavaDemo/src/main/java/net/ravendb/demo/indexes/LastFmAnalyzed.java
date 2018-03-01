package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.indexes.FieldTermVector;

public class LastFmAnalyzed extends AbstractIndexCreationTask {

    public static class Result {
        private String query;

        public String getQuery() {
            return query;
        }

        public void setQuery(String query) {
            this.query = query;
        }
    }

    public LastFmAnalyzed() {

        map = "from song in docs.LastFms " +
                "select new { " +
                "  Query = new object[] { " +
                "    song.Artist, " +
                "    ((object)song.TimeStamp), " +
                "    song.Tags, " +
                "    song.Title, " +
                "    song.TrackId " +
                "  } " +
                "}";

        index("Query", FieldIndexing.SEARCH);
        termVector("Query", FieldTermVector.YES);
    }
}
