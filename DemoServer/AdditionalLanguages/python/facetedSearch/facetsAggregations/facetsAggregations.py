from models import Product
from demo_example import Example, RunParamsBase

#region Usings
from ravendb import AbstractIndexCreationTask, Facet, FacetAggregationField, RangeFacet
from ravendb.documents.queries.facets.misc import FacetAggregation
#endregion

class RunParams(RunParamsBase):
    def __init__(self, range_1: int = None, range_2: int = None, range_3: int = None):
        self.range_1 = range_1
        self.range_2 = range_2
        self.range_3 = range_3

#region Demo
#region Step_1
class Products_ByCategoryPriceAndUnits(AbstractIndexCreationTask):
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.Products.Select(product => new {"
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name,'
            "    PricePerUnit = product.PricePerUnit,"
            "    UnitsInStock = product.UnitsInStock"
            "})"
        )
#endregion
#endregion

class FacetsAggregations(Example):
    def run(self, run_params: RunParams):
        facets_results = []
        range1 = run_params.range_1 or 25.0
        range2 = run_params.range_2 or 50.0
        range3 = run_params.range_3 or 100.0
        Products_ByCategoryPriceAndUnits().execute(self.document_store_holder.store())
        #region Demo
        #region Step_2
        facet = Facet()
        facet.field_name = "CategoryName"

        price_per_unit_aggregation_field = FacetAggregationField()
        price_per_unit_aggregation_field.name = "PricePerUnit"

        units_in_stock_aggregation_field = FacetAggregationField()
        units_in_stock_aggregation_field.name = "UnitsInStock"

        facet.aggregations.update({FacetAggregation.AVERAGE: {price_per_unit_aggregation_field}})
        facet.aggregations.update({FacetAggregation.SUM: {units_in_stock_aggregation_field}})
        facet.aggregations.update({FacetAggregation.MAX: {price_per_unit_aggregation_field}})

        fields = {price_per_unit_aggregation_field, units_in_stock_aggregation_field}

        facet.aggregations.update({FacetAggregation.MIN: fields})
        #endregion

        #region Step_3
        range_facet = RangeFacet()
        range_facet.ranges = [
            f"PricePerUnit < {range1}",
            f"PricePerUnit >= {range1} and PricePerUnit < {range2}",
            f"PricePerUnit >= {range2} and PricePerUnit < {range3}",
            f"PricePerUnit > {range3}",
        ]

        range_facet.aggregations.update({FacetAggregation.AVERAGE: {price_per_unit_aggregation_field}})
        range_facet.aggregations.update({FacetAggregation.SUM: {units_in_stock_aggregation_field}})
        range_facet.aggregations.update({FacetAggregation.MAX: {price_per_unit_aggregation_field}})
        range_facet.aggregations.update({FacetAggregation.MIN: fields})
        #endregion

        #region Step_4
        facets = [facet, range_facet]
        #endregion

        with self.document_store_holder.store().open_session() as session:
            #region Step_5
            query_results = (
                session.query_index_type(Products_ByCategoryPriceAndUnits, Product)
                    .aggregate_by_facets(*facets)
                    .execute()
            )
            #endregion
        #endregion

        return query_results
