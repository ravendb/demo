from demo_example import Example, RunParamsBase

#region Usings
from ravendb.documents.indexes.index_creation import AbstractMultiMapIndexCreationTask
#endregion

class RunParams(RunParamsBase):
    def __init__(self, min_companies_count: int, min_items_count: int):
        self.min_companies_count = min_companies_count
        self.min_items_count = min_items_count

#region Demo
#region Step_1
class CityCommerceDetails(AbstractMultiMapIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(
            self,
            city_name: str = None,
            number_of_companies_in_city: int = None,
            number_of_suppliers_in_city: int = None,
            number_of_items_shipped_to_city: int = None,
        ):
            self.city_name = city_name
            self.number_of_companies_in_city = number_of_companies_in_city
            self.number_of_suppliers_in_city = number_of_suppliers_in_city
            self.number_of_items_shipped_to_city = number_of_items_shipped_to_city
    #endregion

    def __init__(self):
        #region Step_3
        super().__init__()
        self._add_map(
            (
                "docs.Companies.Select(company => new {"
                "    city_name = company.Address.City,"
                "    number_of_companies_in_city = 1,"
                "    number_of_suppliers_in_city = 0,"
                "    number_of_items_shipped_to_city = 0"
                "})"
            )
        )

        self._add_map(
            (
                "docs.Suppliers.Select(supplier => new {"
                "    city_name = supplier.Address.City,"
                "    number_of_companies_in_city = 0,"
                "    number_of_suppliers_in_city = 1,"
                "    number_of_items_shipped_to_city = 0"
                "})"
            )
        )

        self._add_map(
            (
                "docs.Orders.Select(order => new {"
                "    city_name = order.ShipTo.City,"
                "    number_of_companies_in_city = 0,"
                "    number_of_suppliers_in_city = 0,"
                "    number_of_items_shipped_to_city = Enumerable.Sum(order.Lines, x => ((int) x.Quantity))"
                "})"
            )
        )
        #endregion
        
        #region Step_4
        self.reduce = (
            "results.GroupBy(result => result.city_name).Select(g => new {"
            "    city_name = g.Key,"
            "    number_of_companies_in_city = Enumerable.Sum(g, x => ((int) x.number_of_companies_in_city)),"
            "    number_of_suppliers_in_city = Enumerable.Sum(g, x0 => ((int) x0.number_of_suppliers_in_city)),"
            "    number_of_items_shipped_to_city = Enumerable.Sum(g, x1 => ((int) x1.number_of_items_shipped_to_city))"
            "})"
        )
        #endregion
#endregion

class MultiMapReduceIndex(Example):
    def run(self, run_params: RunParams):
        minCompaniesCount = run_params.min_companies_count
        minItemsCount = run_params.min_items_count
        CityCommerceDetails().execute(self.document_store_holder.store())
        #region Demo
        #region Step_5
        with self.document_store_holder.store().open_session() as session:
            commerce_details = list(
                session.query_index_type(CityCommerceDetails, CityCommerceDetails.IndexEntry)
                .where_greater_than("number_of_companies_in_city", minCompaniesCount)
                .or_else()
                .where_greater_than("number_of_items_shipped_to_city", minItemsCount)
                .order_by("city_name")
            )
        #endregion
        #endregion

        return commerce_details
