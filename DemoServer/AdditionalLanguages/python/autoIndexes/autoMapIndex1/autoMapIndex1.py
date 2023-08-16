from demo_example import Example, RunParamsBase
from models import Employee

class RunParams(RunParamsBase):
    def __init__(self, first_name: str):
        self.first_name = first_name

class AutoMapIndex1(Example):
    def run(self, run_params: RunParams):
        firstName = run_params.first_name

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            find_employee_query = session.query(object_type=Employee).where_equals("FirstName", firstName)
            #endregion

            #region Step_2
            employee_result = find_employee_query.first()
            #endregion
        #endregion

        return employee_result
