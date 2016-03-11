package net.ravendb.demo.basic;

import net.ravendb.abstractions.basic.Reference;
import net.ravendb.client.FieldHighlightings;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import net.ravendb.demo.indexes.CompaniesAndAddresses;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class Highlights {

    @RequestMapping("/Basic/HighLights")
    public String highLights(
            @RequestParam(value = "address", defaultValue = "UK USA") String address) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            Reference<FieldHighlightings> highlightingsRef = new Reference<>();

            List<Company> results = session.advanced()
                    .documentQuery(Company.class, CompaniesAndAddresses.class)
                    .highlight("Address", 128, 1, highlightingsRef)
                    .search("Address", address)
                    .toList();

            StringBuilder builder = new StringBuilder();

            builder.append("<ul>");

            for (Company result : results) {
                String[] fragments = highlightingsRef.value.getFragments(result.getId());
                for (String fragment : fragments) {
                    builder.append(String.format("<li>%s</li>", fragment));
                }
            }

            String ul = builder.append("</ul>").toString();
            return ul;
        }
    }
}
