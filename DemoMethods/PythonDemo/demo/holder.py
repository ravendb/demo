from pyravendb.store.document_store import documentstore

store = None


class DocumentStoreHolder(object):
    @staticmethod
    def get_store():
        global store
        if not store:
            store = documentstore(url="http://localhost.fiddler:8080", database="demoDB")
            store.initialize()

        return store
