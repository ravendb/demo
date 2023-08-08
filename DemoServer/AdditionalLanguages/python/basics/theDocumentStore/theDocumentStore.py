#region Usings
from ravendb import DocumentStore
#endregion

#region Demo
#region Step_1
class DocumentStoreHolder:
#endregion
    #region Step_2
    _store: DocumentStore = None
    #endregion

    #region Step_3
    @staticmethod
    def _create_document_store() -> DocumentStore:
    #endregion
        #region Step_4
        server_url = "http://localhost:8080"
        database_name = "YourDatabaseName"

        document_store = DocumentStore([server_url], database_name)
        #endregion

        #region Step_5
        document_store.initialize()
        #endregion
        return document_store

    #region Step_6
    @classmethod
    def store(cls) -> DocumentStore:
        if cls._store is None:
            cls._store = cls._create_document_store()

        return cls._store
    #endregion
#endregion
