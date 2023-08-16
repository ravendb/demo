from demo_example import Example, RunParamsBase
from models import Product

#region Usings
from ravendb import AbstractIndexCreationTask
from ravendb.documents.queries.facets.definitions import FacetSetup, Facet, RangeFacet
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
            '    Category = (this.LoadDocument(product.Category, "Categories")).Name,'
            "    PricePerUnit = product.PricePerUnit"
            "})"
        )
#endregion
#endregion

class FacetsFromDocument(Example):
    def run(self, run_params: RunParams):
        facets_results = []
        range1 = run_params.range_1 or 25
        range2 = run_params.range_2 or 50
        range3 = run_params.range_3 or 100

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_2
            facet_setup = FacetSetup()
            facet_setup.Id = "myFacetSetupDocumentID"

            facet = Facet()
            facet.field_name = "Category"
            facet_setup.facets = [facet]

            range_facet = RangeFacet()
            range_facet.ranges = [
                f"PricePerUnit < {range1}",
                f"PricePerUnit >= {range1} and PricePerUnit < {range2}",
                f"PricePerUnit >= {range2} and PricePerUnit < {range3}",
                f"PricePerUnit > {range3}",
            ]
            facet_setup.range_facets = [range_facet]
            #endregion

            #region Step_3
            session.store(facet_setup, facet_setup.Id)
            session.save_changes()
            #endregion

            #region Step_4
            query_results = (
                session.query_index_type(Products_ByCategoryAndPrice, Product)
                .aggregate_using("myFacetSetupDocumentID")
                .execute()
            )
            #endregion
        #endregion

        for result in query_results.values():
            facet_name = result.name
            for item in result.values:
                facet_result = MyFacetResult()
                facet_result.facet_name = facet_name  # i.e. PricePerUnit
                facet_result.facet_range = item.range_  # i.e PricePerUnit < 50
                facet_result.facet_count = item.count_

                facets_results.append(facet_result)

        return facets_results
