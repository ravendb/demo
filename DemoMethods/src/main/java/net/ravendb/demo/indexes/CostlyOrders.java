package net.ravendb.demo.indexes;

import com.mysema.query.annotations.QueryEntity;
import net.ravendb.client.indexes.AbstractIndexCreationTask;

public class CostlyOrders extends AbstractIndexCreationTask {

    @QueryEntity
    public static class Result {
        private String orderId;
        private String delay;
        private double price;

        public String getOrderId() {
            return orderId;
        }

        public void setOrderId(String orderId) {
            this.orderId = orderId;
        }

        public String getDelay() {
            return delay;
        }

        public void setDelay(String delay) {
            this.delay = delay;
        }

        public double getPrice() {
            return price;
        }

        public void setPrice(double price) {
            this.price = price;
        }
    }

    public CostlyOrders() {
        map = "from order in docs.Orders " +
                "select new { " +
                "  OrderId = order.__document_id, " +
                "  Delay = order.ShippedAt - ((DateTime?)order.OrderedAt), " +
                "  Price = Enumerable.Sum(order.Lines, x => ((decimal)((((decimal)x.Quantity) * x.PricePerUnit) * (1M - x.Discount)))) " +
                "}";
    }

}
