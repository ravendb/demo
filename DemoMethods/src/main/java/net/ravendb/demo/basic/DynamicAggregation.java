package net.ravendb.demo.basic;

import net.ravendb.abstractions.data.FacetResults;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DemoUtilities;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Product;
import net.ravendb.demo.entities.QProduct;
import net.ravendb.demo.indexes.FacetRangeCreation;
import net.ravendb.demo.indexes.Products;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class DynamicAggregation {

    @RequestMapping("/Basic/DynamicAggregation")
    public List<FacetRangeCreation.FacetsRangesResults> dynamicAggregation(
            @RequestParam(value = "fromVal", defaultValue = "0") int fromVal,
            @RequestParam(value = "toVal", defaultValue = "999") int toVal) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            QProduct p = QProduct.product;
            FacetResults result = session.query(Product.class, Products.class)
                    .where(p.unitsInStock.goe(fromVal).and(p.unitsInStock.loe(toVal)))
                    .aggregateBy(p.category)
                    .sumOn(p.unitsInStock)
                    .toList();

            return DemoUtilities.formatRangeResults(result.getResults());
        }
    }
}
