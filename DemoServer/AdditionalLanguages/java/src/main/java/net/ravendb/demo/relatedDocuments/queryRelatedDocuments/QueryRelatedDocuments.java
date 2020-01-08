package net.ravendb.demo.relatedDocuments.queryRelatedDocuments;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Order;
import net.ravendb.demo.common.models.Product;

import java.util.List;
import java.util.stream.Collectors;

public class QueryRelatedDocuments {

    public void run() {
        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            List<Order> shippedOrders = session.query(Order.class)
                .include("lines.product")
                .whereExists("shippedAt")
                .toList();
            //endregion

            //region Step_2
            for (Order shippedOrder : shippedOrders) {
                List<String> productIds = shippedOrder.getLines().stream().map(x -> x.getProduct()).collect(Collectors.toList());
            //endregion
                for (int i = 0; i < productIds.size(); i++) {
                    //region Step_3
                    Product product = session.load(Product.class, productIds.get(i));
                    product.setUnitsOnOrder(product.getUnitsOnOrder() + shippedOrder.getLines().get(i).getQuantity());
                    //endregion
                }
            }

            //region Step_4
            session.saveChanges();
            //endregion

        }
        //endregion
    }
}
