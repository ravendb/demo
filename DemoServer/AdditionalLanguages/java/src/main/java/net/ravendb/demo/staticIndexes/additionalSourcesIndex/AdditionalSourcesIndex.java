package net.ravendb.demo.staticIndexes.additionalSourcesIndex;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import org.apache.commons.lang3.ObjectUtils;

public class AdditionalSourcesIndex {

    //region Demo
    //region Step_1
    public static class Products_ByPrice extends AbstractIndexCreationTask {
    //endregion
    
        //region Step_2
        public static class IndexEntry {
             private String ProductName;
             private double OriginalPrice;
             private double SalePrice;
             private double ProfitPrice;
        }
        //endregion
        
        public Products_ByPrice() {
            //region Step_3
            map = "docs.Products.Select(product => new {" +
                "    ProductName = product.Name," +
                "    OriginalPrice = product.PricePerUnit," +
                "    SalePrice = DiscountUtils.CalcSalePrice(product.PricePerUnit)," +
                "    ProfitPrice = DiscountUtils.CalcProfitPrice(product.PricePerUnit)" +
                "})";
            //endregion

            //region Step_4
            additionalSources = Collections.singletonMap("DiscountLogic", ADDITIONAL_SOURCE);
            //endregion
        }
    }
    //endregion

    public List<DataToShow> run(RunParams runParams) {
        int price = ObjectUtils.firstNonNull(runParams.getPrice(), 5);

        //region Demo
        List<Product> lowCostProducts;
        //region Step_5
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            lowCostProducts = session.query(Product.class, Products_ByPrice.class)
                .whereLessThan("SalePrice", price)
                .orderBy("SalePrice")
                .toList();
        }
        //endregion
        //endregion

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
    
    //region Demo
    //region Step_6
    public static final String ADDITIONAL_SOURCE = "public static class DiscountUtils" +
        "{" +
        "    public static decimal CalcSalePrice(decimal price)" +
        "    {" +
        "        return price - price / 100M * 25M;" +
        "    }" +
        "     " +
        "    public static decimal CalcProfitPrice(decimal price)" +
        "    {" +
        "        return price + price / 100M * 25M;" +
        "    }" +
        "}";
    //endregion
    //endregion

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
