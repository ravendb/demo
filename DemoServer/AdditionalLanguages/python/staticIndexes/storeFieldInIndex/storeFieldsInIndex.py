import datetime
from demo_example import Example, RunParamsBase

#region usings
from ravendb import AbstractIndexCreationTask
from ravendb.documents.indexes.definitions import FieldStorage, FieldIndexing
#endregion

class RunParams(RunParamsBase):
    def __init__(self, company_id: str):
        self.company_id = company_id

#region Demo
#region Step_1
class OrdersQuantity_ByCompany(AbstractIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(self, company: str = None, total_items_ordered: int = None):
            self.company = company
            self.total_items_ordered = total_items_ordered
    #endregion

    #region Step_3
    class OrderProjectedDetails:
        def __init__(self, ordered_at: datetime.datetime, total_items_ordered: int):
            self.ordered_at = ordered_at
            self.total_items_ordered = total_items_ordered
    #endregion

    def __init__(self):
        #region Step_4
        super().__init__()
        self.map = (
            "docs.Orders.Select(order => new { "
            "   company = order.Company, "
            "   total_items_ordered = Enumerable.Sum(order.Lines, orderLine => ((int) orderLine.Quantity)) "
            "})"
        )
        #endregion
        
        #region Step_5
        self._store("total_items_ordered", FieldStorage.YES)
        self._index("Company", FieldIndexing.EXACT)
        #endregion
#endregion

class StoreFieldsInIndex(Example):
    def run(self, run_params: RunParams):
        companyID = run_params.company_id
        OrdersQuantity_ByCompany().execute(self.document_store_holder.store())
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_6
            orders_query = session.query_index_type(
                OrdersQuantity_ByCompany, OrdersQuantity_ByCompany.IndexEntry
            ).where_equals("Company", companyID)
            #endregion
            
            #region Step_7
            orders_query = orders_query.select_fields(OrdersQuantity_ByCompany.OrderProjectedDetails)
            #endregion
            #region Step_8
            orders_details = list(orders_query)
            #endregion
        #endregion
        return orders_details
