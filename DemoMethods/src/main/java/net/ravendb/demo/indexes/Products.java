package net.ravendb.demo.indexes;

import net.ravendb.abstractions.indexing.SortOptions;
import net.ravendb.client.indexes.AbstractIndexCreationTask;
import net.ravendb.demo.entities.QProduct;

public class Products extends AbstractIndexCreationTask {

    public Products() {
        QProduct product = QProduct.product;

        map = "from product in docs.Products " +
                "select new { " +
                "  Category = product.Category, " +
                "  UnitsInStock = product.UnitsInStock " +
                "}";

        sort(product.unitsInStock, SortOptions.INT);
    }
}
