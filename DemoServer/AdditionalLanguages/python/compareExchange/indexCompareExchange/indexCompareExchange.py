from demo_example import Example, RunParamsBase
from models import Product

#region Usings
from ravendb import ( 
    AbstractIndexCreationTask
)
#endregion

class RunParams(RunParamsBase):
    def __init__(self, min_value: int):
        self.min_value = min_value

#region Demo
#region Step_1
class Products_ByUnitsInStock(AbstractIndexCreationTask):
#endregion
    #region Step_2
    class IndexEntry:
        def __init__(self, units_in_stock: int = None):
            self.units_in_stock = units_in_stock
    #endregion

    #region Step_3
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.Products.Select(product => new { "
            "    units_in_stock = this.LoadCompareExchangeValue(Id(product)) "
            "}) "
        )
    #endregion
#endregion

class IndexCompareExchange(Example):
    def run(self, run_params: RunParams):
        minValue = run_params.min_value
        Products_ByUnitsInStock().execute(self.document_store_holder.store())

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_4
            products = list(
                session.query_index_type(Products_ByUnitsInStock, Product)
                .where_greater_than("units_in_stock", minValue)
                .of_type(Product)
            )
            #endregion
        #endregion
        
        return products
