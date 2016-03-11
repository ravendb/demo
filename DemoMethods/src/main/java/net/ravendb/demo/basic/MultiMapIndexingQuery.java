package net.ravendb.demo.basic;

import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.indexes.NameAndCountry;
import net.ravendb.demo.indexes.QNameAndCountry_Result;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;
import java.util.stream.StreamSupport;

import static java.util.stream.Collectors.toList;

@Controller
public class MultiMapIndexingQuery {

    @RequestMapping("/Basic/MultiMapIndexingQuery")
    public List<IdAndName> multiMapIndexingQuery(
            @RequestParam(value = "country", defaultValue = "USA") String country) {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            QNameAndCountry_Result n = QNameAndCountry_Result.result;

            return StreamSupport.stream(
                    session.query(NameAndCountry.Result.class, NameAndCountry.class)
                    .search(n.country, country)
                    .spliterator(), false)
                    .map(x -> new IdAndName(x.getId(), x.getName()))
                    .collect(toList());
        }
    }

    public static class IdAndName {
        private String id;
        private String name;

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
