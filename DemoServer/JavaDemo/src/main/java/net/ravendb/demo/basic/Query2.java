package net.ravendb.demo.basic;

import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class Query2 {

    @GetMapping("/basic/query2")
    public Company query2(
            @RequestParam(value = "externalId", defaultValue = "ALFKI") String externalId) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            Reference<QueryStatistics> statsRef = new Reference<>();

            IDocumentQuery<Company> query = session.query(Company.class)
                    .statistics(statsRef)
                    .whereEquals("ExternalId", externalId);

            Company result = query.firstOrDefault();

            long serverTime = statsRef.value.getDurationInMs();

            return result;
        }
    }
}
