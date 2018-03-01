package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;

public class Products extends AbstractIndexCreationTask {

    public Products() {
        map = "from product in docs.Products " +
                "select new { " +
                "  Category = product.Category, " +
                "  UnitsInStock = product.UnitsInStock " +
                "}";
    }
}
