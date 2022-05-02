package net.ravendb.demo.compareExchange.indexCompareExchange;

import com.google.common.collect.Sets;
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.AbstractJavaScriptIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.javascriptIndexes.javascriptMapIndex.JavascriptMapIndex;

import java.util.List;

public class IndexCompareExchange {
    //region demo
    public static class Products_ByUnitsInStock extends AbstractIndexCreationTask {
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

        public Products_ByUnitsInStock() {
            /*
                setMaps(Sets.newHashSet(
                    "map('Products', function (product) {\n" +
                    "   return {\n" +
                    "           UnitsInStock: this.LoadCompareExchangeValue(Id(product))\n" +
                    "       };\n" +
                    "})"
                ));

             */
            map = "docs.Products.Select(product => new {\n" +
                "    UnitsInStock = this.LoadCompareExchangeValue(Id(product))\n" +
                "})";
        }
    }
    //endregion

    public List<Product> run(RunParams runParams) {
        int minValue = runParams.getMinValue();
        new Products_ByUnitsInStock().execute(DocumentStoreHolder.store);

        List<Product> products;
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            products = session.query(Products_ByUnitsInStock.IndexEntry.class, Products_ByUnitsInStock.class)
                .whereGreaterThan("UnitsInStock" , minValue)
                .ofType(Product.class)
                .toList();
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
