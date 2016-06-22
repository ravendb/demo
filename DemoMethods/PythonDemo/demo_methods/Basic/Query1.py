from demo.holder import DocumentStoreHolder
from demo.entities import *


class Query1(object):
    @staticmethod
    def query1(country="UK"):
        with DocumentStoreHolder.get_store().open_session() as session:
            query, stats = list(session.query(object_type=Company, with_statistics=True,
                                              nested_object_types={"Address": AddressC, "Contact": ContactC}).where(
                **{"Address.Country": country}))

            return query[0] if query else None
