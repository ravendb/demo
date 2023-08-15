from demo_example import Example, RunParamsBase
from models import Employee

#region Usings
from ravendb import AbstractIndexCreationTask
#endregion

class RunParams(RunParamsBase):
    def __init__(self, start_year: int):
        self.start_year = start_year

#region Demo
#region Step_1
class Employees_ImportantDetails(AbstractIndexCreationTask):
#endregion

    #region Step_2
    def __init__(self):
        super().__init__()
        self.map = (
            "docs.Employees.Select(employee => new { "
            '    FullName = (employee.FirstName + " ") + employee.LastName, '
            "    Country = employee.Address.Country, "
            "    WorkingInCompanySince = employee.HiredAt.Year, "
            "    NumberOfTerritories = employee.Territories.Count "
            "})"
        )
    #endregion
#endregion

class MapIndex(Example):
    def run(self, run_params: RunParams):
        startYear = run_params.start_year
        Employees_ImportantDetails().execute(self.document_store_holder.store())
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_3
            employees_from_USA = list(
                session.query_index_type(Employees_ImportantDetails, Employee)
                .where_equals("Country", "USA")
                .where_greater_than("WorkingInCompanySince", startYear)
            )
            #endregion
        #endregion

        return employees_from_USA
