package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;

public class ProductSales extends AbstractIndexCreationTask {

    public static class Result {
        private String product;
        private int count;
        private double total;

        public String getProduct() {
            return product;
        }

        public void setProduct(String product) {
            this.product = product;
        }

        public int getCount() {
            return count;
        }

        public void setCount(int count) {
            this.count = count;
        }

        public double getTotal() {
            return total;
        }

        public void setTotal(double total) {
            this.total = total;
        }
    }

    public ProductSales() {
        map = "from order in docs.Orders " +
                "from line in order.Lines " +
                "select new { " +
                "  Product = line.Product, " +
                "  Count = 1, " +
                "  Total = (((decimal)line.Quantity) * line.PricePerUnit) * (1M - line.Discount) " +
                "}";

        reduce = "from result in results " +
                "group result by result.Product into g " +
                "select new { " +
                "  Product = g.Key, " +
                "  Count = Enumerable.Sum(g, x => ((int)x.Count)), " +
                "  Total = Enumerable.Sum(g, x0 => ((decimal)x0.Total)) " +
                "}";
    }

}
