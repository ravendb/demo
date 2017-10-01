from demo.holder import DocumentStoreHolder
from demo.indexes import NameAndCountry


class MultiMapIndexingQuery(object):
    @staticmethod
    def multi_map_indexing_query(country="USA"):
        with DocumentStoreHolder.get_store().open_session() as session:
            return [{"Id": result.Id, "Name": result.Name} for result in
                    list(session.query(index_name=NameAndCountry.__name__, object_type=NameAndCountry.Result,
                                       wait_for_non_stale_results=True).
                         search("Country", country).
                         select("Name", "Id"))]
