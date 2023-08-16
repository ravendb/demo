from demo_example import Example, RunParamsBase
from models import Employee

class RunParams(RunParamsBase):
    def __init__(self, country: str):
        self.country = country

class AutoMapIndex2(Example):
    def run(self, run_params: RunParams):
        country = run_params.country

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            find_employee_query = (
                session.query(object_type=Employee)
                .where_equals("Address.Country", country)
                .where_starts_with("Title", "Sales")
            )
            #endregion

            #region Step_2
            employee_result = find_employee_query.first()
            #endregion
        #endregion

        return employee_result
