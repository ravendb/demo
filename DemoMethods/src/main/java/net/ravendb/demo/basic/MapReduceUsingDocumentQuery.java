package net.ravendb.demo.basic;

import net.ravendb.abstractions.basic.Reference;
import net.ravendb.client.IDocumentSession;
import net.ravendb.client.RavenQueryStatistics;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.indexes.ProductSales;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.List;

@Controller
public class MapReduceUsingDocumentQuery {

    @RequestMapping("/Basic/MapReduceUsingDocumentQuery")
    public List<ProductSales.Result> mapReduceUsingDocumentQuery() {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            Reference<RavenQueryStatistics> statsRef = new Reference<>();

            List<ProductSales.Result> results = session
                    .advanced()
                    .documentQuery(ProductSales.Result.class, ProductSales.class)
                    .statistics(statsRef)
                    .toList();

            return results;
        }
    }
}
