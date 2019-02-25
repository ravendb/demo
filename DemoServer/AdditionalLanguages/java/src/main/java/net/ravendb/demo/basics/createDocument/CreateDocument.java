package net.ravendb.demo.basics;

//region Imports
import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
//endregion
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.LastFm;
import net.ravendb.demo.indexes.LastFmAnalyzed;

import java.util.List;

public class CreateDocument {

    //region Demo
    public List<LastFm> createDocument(String searchTerm) {

        //region Step_1
        try (IDocumentSession session = DocumentStoreHolder.getMediaStore().openSession()) {
        //endregion
            
            //region Step_2
            Reference<QueryStatistics> statsRef = new Reference<>();
            List<LastFm> results = session.query(LastFmAnalyzed.Result.class, LastFmAnalyzed.class)
                    .statistics(statsRef)
                    .search("Query", searchTerm)
                    .ofType(LastFm.class)
                    .selectFields(LastFm.class, "Artist", "Tags", "TimeStamp", "Title", "TrackId")
                    .take(20)
                    .toList();
            //endregion

            long serverTime = statsRef.value.getDurationInMs();

            return results;
        }
    }
    //endregion
}
