package net.ravendb.demo.staticIndexes.fanoutIndex;

//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Order;
import org.apache.commons.lang3.ObjectUtils;

import java.util.List;

public class FanoutIndex {
    //region Demo
    //region Step_1
    public static class Orders_ByProductDetails extends AbstractIndexCreationTask {
    //endregion
    
        //region Step_2
        public static class IndexEntry {
             private String ProductId;
             private String ProductName;
        }
        //endregion
             
        //region Step_3
        public Orders_ByProductDetails() {
            map = "docs.Orders.SelectMany(order => order.Lines, (order, orderLine) => new {" +
                "    ProductId = orderLine.Product," +
                "    ProductName = orderLine.ProductName" +
                "})";
        }
        //endregion
    }
    //endregion

    public List<Order> run(RunParams runParams) {
        String namePrefix = ObjectUtils.firstNonNull(runParams.getNamePrefix(), "Chocolade");

        //region Demo
        List<Order> orders;
        
        //region Step_4
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            
            orders = session.query(Order.class, Orders_ByProductDetails.class)
                .whereStartsWith("ProductName", namePrefix)
                .toList();
        }
        //endregion
        //endregion

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
