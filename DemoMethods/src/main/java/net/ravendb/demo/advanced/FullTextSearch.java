package net.ravendb.demo.advanced;

import net.ravendb.abstractions.basic.Reference;
import net.ravendb.client.IDocumentSession;
import net.ravendb.client.RavenQueryStatistics;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.LastFm;
import net.ravendb.demo.indexes.LastFmAnalyzed;
import net.ravendb.demo.indexes.QLastFmAnalyzed_Result;
import net.ravendb.demo.indexes.TransformerLastFm;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class FullTextSearch {

    @RequestMapping("/Advanced/FullTextSearch")
    public List<LastFm> fullTextSearch(
            @RequestParam(value = "searchTerm", defaultValue = "Jazz") String searchTerm) {

        QLastFmAnalyzed_Result r = QLastFmAnalyzed_Result.result;

        try (IDocumentSession session = DocumentStoreHolder.getMediaStore().openSession()) {
            Reference<RavenQueryStatistics> statsRef = new Reference<>();
            List<LastFm> results = session.query(LastFmAnalyzed.Result.class, LastFmAnalyzed.class)
                    .search(r.query, searchTerm)
                    .transformWith(TransformerLastFm.class, LastFm.class)
                    .statistics(statsRef)
                    .toList();

            return results;
        }
    }
}
