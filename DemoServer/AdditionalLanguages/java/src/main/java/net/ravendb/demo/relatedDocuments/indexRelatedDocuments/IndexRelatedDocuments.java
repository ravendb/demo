package net.ravendb.demo.relatedDocuments.indexRelatedDocuments;

//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;

public class IndexRelatedDocuments {

    //region Demo
    //region Step_1
    public static class Products_ByCategoryName extends AbstractIndexCreationTask {
    //endregion

        //region Step_2
        public Products_ByCategoryName() {
            map = "docs.products.Select(product => new { " +
                "    categoryName = (this.LoadDocument(product.Category, \"Categories\")).Name " +
                "})";
        }
        //endregion
    }
    //endregion

    public List<Product> run(RunParams runParams) {
        String categoryName = runParams.getCategoryName();

        //region Demo
        List<Product> productsWithCategoryName;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_3
            productsWithCategoryName = session
                .query(Product.class, Products_ByCategoryName.class)
                .whereEquals("categoryName", categoryName)
                .toList();
            //endregion
        }
        //endregion

        return productsWithCategoryName;
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
