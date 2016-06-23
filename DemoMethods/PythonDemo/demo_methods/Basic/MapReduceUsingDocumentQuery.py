from demo.holder import DocumentStoreHolder
from demo.indexes import *


class MapReduceUsingDocumentQuery(object):
    @staticmethod
    def map_reduce_using_document_query():
        with DocumentStoreHolder.get_store().open_session() as session:
            query, stats = list(
                session.query(object_type=ProductSales.Result, index_name=ProductSales.__name__, with_statistics=True))

            return query
