from demo_example import Example, RunParamsBase
from models import Employee

#region Usings
from ravendb import SearchOperator
#endregion

class RunParams(RunParamsBase):
    def __init__(self, term1: str = None, term2: str = None, term3: str = None):
        self.term1 = term1
        self.term2 = term2
        self.term3 = term3

class FtsQueryOperators(Example):
    def run(self, run_params: RunParams):
        term1 = run_params.term1
        term2 = run_params.term2
        term3 = run_params.term3

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            
            #region Step_1
            my_query = session.query(object_type=Employee)
            #endregion
            #region Step_2
            my_query = my_query.search("Notes", f"{term1} {term2}", SearchOperator.AND)
            #endregion
            #region Step_3
            my_query = my_query.search("Notes", term3)
            #endregion
            
            #region Step_4
            employees_with_matching_terms = list(my_query)
            #endregion
        #endregion
        
        return employees_with_matching_terms
