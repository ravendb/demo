from typing import List
from demo_example import Example, RunParamsBase
from models import Employee

class RunParams(RunParamsBase):
    def __init__(self, country: str):
        self.country = country

class FilteringResultsMultipleConditions(Example):
    def run(self, run_params: RunParams) -> List[Employee]:
        country = run_params.country
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            
            #region Step_1
            filtered_query = session.query(object_type=Employee)
            #endregion
            #region Step_2
            filtered_query = (
                filtered_query.where_in("FirstName", ["Anne", "John"])
                .or_else()
                .open_subclause()
                .where_equals("Address.Country", country)
                .where_greater_than("Territories.Count", 2)
                .where_starts_with("Title", "Sales")
                .close_subclause()
            )
            #endregion

            #region Step_3
            filtered_employees = list(filtered_query)
            #endregion
        #endregion

        return filtered_employees
