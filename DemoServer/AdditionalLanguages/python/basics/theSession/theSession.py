from Basics.the_document_store import DocumentStoreHolder
from demo_example import Example, RunParamsBase

#region Usings
from ravendb import DocumentSession
#endregion


class TheSession(Example):
    def run(self, run_params: RunParamsBase = None):
        #region Demo
        #region Step_1
        session = DocumentStoreHolder.store().open_session("YourDatabaseName")
        #endregion

        #region Step_2
        # Run your business logic:
        #
        # Store documents
        # Load and Modify documents
        # Query indexes & collections
        # .... etc.
        #endregion

        #region Step_3
        session.save_changes()
        #endregion
        #endregion
