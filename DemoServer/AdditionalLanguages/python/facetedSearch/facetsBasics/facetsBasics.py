from demo_example import Example, RunParamsBase
from models import Product

#region Usings
from ravendb import AbstractIndexCreationTask, Facet, RangeFacet
#endregion

class RunParams(RunParamsBase):
    def __init__(self, range_1: int = None, range_2: int = None, range_3: int = None):
        self.range_1 = range_1
        self.range_2 = range_2
        self.range_3 = range_3

class MyFacetResult:
    def __init__(self, facet_name: str = None, facet_range: str = None, facet_count: float = None):
        self.facet_name = facet_name
        self.facet_range = facet_range
        self.facet_count = facet_count

#region Demo
#region Step_1
class Products_ByCategoryAndPrice(AbstractIndexCreationTask):
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.Products.Select(product => new {"
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name,'
            "    PricePerUnit = product.PricePerUnit"
            "})"
        )
#endregion
#endregion

class FacetsBasics(Example):
    def run(self, run_params: RunParams):
        facets_results = []
        range1 = run_params.range_1 or 25
        range2 = run_params.range_2 or 50
        range3 = run_params.range_3 or 100
        Products_ByCategoryAndPrice().execute(self.document_store_holder.store())
        #region Demo
        #region Step_2
        category_name_facet = Facet()
        category_name_facet.field_name = "CategoryName"
        category_name_facet.display_field_name = "Product Category"
        #endregion

        #region Step_3
        range_facet = RangeFacet()
        range_facet.ranges = [
            f"PricePerUnit < {range1}",
            f"PricePerUnit >= {range1} and PricePerUnit < {range2}",
            f"PricePerUnit >= {range2} and PricePerUnit < {range3}",
            f"PricePerUnit > {range3}",
        ]
        range_facet.display_field_name = "Price per Unit"
        #endregion

        #region Step_4
        facets = [category_name_facet, range_facet]
        #endregion

        with self.document_store_holder.store().open_session() as session:
            #region Step_5
            query = session.query_index_type(Products_ByCategoryAndPrice, Product)
            #endregion
            #region Step_6
            query = query.aggregate_by_facets(*facets)
            #endregion
            #region Step_7
            query_results = query.execute()
            #endregion
        #endregion

        for result in query_results.values():
            facets_results.extend([MyFacetResult(result.name, item.range_, item.count_) for item in result.values])

        return facets_results
