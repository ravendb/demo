from demo.holder import DocumentStoreHolder
from demo.entities import *


class Query1(object):
    @staticmethod
    def query1(country="UK"):
        with DocumentStoreHolder.get_store().open_session() as session:
            result, stats = list(session.query(object_type=Company,
                                               with_statistics=True).
                                 where(**{"Address.Country": country}))

            return result[0] if result else None
