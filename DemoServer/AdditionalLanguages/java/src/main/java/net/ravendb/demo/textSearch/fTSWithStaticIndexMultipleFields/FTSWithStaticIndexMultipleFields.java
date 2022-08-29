package net.ravendb.demo.textSearch.fTSWithStaticIndexMultipleFields;

//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.LastFm;

public class FTSWithStaticIndexMultipleFields {

    //region Demo
    //region Step_1
    public static class Song_TextData extends AbstractIndexCreationTask {
    //endregion

        public Song_TextData() {
            //region Step_2
            map = "docs.LastFms.Select(song => new { " +
                "    songData = new object[] { " +
                "        song.Artist, " +
                "        song.Title, " +
                "        song.Tags, " +
                "        song.TrackId " +
                "    } " +
                "})";
            //endregion

            //region Step_3
            index("songData", FieldIndexing.SEARCH);
            //endregion
        }
    }
    //endregion

    public List<LastFm> run(RunParams runParams) {
        String searchTerm = runParams.getSearchTerm();

        //region Demo
        List<LastFm> results;

        try (IDocumentSession session = DocumentStoreHolder.mediaStore.openSession()) {
            //region Step_4
            results = session.query(LastFm.class, Song_TextData.class)
                .search("songData", searchTerm)
                .take(20)
                .toList();
            //endregion
        }
        //endregion

        return results;
    }

    public static class RunParams {
        private String searchTerm;

        public String getSearchTerm() {
            return searchTerm;
        }

        public void setSearchTerm(String searchTerm) {
            this.searchTerm = searchTerm;
        }
    }
}
