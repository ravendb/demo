from demo_example import Example, RunParamsBase
from models import Order, Product

class QueryRelatedDocuments(Example):
    def run(self, run_params: RunParamsBase = None) -> None:
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            shipped_orders = list(session.query(object_type = Order)\
                                  .include("Lines.Product")\
                                  .where_exists("ShippedAt"))
            #endregion

            #region Step_2
            for shipped_order in shipped_orders:
                product_ids = [x.Product for x in shipped_order.Lines]
                #endregion

                for i in range(len(product_ids)):
                    #region Step_3
                    product = session.load(product_ids[i], Product)
                    product.UnitsOnOrder = product.UnitsOnOrder + shipped_order.Lines[i].Quantity
                    #endregion

            #region Step_4
            session.save_changes()
            #endregion
        #endregion
