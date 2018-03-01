package net.ravendb.demo.basic;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.indexes.ProductSales;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

import java.util.List;

@Controller
public class MapReduceUsingDocumentQuery {

    @GetMapping("/basic/mapReduceUsingDocumentQuery")
    public List<ProductSales.Result> mapReduceUsingDocumentQuery() {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            Reference<QueryStatistics> statsRef = new Reference<>();

            List<ProductSales.Result> results = session
                    .advanced()
                    .documentQuery(ProductSales.Result.class, ProductSales.class)
                    .statistics(statsRef)
                    .toList();

            return results;
        }
    }
}
