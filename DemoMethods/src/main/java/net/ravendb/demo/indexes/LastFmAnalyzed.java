package net.ravendb.demo.indexes;

import com.mysema.query.annotations.QueryEntity;
import net.ravendb.abstractions.indexing.FieldIndexing;
import net.ravendb.client.indexes.AbstractIndexCreationTask;

public class LastFmAnalyzed extends AbstractIndexCreationTask {

    @QueryEntity
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

        QLastFmAnalyzed_Result result = QLastFmAnalyzed_Result.result;

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

        index(result.query, FieldIndexing.ANALYZED);
    }
}
