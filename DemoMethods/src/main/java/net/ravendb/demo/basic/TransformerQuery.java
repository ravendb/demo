package net.ravendb.demo.basic;

import com.mysema.query.annotations.QueryEntity;
import net.ravendb.abstractions.basic.CloseableIterator;
import net.ravendb.abstractions.data.StreamResult;
import net.ravendb.client.IDocumentSession;
import net.ravendb.client.linq.IRavenQueryable;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.indexes.NameAndCountry;
import net.ravendb.demo.indexes.QNameAndCountry_Result;
import net.ravendb.demo.indexes.TransformerNameAndCountry;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.ArrayList;
import java.util.List;

@Controller
public class TransformerQuery {

    @RequestMapping("/Basic/TransformerQuery")
    public List<String> transformerQuery(
            @RequestParam(value = "country", defaultValue = "USA") String country) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            List<String> namesList = new ArrayList<>();

            QNameAndCountry_Result r = QNameAndCountry_Result.result;
            IRavenQueryable<NameAndCountry.Result> query = session
                    .query(NameAndCountry.Result.class, NameAndCountry.class)
                    .transformWith(TransformerNameAndCountry.class, NameAndCountry.Result.class)
                    .search(r.country, country);

            // stream the results:
            try (CloseableIterator<StreamResult<NameAndCountry.Result>> iterator =
                         session.advanced().stream(query)) {
                while (iterator.hasNext()) {
                    NameAndCountry.Result result = iterator.next().getDocument();
                    namesList.add(result.getName());
                }
            }

            return namesList;
        }
    }

    @QueryEntity
    public class Result {
        private String name;

        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }
    }
}
