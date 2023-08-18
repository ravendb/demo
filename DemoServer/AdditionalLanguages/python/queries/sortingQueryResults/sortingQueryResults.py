from demo_example import Example
from models import Product

#region Usings
from ravendb import OrderingType
#endregion

class RunParams:
    def __init__(self, number_of_units: float):
        self.number_of_units = number_of_units


class SortingQueryResults(Example):
    def run(self, run_params: RunParams):
        numberOfUnits = run_params.number_of_units
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            
            #region Step_1
            sorted_query = session.query(object_type=Product)
            #endregion
            
            #region Step_2
            sorted_query = sorted_query.where_greater_than("UnitsInStock", numberOfUnits)
            #endregion
            #region Step_3
            sorted_query.order_by_descending("UnitsInStock")
            #endregion
            #region Step_4
            sorted_query.order_by("Name", OrderingType.ALPHA_NUMERIC)
            #endregion
            
            #region Step_5
            sorted_products = list(sorted_query)
            #endregion
        #endregion
        return sorted_products
