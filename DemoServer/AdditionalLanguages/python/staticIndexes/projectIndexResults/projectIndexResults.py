from demo_example import Example, RunParamsBase

#region Usings
from ravendb import (
    AbstractIndexCreationTask,
    QueryData
)
#endregion

#region Demo
#region Step_1
class Employees_ByWorkPeriod(AbstractIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(self, working_in_company_since: int = None):
            self.working_in_company_since = working_in_company_since
    #endregion

    #region Step_3
    class EmployeeProjectedDetails:
        def __init__(self, first_name: str = None, phone: str = None, location: str = None):
            self.first_name = first_name
            self.phone = phone
            self.location = location
    #endregion

    #region Step_4
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.Employees.Select(employee => new { "
            "    working_in_company_since = employee.HiredAt.Year "
            "})"
        )
    #endregion
#endregion

class RunParams(RunParamsBase):
    def __init__(self, start_year: int = None):
        self.start_year = start_year

class ProjectIndexResults(Example):
    def run(self, run_params: RunParams):
        startYear = run_params.start_year or 1993
        Employees_ByWorkPeriod().execute(self.document_store_holder.store())
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_5
            employees_query = session.query_index_type(
                Employees_ByWorkPeriod, Employees_ByWorkPeriod.IndexEntry
            ).where_greater_than("working_in_company_since", startYear)
            #endregion
            
            #region Step_6
            employees_query = employees_query.select_fields_query_data(
                Employees_ByWorkPeriod.EmployeeProjectedDetails,
                QueryData.custom_function(
                    "employee ",
                    (
                        "{ first_name: employee.FirstName, "
                        " phone: employee.HomePhone, "
                        " location: employee.Address.City + ' ' + employee.Address.Country } "
                    ),
                ),
            )
            #endregion

            #region Step_7
            employees_since_year = list(employees_query)
            #endregion
        #endregion
        return employees_since_year
