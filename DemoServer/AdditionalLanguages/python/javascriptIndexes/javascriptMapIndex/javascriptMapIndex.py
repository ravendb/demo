from typing import List
from demo_example import Example, RunParamsBase
from models import Employee

#region Usings
from ravendb.documents.indexes.index_creation import AbstractJavaScriptIndexCreationTask
#endregion

#region Demo
#region Step_1
class Employees_ByImportantDetailsJS(AbstractJavaScriptIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(
            self,
            full_name: str = None,
            country: str = None,
            working_in_company_since: int = None,
            number_of_territories: int = None,
        ):
            self.full_name = full_name
            self.country = country
            self.working_in_company_since = working_in_company_since
            self.number_of_territories = number_of_territories
    #endregion

    #region Step_3
    def __init__(self):
        super().__init__()
        self.maps = {
            (
                "map('Employees', function (employee) {"
                "   return {"
                "       full_name: employee.FirstName + ' ' + employee.LastName,"
                "       country: employee.Address.Country,"
                "       working_in_company_since: new Date(employee.HiredAt).getFullYear(),"
                "       number_of_territories: employee.Territories.length"
                "   };"
                "})"
            )
        }
    #endregion
#endregion

class RunParams(RunParamsBase):
    def __init__(self, start_year: int = None):
        self.start_year = start_year

class JavaScriptMapIndex(Example):
    def run(self, run_params: RunParams) -> List[Employee]:
        startYear = run_params.start_year
        Employees_ByImportantDetailsJS().execute(self.document_store_holder.store())

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_4
            employees_from_USA = list(
                session.query_index_type(
                    Employees_ByImportantDetailsJS,
                    Employees_ByImportantDetailsJS.IndexEntry,
                )
                .where_equals("country", "USA")
                .where_greater_than("working_in_company_since", startYear)
                .select_fields(Employee)
            )
            #endregion
        #endregion
        
        return employees_from_USA
