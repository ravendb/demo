package net.ravendb.demo.staticIndexes.storeFieldInIndex;
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
    public static class OrdersQuantity_ByCompany extends AbstractIndexCreationTask {
    //endregion

        //region Step_2
        public static class IndexEntry {
            private String Company;
            private int TotalItemsOrdered;

            public String getCompany() {
                return Company;
            }

            public void setCompany(String company) {
                this.Company = company;
            }

            public int getTotalItemsOrdered() {
                return TotalItemsOrdered;
            }

            public void setTotalItemsOrdered(int totalItemsOrdered) {
                this.TotalItemsOrdered = totalItemsOrdered;
            }
        }
        //endregion

        //region Step_3
        public static class OrderProjectedDetails {
            private Date OrderedAt;
            private int TotalItemsOrdered;

            public Date getOrderedAt() {
                return OrderedAt;
            }

            public void setOrderedAt(Date orderedAt) {
                this.OrderedAt = orderedAt;
            }

            public int getTotalItemsOrdered() {
                return TotalItemsOrdered;
            }

            public void setTotalItemsOrdered(int totalItemsOrdered) {
                this.TotalItemsOrdered = totalItemsOrdered;
            }
        }
        //endregion

        public OrdersQuantity_ByCompany() {
            //region Step_4
            map = "docs.Orders.Select(order => new { \n" +
                "   Company = order.Company, \n" +
                "   TotalItemsOrdered = Enumerable.Sum(order.Lines, orderLine => ((int) orderLine.Quantity)) \n" +
                "})";
            //endregion
            //region Step_5
            store("TotalItemsOrdered", FieldStorage.YES);
            //endregion
        }
    }
    //endregion

    public List<OrdersQuantity_ByCompany.OrderProjectedDetails> run(RunParams runParams) {
        String companyId = runParams.getCompanyId();
        new OrdersQuantity_ByCompany().execute(DocumentStoreHolder.store);
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
        return ordersDetails;
    }

    public static class RunParams {
        private String companyId;

        public String getCompanyId() {
            return companyId;
        }

        public void setCompanyId(String companyId) {
            this.companyId = companyId;
        }
    }
}
