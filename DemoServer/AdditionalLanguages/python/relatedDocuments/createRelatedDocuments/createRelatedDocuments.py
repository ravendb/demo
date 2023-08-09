from demo_example import RunParamsBase, Example
from models import Supplier, Category, Product

class RunParams(RunParamsBase):
    def __init__(self, supplier_name: str, supplier_phone: str, product_name: str):
        self.supplier_name = supplier_name
        self.supplier_phone = supplier_phone
        self.product_name = product_name


class CreateRelatedDocuments(Example):
    def run(self, run_params: RunParams) -> Product:
        supplier_name = run_params.supplier_name
        supplier_phone = run_params.supplier_phone
        product_name = run_params.product_name

        #region Demo
        #region Step_1
        supplier = Supplier()
        supplier.Name = supplier_name
        supplier.Phone = supplier_phone

        category = Category()
        category.Name = "NoSQL Databases"
        category.Description = "Non-relational databases"
        #endregion

        #region Step_2
        product = Product(Name=product_name)
        #endregion

        with self.document_store_holder.store().open_session() as session:
            #region Step_3
            session.store(supplier)
            session.store(category)
            #endregion

            #region Step_4
            product.Supplier = supplier.Id
            product.Category = category.Id
            #endregion

            #region Step_5
            session.store(product, "products/34-A")
            #endregion

            #region Step_6
            session.save_changes()
            #endregion

        #endregion
        return product
