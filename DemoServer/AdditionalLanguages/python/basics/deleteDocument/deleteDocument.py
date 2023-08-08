from demo_example import Example, RunParamsBase

class RunParams(RunParamsBase):
    def __init__(self, document_id: str):
        self.document_id = document_id

class DeleteDocument(Example):
    def run(self, run_params: RunParams) -> None:
        document_id = run_params.document_id

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            session.delete(document_id)
            #endregion

            #region Step_2
            session.save_changes()
            #endregion
        #endregion
