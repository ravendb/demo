from pyravendb.store.document_store import documentstore


class ReplicationFailover(object):
    @staticmethod
    def replication_failover(document_id):
        with documentstore(url="http://localhost:8080", database="Rep1") as store:
            store.initialize()

            with store.open_session() as session:
                results = session.load(document_id, object_type=dict)

                return results
