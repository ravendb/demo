package net.ravendb.demo.advanced;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.LastFm;
import net.ravendb.demo.indexes.LastFmAnalyzed;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class FullTextSearch {

    @GetMapping("/advanced/fullTextSearch")
    public List<LastFm> fullTextSearch(
            @RequestParam(value = "searchTerm", defaultValue = "Jazz") String searchTerm) {

        try (IDocumentSession session = DocumentStoreHolder.getMediaStore().openSession()) {
            Reference<QueryStatistics> statsRef = new Reference<>();
            List<LastFm> results = session.query(LastFmAnalyzed.Result.class, LastFmAnalyzed.class)
                    .statistics(statsRef)
                    .search("Query", searchTerm)
                    .ofType(LastFm.class)
                    .selectFields(LastFm.class, "Artist", "Tags", "TimeStamp", "Title", "TrackId")
                    .take(20)
                    .toList();

            long serverTime = statsRef.value.getDurationInMs();

            return results;
        }
    }
}
