package net.ravendb.demo.staticIndexes.fanoutIndex;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Order;
import org.apache.commons.lang3.ObjectUtils;

import java.util.List;

public class FanoutIndex {

    public static class Orders_ByProductDetails extends AbstractIndexCreationTask {

        public Orders_ByProductDetails() {
            this.map = "docs.Orders.SelectMany(order => order.Lines, (order, orderLine) => new {\n" +
                "    ProductId = orderLine.Product,\n" +
                "    ProductName = orderLine.ProductName\n" +
                "})";
        }
    }

    public List<Order> run(RunParams runParams) {
        String namePrefix = ObjectUtils.firstNonNull(runParams.getNamePrefix(), "Chocolade");

        List<Order> orders;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            orders = session.query(Order.class, Orders_ByProductDetails.class)
                .whereStartsWith("ProductName", namePrefix)
                .toList();
        }

        return orders;
    }

    public static class RunParams {
        private String namePrefix;

        public String getNamePrefix() {
            return namePrefix;
        }

        public void setNamePrefix(String namePrefix) {
            this.namePrefix = namePrefix;
        }
    }
}
