package net.ravendb.demo.compareExchange.indexCompareExchange;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.AbstractJavaScriptIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.javascriptIndexes.javascriptMapIndex.JavascriptMapIndex;
import java.util.List;

public class IndexCompareExchange {
    //region demo
    //region Step_1
    public static class Products_ByUnitsInStock extends AbstractIndexCreationTask {
    //endregion
        //region Step_2
        public static class IndexEntry
        {
            private int unitsInStock;

            public int getUnitsInStock() {
                return unitsInStock;
            }

            public void setUnitsInStock(int unitsInStock) {
                this.unitsInStock = unitsInStock;
            }
        }
        //endregion

        //region Step_3
        public Products_ByUnitsInStock() {
            map = "docs.Products.Select(product => new {\n" +
                "    UnitsInStock = this.LoadCompareExchangeValue(Id(product))\n" +
                "})";
        }
        //endregion
    }
    //endregion

    public List<Product> run(RunParams runParams) {
        int minValue = runParams.getMinValue();
        new Products_ByUnitsInStock().execute(DocumentStoreHolder.store);
        //region demo
        List<Product> products;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            products = session.query(Products_ByUnitsInStock.IndexEntry.class, Products_ByUnitsInStock.class)
                .whereGreaterThan("UnitsInStock" , minValue)
                .ofType(Product.class)
                .toList();
            //endregion
        //endregion
            return products;
        }
    }

    public static class RunParams {
        private int minValue;


        public int getMinValue() {
            return minValue;
        }

        public void setMinValue(int minValue) {
            this.minValue = minValue;
        }
    }
}
