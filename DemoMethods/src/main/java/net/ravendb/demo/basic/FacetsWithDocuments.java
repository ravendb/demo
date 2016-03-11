package net.ravendb.demo.basic;

import net.ravendb.abstractions.data.FacetResults;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DemoUtilities;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.MenuController;
import net.ravendb.demo.entities.Product;
import net.ravendb.demo.entities.QProduct;
import net.ravendb.demo.indexes.FacetRangeCreation;
import net.ravendb.demo.indexes.ProductsAndPriceAndSuplier;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.List;

@Controller
public class FacetsWithDocuments {

    @RequestMapping("/Basic/FacetsWithDocuments")
    public List<FacetRangeCreation.FacetsRangesResults> facetsWithDocuments() {

        QProduct p = QProduct.product;
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            FacetResults facetResults = session.query(Product.class, ProductsAndPriceAndSuplier.class)
                    .where(p.unitsInStock.gt(1))
                    .toFacets(MenuController.getFixedFacet());

            return DemoUtilities.formatRangeResults(facetResults.getResults());
        }
    }
}