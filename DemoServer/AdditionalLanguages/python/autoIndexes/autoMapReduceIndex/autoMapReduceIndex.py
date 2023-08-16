from typing import List
from demo_example import Example
from models import Employee

#region Demo
#region Step_1
class CountryDetails:
    def __init__(self, country: str = None, number_of_employees: int = None):
        self.country = country
        self.number_of_employees = number_of_employees
#endregion
#endregion

class AutoMapReduceIndex(Example):
    def run(self, run_params=None) -> List[CountryDetails]:
        #region Demo
        with self.document_store_holder.store().open_session() as session:            
            #region Step_2
            my_query = session.query(object_type=Employee)
            #endregion
            
            #region Step_3
            my_query = my_query.group_by("Address.Country")
            #endregion
            #region Step_4
            my_query = my_query.select_key("Address.Country", "country").select_count("number_of_employees")
            #endregion
            #region Step_5
            my_query = my_query.order_by_descending("number_of_employees")
            #endregion
            #region Step_6
            my_query = my_query.of_type(CountryDetails)
            #endregion
            
            #region Step_7
            number_of_employees_per_country = list(my_query)
            #endregion
        #endregion
        
        return number_of_employees_per_country
