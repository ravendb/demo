package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;

public class ProductsAndPriceAndSuplier extends AbstractIndexCreationTask {

    public static class Result {

        private String productId;
        private double pricePerUnit;
        private int unitsInStock;

        public String getProductId() {
            return productId;
        }

        public void setProductId(String productId) {
            this.productId = productId;
        }

        public double getPricePerUnit() {
            return pricePerUnit;
        }

        public void setPricePerUnit(double pricePerUnit) {
            this.pricePerUnit = pricePerUnit;
        }

        public int getUnitsInStock() {
            return unitsInStock;
        }

        public void setUnitsInStock(int unitsInStock) {
            this.unitsInStock = unitsInStock;
        }
    }

    public ProductsAndPriceAndSuplier() {
        map = "from product in docs.Products " +
                "select new { " +
                "  ProductId = product.__document_id, " +
                "  PricePerUnit = product.PricePerUnit, " +
                "  UnitsInStock = product.UnitsInStock " +
                "}";
    }

}
