package net.ravendb.demo.textSearch.fTSWithStaticIndexSingleField;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Category;

import java.util.List;

public class FTSWithStaticIndexSingleField {

    //region Demo
    //region Step_1
    public static class Categories_DescriptionText extends AbstractIndexCreationTask {
    //endregion
        //region Step_2
        public static class Result {
            private String categoryDescription;

            public String getCategoryDescription() {
                return categoryDescription;
            }

            public void setCategoryDescription(String categoryDescription) {
                this.categoryDescription = categoryDescription;
            }
        }
        //endregion

        public Categories_DescriptionText() {
            //region Step_3
            map = "docs.Categories.Select(category => new { " +
                "    CategoryDescription = category.Description " +
                "})";
            //endregion

            //region Step_4
            index("CategoryDescription", FieldIndexing.SEARCH);
            //endregion
        }
    }
    //endregion

    public void run(RunParams runParams) {
        String searchTerm = runParams.getSearchTerm();

        //region Demo
        List<Category> categoriesWithSearchTerm;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_5
            categoriesWithSearchTerm = session.query(Categories_DescriptionText.Result.class, Categories_DescriptionText.class)
                .whereEquals("CategoryDescription", searchTerm)
                .ofType(Category.class)
                .toList();
            //endregion
        }
        //endregion
    }

    public static class RunParams {
        private String searchTerm;

        public String getSearchTerm() {
            return searchTerm;
        }

        public void setSearchTerm(String searchTerm) {
            this.searchTerm = searchTerm;
        }
    }
}
