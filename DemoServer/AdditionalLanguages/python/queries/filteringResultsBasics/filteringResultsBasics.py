from typing import List
from demo_example import Example
from models import Employee

class FilteringResultsBasic(Example):
    def run(self, run_params=None) -> List[Employee]:
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            filtered_query = session.query(object_type=Employee)
            #endregion
            #region Step_2
            filtered_query = filtered_query.where_equals("FirstName", "Anne")
            #endregion

            #region Step_3
            filtered_employees = list(filtered_query)
            #endregion
        #endregion

            return filtered_employees
