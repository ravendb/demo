from demo.holder import DocumentStoreHolder
from datetime import timedelta
from demo.entities import Order
from demo.indexes import CostlyOrders
from pyravendb.tools.utils import Utils


class StaticQuery(object):
    @staticmethod
    def static_query(high_price=500, delay_days=35):
        with DocumentStoreHolder.get_store().open_session() as session:
            result = list(
                session.query(index_name=CostlyOrders.__name__, object_type=Order).
                    where_greater_than("Price", high_price).
                    and_also().
                    where_greater_than("Delay", Utils.timedelta_tick(timedelta(days=delay_days))))

            return result[0] if result else None
