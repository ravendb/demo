package net.ravendb.demo.basic;

import net.ravendb.abstractions.data.Facet;
import net.ravendb.abstractions.data.FacetResults;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DemoUtilities;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Product;
import net.ravendb.demo.entities.QProduct;
import net.ravendb.demo.indexes.FacetRangeCreation;
import net.ravendb.demo.indexes.ProductsAndPriceAndSuplier;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.Arrays;
import java.util.List;

@Controller
public class FacetsDynamicRange {

    @RequestMapping("/Basic/FacetsDynamicRange")
    public List<FacetRangeCreation.FacetsRangesResults> facetsDynamicRange(
            @RequestParam (value = "fromVal", defaultValue = "10") int from,
            @RequestParam(value = "toVal", defaultValue = "20") int to) {

        QProduct p = QProduct.product;

        Facet facet1 = new Facet();
        facet1.setName("Products");

        Facet facet2 = new Facet();
        facet2.setName(p.pricePerUnit);
        facet2.setRanges(p.pricePerUnit.lt(from),
                p.pricePerUnit.goe(from).and(p.pricePerUnit.lt(to)),
                p.pricePerUnit.lt(to));

        Facet facet3 = new Facet();
        facet3.setName(p.unitsInStock);
        facet3.setRanges(p.unitsInStock.lt(10),
                p.unitsInStock.goe(10));

        List<Facet> newFacet = Arrays.asList(facet1, facet2, facet3);

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            FacetResults facetResults = session
                    .query(Product.class, ProductsAndPriceAndSuplier.class)
                    .where(p.unitsInStock.gt(1))
                    .toFacets(newFacet);

            return DemoUtilities.formatRangeResults(facetResults.getResults());
        }
    }
}
