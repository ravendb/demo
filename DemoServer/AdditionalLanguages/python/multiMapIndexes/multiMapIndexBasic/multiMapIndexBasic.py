from typing import List
from ravendb.documents.indexes.definitions import FieldIndexing
from demo_example import Example, RunParamsBase

#region Usings
from ravendb.documents.indexes.index_creation import AbstractMultiMapIndexCreationTask
#endregion

#region Demo
#region Step_1
class CompaniesAndSuppliers_ByName(AbstractMultiMapIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(self, name: str = None):
            self.name = name
    #endregion

    #region Step_3
    def __init__(self):
        super().__init__()
        self._add_map("docs.Companies.Select(company => new { name = company.Name })")
        self._add_map("docs.Suppliers.Select(supplier => new { name = supplier.Name })")
        self._index("name", FieldIndexing.EXACT)
    #endregion
#endregion

class RunParams(RunParamsBase):
    def __init__(self, name_prefix: str = None):
        self.name_prefix = name_prefix

class MultiMapIndexBasic(Example):
    def run(self, run_params: RunParams) -> List[CompaniesAndSuppliers_ByName.IndexEntry]:
        namePrefix = run_params.name_prefix or "A"
        CompaniesAndSuppliers_ByName().execute(self.document_store_holder.store())
        #region Demo
        
        #region Step_4
        with self.document_store_holder.store().open_session() as session:
            companies_and_supplier_names = list(
                session.query_index_type(
                    CompaniesAndSuppliers_ByName,
                    CompaniesAndSuppliers_ByName.IndexEntry,
                ).where_starts_with("name", namePrefix)
            )
        #endregion
        #endregion

        return companies_and_supplier_names
