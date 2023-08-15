#region Usings
from ravendb import AbstractIndexCreationTask
#endregion

from demo_example import Example, RunParamsBase
from models import Product
from typing import List

class RunParams(RunParamsBase):
    def __init__(self, category_name: str):
        self.category_name = category_name

#region Demo
#region Step_1
class Products_ByCategoryName(AbstractIndexCreationTask):
#endregion

    #region Step_2
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.products.Select(product => new { "
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name'
            "})"
        )
    #endregion
#endregion

def run(self, run_params: RunParams) -> List[Product]:
    categoryName = run_params.category_name
    Products_ByCategoryName().execute(self.document_store_holder.store())
    #region Demo
    products_with_category_name: List[Product] = []

    with self.document_store_holder.store().open_session() as session:
        #region Step_3
        products_with_category_name = list(
            session.query_index_type(Products_ByCategoryName, Product).where_equals(
                "CategoryName", categoryName
            )
        )
        #endregion
    #endregion
    
    return products_with_category_name
