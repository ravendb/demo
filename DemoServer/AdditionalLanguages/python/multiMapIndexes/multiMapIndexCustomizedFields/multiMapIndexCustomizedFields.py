from demo_example import Example, RunParamsBase

#region Usings
from ravendb.documents.indexes.index_creation import AbstractMultiMapIndexCreationTask
from ravendb.documents.indexes.definitions import FieldStorage
#endregion

class RunParams(RunParamsBase):
    def __init__(self, name_prefix: str = None, title_prefix: str = None):
        self.name_prefix = name_prefix
        self.title_prefix = title_prefix

#region Demo
#region Step_1
class Contacts_ByNameAndTitle(AbstractMultiMapIndexCreationTask):
#endregion
    #region Step_2
    class IndexEntry:
        def __init__(
            self,
            contact_name: str = None,
            contact_title: str = None,
            collection: object = None,
        ):
            self.contact_name = contact_name
            self.contact_title = contact_title
            self.collection = collection
    #endregion

    #region Step_3
    class ProjectedEntry(IndexEntry):
        def __init__(
            self,
            contact_name: str = None,
            contact_title: str = None,
            collection: object = None,
            phone: str = None,
        ):
            super().__init__(contact_name, contact_title, collection)
            self.phone = phone
    #endregion

    def __init__(self):
        #region Step_4
        super().__init__()
        self._add_map(
            (
                "docs.Employees.Select(employee => new {"
                '    contact_name = (employee.FirstName + " ") + employee.LastName,'
                "    contact_title = employee.Title,"
                '    collection = this.MetadataFor(employee)["@collection"]'
                "})"
            )
        )

        self._add_map(
            (
                "docs.Companies.Select(company => new {"
                "    contact_name = company.Contact.Name,"
                "    contact_title = company.Contact.Title,"
                '    collection = this.MetadataFor(company)["@collection"]'
                "})"
            )
        )

        self._add_map(
            (
                "docs.Suppliers.Select(supplier => new {"
                "    contact_name = supplier.Contact.Name,"
                "    contact_title = supplier.Contact.Title,"
                '    collection = this.MetadataFor(supplier)["@collection"]'
                "})"
            )
        )
        #endregion
        #region Step_5
        self._store("contact_name", FieldStorage.YES)
        self._store("contact_title", FieldStorage.YES)
        self._store("collection", FieldStorage.YES)
        #endregion
#endregion

class MultiMapReduceIndexCustomizedFields(Example):
    def run(self, run_params: RunParams):
        namePrefix = run_params.name_prefix or "Michael"
        titlePrefix = run_params.title_prefix or "Sales"
        Contacts_ByNameAndTitle().execute(self.document_store_holder.store())
        #region Demo        
        #region Step_6
        with self.document_store_holder.store().open_session() as session:
            contacts = list(
                session.query_index_type(Contacts_ByNameAndTitle, Contacts_ByNameAndTitle.IndexEntry)
                .where_starts_with("contact_name", namePrefix)
                .where_starts_with("contact_title", titlePrefix)
                .select_fields(Contacts_ByNameAndTitle.ProjectedEntry)
            )
        #endregion
        #endregion

        return contacts
