from typing import List
from demo_example import Example, RunParamsBase
from models import Order

#region Usings
from ravendb import AbstractIndexCreationTask
#endregion

class RunParams(RunParamsBase):
    def __init__(self, name_prefix: str = None):
        self.name_prefix = name_prefix

#region Demo
#region Step_1
class Orders_ByProductDetails(AbstractIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(self, product_id: str, product_name: str):
            self.product_id = product_id
            self.product_name = product_name
    #endregion

    #region Step_3
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.Orders.SelectMany(order => order.Lines, (order, orderLine) => new {"
            "    product_id = orderLine.Product,"
            "    product_name = orderLine.ProductName"
            "})"
        )
    #endregion
#endregion

class FanoutIndex(Example):
    def run(self, run_params: RunParams) -> List[Order]:
        namePrefix = run_params.name_prefix or "Chocolade"
        Orders_ByProductDetails().execute(self.document_store_holder.store())
        #region Demo
        #region Step_4
        with self.document_store_holder.store().open_session() as session:
            orders = list(
                session.query_index_type(Orders_ByProductDetails, Orders_ByProductDetails.IndexEntry)
                    .where_starts_with("product_name", namePrefix)
            )
        #endregion
        #endregion

        return orders
