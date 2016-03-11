package net.ravendb.demo.basic;

import net.ravendb.abstractions.basic.Reference;
import net.ravendb.client.IDocumentSession;
import net.ravendb.client.RavenQueryStatistics;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import net.ravendb.demo.entities.QCompany;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class Query2 {
    @RequestMapping("/Basic/Query2")
    public Company query2(
            @RequestParam(value = "externalId", defaultValue = "ALFKI") String externalId) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            Reference<RavenQueryStatistics> statsRef = new Reference<>();

            QCompany c = QCompany.company;

            List<Company> query = session.query(Company.class)
                    .statistics(statsRef)
                    .where(c.externalId.eq(externalId))
                    .toList();

            Company company = query.stream().findFirst().orElse(null);
            return company;
        }
    }
}
