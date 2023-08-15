from demo_example import Example, RunParamsBase

# region Usings
from ravendb import AbstractIndexCreationTask
#endregion

class RunParams(RunParamsBase):
    def __init__(self, country: str):
        self.country = country

#region Demo
#region Step_1
class Employees_ByCountry(AbstractIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(self, country: str, country_count: int):
            self.Country = country
            self.CountryCount = country_count
    #endregion

    def __init__(self):
        #region Step_3
        super().__init__()
        self.map = (
            "docs.Employees.Select(employee => new { "
            "    Country = employee.Address.Country, "
            "    CountryCount = 1 "
            "})"
        )
        #endregion
        #region Step_4
        self.reduce = (
            "results.GroupBy(result => result.Country).Select(g => new { "
            "    Country = g.Key, "
            "    CountryCount = Enumerable.Sum(g, x => x.CountryCount) "
            "})"
        )
        #endregion
#endregion

class MapReduceIndex(Example):
    def run(self, run_params: RunParams) -> int:
        country = run_params.country
        Employees_ByCountry().execute(self.document_store_holder.store())
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_5
            query_result = (
                session.query_index_type(Employees_ByCountry, Employees_ByCountry.IndexEntry)
                .where_equals("Country", country)
                .single()
            )
            number_of_employees_in_country = query_result.CountryCount if query_result is not None else 0
            #endregion
        #endregion

        return number_of_employees_in_country
