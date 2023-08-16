from demo_example import Example, RunParamsBase
from models import Category

#region Usings
from ravendb import AbstractIndexCreationTask
from ravendb.documents.indexes.definitions import FieldIndexing
#endregion

class RunParams(RunParamsBase):
    def __init__(self, search_term: str = None):
        self.search_term = search_term

#region Demo
#region Step_1
class Categories_DescriptionText(AbstractIndexCreationTask):
#endregion
    def __init__(self):
        #region Step_2
        super().__init__()
        self.map = (
            "docs.Products.Select(product => new {"
            "    CategoryDescription = category.Description"
            "})"
        )        
        #endregion

        #region Step_3
        self._index("CategoryDescription", FieldIndexing.SEARCH)
        #endregion
#endregion

class FtsWithStaticIndexSingleField(Example):
    def run(self, run_params: RunParams):
        searchTerm = run_params.search_term
        Categories_DescriptionText().execute(self.document_store_holder.store())

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_4
            categories_with_search_term = list(
                session.query_index_type(Categories_DescriptionText, Category).search(
                    "CategoryDescription", searchTerm
                )
            )
            #endregion
        #endregion

        return categories_with_search_term
