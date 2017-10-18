from demo.holder import DocumentStoreHolder
from pyravendb.data.query import IndexQuery
from pyravendb.raven_operations.operations import QueryOperationOptions, PatchByQueryOperation
from demo.entities import Order


class SetBasedScripted(object):
    @staticmethod
    def set_based_scripted(employee="employees/1", discount=5):
        query = ("FROM INDEX 'Orders/Totals' "
                 "WHERE Employee = $emp "
                 "UPDATE { for(var i = 0; i < this.Lines.length; i++) { "
                 "this.Lines[i].Discount = Math.max(this.Lines[i].Discount || 0, args.discount);}}")

        query_parameters = {"emp": employee, "discount": discount}

        patch_operation = PatchByQueryOperation(
            IndexQuery(query=query, query_parameters=query_parameters),
            options=QueryOperationOptions(allow_stale=False))

        store = DocumentStoreHolder.get_store()
        result = store.operations.send(patch_operation)
        store.operations.wait_for_operation_complete(result['operation_id'])

        with DocumentStoreHolder.get_store().open_session() as session:
            results = list(
                session.query(object_type=Order,
                              wait_for_non_stale_results=True).where(Employee=employee))

            return results
