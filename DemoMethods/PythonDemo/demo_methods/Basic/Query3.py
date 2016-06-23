from demo.holder import DocumentStoreHolder
from demo.entities import *


class Query3(object):
    @staticmethod
    def query3(city="Berlin"):
        with DocumentStoreHolder.get_store().open_session() as session:
            query, stats = list(session.query(object_type=Company, with_statistics=True,
                                              nested_object_types={"Address": AddressC,
                                                                   "Contact": ContactC}).
                                where_equals("Address.City", city))

            return query[0] if query else None
