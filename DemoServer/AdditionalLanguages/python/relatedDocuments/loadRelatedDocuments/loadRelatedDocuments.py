from demo_example import Example, RunParamsBase
from models import Product, Supplier

class RunParams(RunParamsBase):
    def __init__(self, price_per_unit: float, phone: str):
        self.price_per_unit = price_per_unit
        self.phone = phone

class LoadRelatedDocuments(Example):
    def run(self, run_params: RunParams) -> None:
        pricePerUnit = run_params.price_per_unit
        phone = run_params.phone

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            product = session\
                .include("Supplier")\
                .load(Product, "products/34-A")["products/34-A"]
            #endregion

            #region Step_2
            supplier = session.load(product.Supplier, Supplier)
            #endregion

            #region Step_3
            product.PricePerUnit = pricePerUnit
            supplier.Phone = phone
            #endregion

            #region Step_4
            session.save_changes()
            #endregion
        #endregion
