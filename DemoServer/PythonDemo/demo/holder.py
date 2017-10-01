from pyravendb.store.document_store import DocumentStore

store = None


class DocumentStoreHolder(object):
    @staticmethod
    def get_store():
        global store
        if not store:
            store = DocumentStore(urls=["http://localhost.fiddler:8080"], database="Demo")
            store.initialize()

        return store
