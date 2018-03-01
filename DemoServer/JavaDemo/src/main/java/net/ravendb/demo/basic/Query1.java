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
public class Query1 {
    @GetMapping("/basic/query1")
    public Company query1(
            @RequestParam(value = "country", defaultValue = "UK") String country) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            Reference<QueryStatistics> statsRef = new Reference<>();

            IDocumentQuery<Company> query = session.query(Company.class)
                    .statistics(statsRef)
                    .whereEquals("Address.Country", country);

            Company result = query.firstOrDefault();

            long serverTime = statsRef.value.getDurationInMs();

            return result;
        }
    }
}
