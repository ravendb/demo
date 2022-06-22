package net.ravendb.demo.staticIndexes.additionalSourcesIndex;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import org.apache.commons.lang3.ObjectUtils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;

public class AdditionalSourcesIndex {

    public static final String ADDITIONAL_SOURCE = "public static class DiscountUtils\n" +
        "{\n" +
        "    public static decimal CalcSalePrice(decimal price)\n" +
        "    {\n" +
        "        return price - price / 100M * 25M;\n" +
        "    }\n" +
        "    \n" +
        "    public static decimal CalcProfitPrice(decimal price)\n" +
        "    {\n" +
        "        return price + price / 100M * 25M;\n" +
        "    }\n" +
        "}";

    public static class Products_ByPrice extends AbstractIndexCreationTask {
        public Products_ByPrice() {
            this.map = "docs.Products.Select(product => new {\n" +
                "    ProductName = product.Name,\n" +
                "    OriginalPrice = product.PricePerUnit,\n" +
                "    SalePrice = DiscountUtils.CalcSalePrice(product.PricePerUnit),\n" +
                "    ProfitPrice = DiscountUtils.CalcProfitPrice(product.PricePerUnit)\n" +
                "})";

            this.setAdditionalSources(Collections.singletonMap("DiscountLogic", ADDITIONAL_SOURCE));
        }
    }

    public List<DataToShow> run(RunParams runParams) {
        int price = ObjectUtils.firstNonNull(runParams.getPrice(), 5);

        List<Product> lowCostProducts;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            lowCostProducts = session.query(Product.class, Products_ByPrice.class)
                .whereLessThan("SalePrice", price)
                .orderBy("SalePrice")
                .toList();
        }

        // Manipulate results to show because index fields are Not stored..
        List<DataToShow> productsData = new ArrayList<>();
        for (Product item : lowCostProducts) {
            DataToShow dataToShow = new DataToShow();
            dataToShow.setProductName(item.getName());
            dataToShow.setOriginalPrice(item.getPricePerUnit());
            dataToShow.setSalesPrice(item.getPricePerUnit() - item.getPricePerUnit() / 100 * 25);
            dataToShow.setProfitPrice(item.getPricePerUnit() + item.getPricePerUnit() / 100 * 25);
            productsData.add(dataToShow);
        }

        return productsData;
    }

    public static class RunParams {
        private Integer price;

        public Integer getPrice() {
            return price;
        }

        public void setPrice(Integer price) {
            this.price = price;
        }
    }

    public static class DataToShow {
        private String productName;
        private double originalPrice;
        private double salesPrice;
        private double profitPrice;

        public String getProductName() {
            return productName;
        }

        public void setProductName(String productName) {
            this.productName = productName;
        }

        public double getOriginalPrice() {
            return originalPrice;
        }

        public void setOriginalPrice(double originalPrice) {
            this.originalPrice = originalPrice;
        }

        public double getSalesPrice() {
            return salesPrice;
        }

        public void setSalesPrice(double salesPrice) {
            this.salesPrice = salesPrice;
        }

        public double getProfitPrice() {
            return profitPrice;
        }

        public void setProfitPrice(double profitPrice) {
            this.profitPrice = profitPrice;
        }
    }
}
