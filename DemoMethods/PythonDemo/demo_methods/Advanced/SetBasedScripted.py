from pyravendb.data.patches import ScriptedPatchRequest
from demo.holder import DocumentStoreHolder
from pyravendb.data.indexes import IndexQuery
from demo.entities import Company


class SetBasedScripted(object):
    @staticmethod
    def set_based_scripted(original="USA", newVal="United States of America"):
        index_query = IndexQuery(query="Address_Country:{0}".format(original))
        scripted_patch = ScriptedPatchRequest(script="this.Address.Country = newVal;", values={"newVal": newVal})

        DocumentStoreHolder.get_store().database_commands.update_by_index(index_name="CompaniesAndCountry",
                                                                          query=index_query,
                                                                          scripted_patch=scripted_patch)

        with DocumentStoreHolder.get_store().open_session() as session:
            results = list(
                session.query(object_type=Company, wait_for_non_stale_results=True).where(Address_Country=newVal))

            return results
