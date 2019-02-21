package net.ravendb.demo.basics;

//region Usings
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

            //region Step_3
            long serverTime = statsRef.value.getDurationInMs();
            //endregion

            return results;
        }
    }
    //endregion

    public List<LastFm> anotherMethod(String searchTerm) {

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.getMediaStore().openSession()) {
            
            //region Step_4
            Reference<QueryStatistics> statsRef = new Reference<>();
            //endregion
            List<LastFm> results = session.query(LastFmAnalyzed.Result.class, LastFmAnalyzed.class)
                    .statistics(statsRef)
                    .search("Query", searchTerm)
                    .ofType(LastFm.class)
                    .selectFields(LastFm.class, "Artist", "Tags", "TimeStamp", "Title", "TrackId")
                    .take(20)
                    .toList();

            //region Step_5
            long serverTime = statsRef.value.getDurationInMs();

            return results;
            //endregion
        }
        //endregion
    }

    //region Demo
    public List<LastFm> thirdMethod(String searchTerm) {

        try (IDocumentSession session = DocumentStoreHolder.getMediaStore().openSession()) {
            
            Reference<QueryStatistics> statsRef = new Reference<>();
            List<LastFm> results = session.query(LastFmAnalyzed.Result.class, LastFmAnalyzed.class)
                    .statistics(statsRef)
                    .search("Query", searchTerm)
                    .ofType(LastFm.class)
                    .selectFields(LastFm.class, "Artist", "Tags", "TimeStamp", "Title", "TrackId")
                    .take(20)
                    .toList();

            //region Step_6
            long serverTime = statsRef.value.getDurationInMs();

            return results;
            //endregion
        }
    }
    //endregion
}
