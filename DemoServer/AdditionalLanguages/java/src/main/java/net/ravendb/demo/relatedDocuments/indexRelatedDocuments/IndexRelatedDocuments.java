package net.ravendb.demo.relatedDocuments.indexRelatedDocuments;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;

import java.util.List;

public class IndexRelatedDocuments {

    //region Demo
    //region Step_1
    public static class Products_ByCategoryName extends AbstractIndexCreationTask {
    //endregion

        //region Step_2
        public static class Result {
            private String categoryName;

            public String getCategoryName() {
                return categoryName;
            }

            public void setCategoryName(String categoryName) {
                this.categoryName = categoryName;
            }
        }
        //endregion

        //region Step_3
        public Products_ByCategoryName() {
            map = "docs.products.Select(product => new { " +
                "    categoryName = (this.LoadDocument(product.category, \"Categories\")).name " +
                "})";
        }
        //endregion
    }
    //endregion

    public void run(RunParams runParams) {
        String categoryName = runParams.getCategoryName();

        //region Demo
        List<Product> productsWithCategoryName;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            productsWithCategoryName = session
                .query(Products_ByCategoryName.Result.class, Products_ByCategoryName.class)
                .whereEquals("categoryName", categoryName)
                .ofType(Product.class)
                .toList();
            //endregion
        }
        //endregion
    }

    public static class RunParams {
        private String categoryName;

        public String getCategoryName() {
            return categoryName;
        }

        public void setCategoryName(String categoryName) {
            this.categoryName = categoryName;
        }
    }
}
