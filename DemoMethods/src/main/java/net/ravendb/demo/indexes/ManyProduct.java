package net.ravendb.demo.indexes;

import net.ravendb.client.indexes.AbstractIndexCreationTask;

public class ManyProduct extends AbstractIndexCreationTask {

    public ManyProduct() {
        map = "from product in docs.ProductNames " +
                "select new { " +
                "  product = product " +
                "}";
    }
}
