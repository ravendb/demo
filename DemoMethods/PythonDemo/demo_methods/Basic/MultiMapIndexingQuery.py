from demo.holder import DocumentStoreHolder


class MultiMapIndexingQuery(object):
    @staticmethod
    def multi_map_indexing_query(country="USA"):
        with DocumentStoreHolder.get_store().open_session() as session:
            return list(session.query(index_name="NameAndCountry", wait_for_non_stale_results=True).
                        search("Country", country).select("Name", "Id"))
