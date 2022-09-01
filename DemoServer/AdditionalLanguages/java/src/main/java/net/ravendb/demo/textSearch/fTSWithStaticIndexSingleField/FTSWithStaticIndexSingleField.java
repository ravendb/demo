package net.ravendb.demo.textSearch.fTSWithStaticIndexSingleField;

//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Category;

public class FTSWithStaticIndexSingleField {

    //region Demo
    //region Step_1
    public static class Categories_DescriptionText extends AbstractIndexCreationTask {
    //endregion

        public Categories_DescriptionText() {
            //region Step_2
            map = "docs.Categories.Select(category => new { " +
                "    CategoryDescription = category.Description " +
                "})";
            //endregion

            //region Step_3
            index("CategoryDescription", FieldIndexing.SEARCH);
            //endregion
        }
    }
    //endregion

    public List<Category> run(RunParams runParams) {
        String searchTerm = runParams.getSearchTerm();

        //region Demo
        List<Category> categoriesWithSearchTerm;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            categoriesWithSearchTerm = session.query(Category.class, Categories_DescriptionText.class)
                .whereEquals("CategoryDescription", searchTerm)
                .toList();
            //endregion
        }
        //endregion

        return categoriesWithSearchTerm;
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
