from demo.holder import DocumentStoreHolder
from demo.entities import *


class Query2(object):
    @staticmethod
    def query2(externalId="ALFKI"):
        with DocumentStoreHolder.get_store().open_session() as session:
            result, stats = list(session.query(object_type=Company,
                                               with_statistics=True).
                                 where(ExternalId=externalId))

            return result[0] if result else None
