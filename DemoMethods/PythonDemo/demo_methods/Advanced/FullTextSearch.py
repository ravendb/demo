from demo.holder import DocumentStoreHolder
from datetime import timedelta
from demo.entities import LastFm
from demo.indexes import LastFmAnalyzed


class FullTextSearch(object):
    @staticmethod
    def full_text_search(searchTerm="Jazz"):
        with DocumentStoreHolder.get_store().open_session() as session:
            query, stats = list(session.query(object_type=LastFm, with_statistics=True,
                                              index_name=LastFmAnalyzed.__name__).
                                search("Query", search_terms=searchTerm))

            server_time = timedelta(milliseconds=stats["DurationMilliseconds"])

            return query
