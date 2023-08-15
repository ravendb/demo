from __future__ import annotations
from typing import List
from demo_example import Example, RunParamsBase
from models import Product

#region Usings
from ravendb import AbstractIndexCreationTask
#endregion

class RunParams(RunParamsBase):
    def __init__(self, price: int = None):
        self.price = price

#region Demo
#region Step_1
class Products_ByPrice(AbstractIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(
            self,
            product_name: str = None,
            original_price: float = None,
            sale_price: float = None,
            profit_price: float = None,
        ):
            self.product_name = product_name
            self.original_price = original_price
            self.sale_price = sale_price
            self.profit_price = profit_price
    #endregion

    def __init__(self):
        #region Step_3
        super().__init__()
        self.map = (
            "docs.Products.Select(product => new {"
            "    product_name = product.Name,"
            "    original_price = product.PricePerUnit,"
            "    sale_price = DiscountUtils.CalcSalePrice(product.PricePerUnit),"
            "    profit_price = DiscountUtils.CalcProfitPrice(product.PricePerUnit)"
            "})"
        )
        #endregion
        
        #region Step_4
        self.additional_sources = {"DiscountLogic": ADDITIONAL_SOURCE}
        #endregion
#endregion

class AdditionalSourcesIndex(Example):
    def run(self, run_params: RunParams) -> List[DataToShow]:
        price = run_params.price or 5
        Products_ByPrice().execute(self.document_store_holder.store())

        #region Demo
        #region Step_5
        with self.document_store_holder.store().open_session() as session:
            low_cost_products = list(
                session.query_index_type(Products_ByPrice, Products_ByPrice.IndexEntry)
                .where_less_than("sale_price", price)
                .order_by("sale_price")
            )
        #endregion
        #endregion

        # Manipulate results to show because index fields are not stored..
        products_data = [
            DataToShow(
                item.Name,
                item.PricePerUnit,
                item.PricePerUnit - (item.PricePerUnit / 100 * 25),
                item.PricePerUnit + (item.PricePerUnit / 100 * 25),
                )
            for item in low_cost_products
        ]
        return products_data

#region Demo
#region Step_6
ADDITIONAL_SOURCE = (
    "public static class DiscountUtils"
    "{"
    "    public static decimal CalcSalePrice(decimal price)"
    "    {"
    "        return price - price / 100M * 25M;"
    "    }"
    "     "
    "    public static decimal CalcProfitPrice(decimal price)"
    "    {"
    "        return price + price / 100M * 25M;"
    "    }"
    "}"
)
#endregion
#endregion

class DataToShow:
    def __init__(
        self,
        product_name: str = None,
        original_price: float = None,
        sale_price: float = None,
        profit_price: float = None,
    ):
        self.product_name = product_name
        self.original_price = original_price
        self.sale_price = sale_price
        self.profit_price = profit_price
