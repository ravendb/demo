from demo_example import Example, RunParamsBase
from models import Employee

class RunParams(RunParamsBase):
    def __init__(self, boost1: int = None, boost2: int = None, boost3: int = None):
        self.boost1 = boost1
        self.boost2 = boost2
        self.boost3 = boost3

class FtsQueryBoosting(Example):
    def run(self, run_params: RunParams):
        boost1 = run_params.boost1
        boost2 = run_params.boost2
        boost3 = run_params.boost3
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            search_query = session.query(object_type=Employee)
            #endregion
            
            #region Step_2
            search_query = (
                search_query.search("Notes", "ph.d.")
                .boost(boost1)
                .search("Notes", "university")
                .boost(boost2)
                .search("Notes", "college")
                .boost(boost3)
            )
            #endregion

            #region Step_3
            employees_with_matching_terms = list(search_query)
            #endregion
        #endregion
        return employees_with_matching_terms
