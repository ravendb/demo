from demo.holder import DocumentStoreHolder
from datetime import timedelta
from pyravendb.tools.utils import Utils


class StaticQuery(object):
    @staticmethod
    def static_query(high_price=500, delay_days=35):
        with DocumentStoreHolder.get_store().open_session() as session:
            query = list(
                session.query(index_name="CostlyOrders").
                where_greater_than("Price", high_price).
                and_also().
                where_greater_than("Delay", Utils.timedelta_tick(timedelta(days=delay_days))))

            return query[0] if query else None
