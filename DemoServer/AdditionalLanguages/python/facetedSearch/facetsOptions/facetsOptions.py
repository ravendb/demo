from demo_example import Example, RunParamsBase
from models import Product

#region Usings
from ravendb import AbstractIndexCreationTask, Facet
from ravendb.documents.queries.facets.misc import FacetOptions, FacetTermSortMode
#endregion

class RunParams(RunParamsBase):
    def __init__(
        self,
        start: int = None,
        page_size: int = None,
        include_remaining_terms: str = None,
    ):
        self.start = start
        self.page_size = page_size
        self.include_remaining_terms = include_remaining_terms


#region Demo
#region Step_1
class Products_ByCategoryAndSupplier(AbstractIndexCreationTask):
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.Products.Select(product => new {"
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name,'
            "    Supplier = product.Supplier"
            "})"
        )
#endregion
#endregion

class FacetsOptions(Example):
    def run(self, run_params: RunParams):
        start = run_params.start or 3
        pageSize = run_params.page_size or 2
        include = run_params.include_remaining_terms
        includeRemainingTerms = include is None or include == "true"
        
        Products_ByCategoryAndSupplier().execute(self.document_store_holder.store())
        #region Demo
        #region Step_2
        facet1 = Facet()
        facet1.field_name = "CategoryName"

        facet1_options = FacetOptions()
        facet1_options.start = start
        facet1_options.page_size = pageSize
        facet1_options.include_remaining_terms = includeRemainingTerms
        facet1_options.term_sort_mode = FacetTermSortMode.COUNT_DESC
        
        facet1.options = facet1_options
        #endregion

        facet2 = Facet()
        facet2.field_name = "Supplier"

        facet2_options = FacetOptions()
        facet2_options.start = start
        facet2_options.page_size = pageSize
        facet2_options.include_remaining_terms = includeRemainingTerms
        facet2_options.term_sort_mode = FacetTermSortMode.VALUE_ASC

        facet2.options = facet2_options
        
        #region Step_3
        facets = [facet1, facet2]
        #endregion

        with self.document_store_holder.store().open_session() as session:
            #region Step_4
            query_results = (
                session.query_index_type(Products_ByCategoryAndSupplier, Product)
                    .aggregate_by_facets(*facets)
                    .execute()
            )
            #endregion
        #endregion

        return query_results
