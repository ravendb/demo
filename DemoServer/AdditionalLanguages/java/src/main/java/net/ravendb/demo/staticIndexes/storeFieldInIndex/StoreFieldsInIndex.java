//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import java.util.Date;
import java.util.List;

public class StoreFieldsInIndex {
    //region Demo
    //region Step_1
    public class OrdersQuantity_ByCompany extends AbstractIndexCreationTask {
    //endregion

        //region Step_2
        public class IndexEntry {
            public String company;
            public int totalItemsOrdered;
        }
        //endregion
        
        //region Step_3
        public static class OrderProjectedDetails {
            public Date orderedAt;
            public int totalItemsOrdered;
        }
        //endregion

        public OrdersQuantity_ByCompany() {
            //region Step_4
            map = "docs.Orders.Select(order => new { \n" +
                "   Company = order.Company, \n" +
                "   TotalItemsOrdered = Enumerable.Sum(order.Lines, orderLine => ((int) orderLine.Quantity)) \n"+
                "})";
            //endregion
            //region Step_5
            store("TotalItemsOrdered", FieldStorage.YES);
            //endregion
        }
    }
    //endregion

    public void run( String companyId ){
        //region Demo
        List<OrdersQuantity_ByCompany.OrderProjectedDetails> ordersDetails;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            
            IDocumentQuery<OrdersQuantity_ByCompany.OrderProjectedDetails> ordersQuery = session
                //region Step_6 
                .query(OrdersQuantity_ByCompany.IndexEntry.class, OrdersQuantity_ByCompany.class)
                .whereEquals("Company", companyId)
                //endregion
                //region Step_7
                .selectFields(OrdersQuantity_ByCompany.OrderProjectedDetails.class);
                 //endregion

            //region Step_8
            ordersDetails = ordersQuery.toList();
            //endregion
        }
        //endregion
    }
}
