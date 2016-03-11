package net.ravendb.demo.advanced;

import net.ravendb.abstractions.data.MoreLikeThisQuery;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.LastFm;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class MoreLikeThis {

    @RequestMapping("/Advanced/MoreLikeThis")
    public LastFm[] moreLikeThis(
            @RequestParam(value = "documentId", defaultValue = "lastfm/9295") String documentId) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            MoreLikeThisQuery query = new MoreLikeThisQuery();
            query.setIndexName("IndexFullTextSearch");
            query.setDocumentId(documentId);
            query.setFields(new String[] { "Artist" });

            LastFm[] mltByArtist = session
                    .advanced()
                    .moreLikeThis(LastFm.class, "IndexFullTestSearch", null, query);

            return mltByArtist;
        }
    }
}
