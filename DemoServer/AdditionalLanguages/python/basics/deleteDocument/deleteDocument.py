from demo_example import Example, RunParamsBase

class RunParams(RunParamsBase):
    def __init__(self, document_id: str):
        self.document_id = document_id

class DeleteDocument(Example):
    def run(self, run_params: RunParams) -> None:
        documentID = run_params.document_id

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            session.delete(documentID)
            #endregion

            #region Step_2
            session.save_changes()
            #endregion
        #endregion
