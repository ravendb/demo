package net.ravendb.demo.basic;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.IRawDocumentQuery;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.indexes.NameAndCountry;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class MultiMapIndexingQuery {

    @GetMapping("/basic/multiMapIndexingQuery")
    public List<IdAndName> multiMapIndexingQuery(
            @RequestParam(value = "country", defaultValue = "USA") String country) {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            IRawDocumentQuery<NameAndCountry.Result> query = session
                    .advanced()
                    .rawQuery(NameAndCountry.Result.class,
                            "FROM INDEX 'NameAndCountry' WHERE search(Country, $p0)");

            query.addParameter("p0", country);

            List<NameAndCountry.Result> result = query.toList();

            return session.query(NameAndCountry.Result.class, NameAndCountry.class)
                    .search("Country", country)
                    .selectFields(IdAndName.class, "Id", "Name")
                    .toList();
        }
    }

    public static class IdAndName {
        private String id;
        private String name;

        public IdAndName() {
        }

        public IdAndName(String id, String name) {
            this.id = id;
            this.name = name;
        }

        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        public String getId() {
            return id;
        }

        public void setId(String id) {
            this.id = id;
        }
    }
}
