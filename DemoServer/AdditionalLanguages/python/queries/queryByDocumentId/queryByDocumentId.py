from demo_example import Example, RunParamsBase
from models import Employee

class RunParams(RunParamsBase):
    def __init__(self, employee_document_id: str):
        self.employee_document_id = employee_document_id

class QueryByDocumentId(Example):
    def run(self, run_params: RunParams) -> Employee:
        employeeDocumentID = run_params.employee_document_id
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            
            #region Step_1
            query_by_document_id = session.query(object_type=Employee)
            #endregion
            #region Step_2
            query_by_document_id.where_equals("Id", employeeDocumentID)
            #endregion

            #region Step_3
            employee = query_by_document_id.first()
            #endregion
        #endregion

        return employee
