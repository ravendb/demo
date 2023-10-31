from demo_example import RunParamsBase, Example
from models import LastFm

#region Usings
from ravendb import SearchOperator
#endregion

class RunParams(RunParamsBase):
    def __init__(
        self,
        start: str = None,
        end: str = None,
        middle: str = None,
        number_of_results: int = None,
    ):
        self.start = start
        self.end = end
        self.middle = middle
        self.number_of_results = number_of_results

class FtsQueryWildcards(Example):
    def run(self, run_params: RunParams):
        start = run_params.start
        end = run_params.end
        middle = run_params.middle
        numberOfResults = run_params.number_of_results

        #region Demo
        with (self.document_store_holder.store().open_session() as session):
            
            #region Step_1
            last_fm_query = session.query(object_type=LastFm)
            #endregion
            
            #region Step_2
            last_fm_query = last_fm_query.search("Artist", f"{start}* *{end}", SearchOperator.AND)
            #endregion
            #region Step_3
            last_fm_query = last_fm_query.search("Title", f"*{middle}*")
            #endregion
            
            #region Step_4
            last_fm_query = last_fm_query.take(numberOfResults)\
                                         .order_by("Artist")
            #endregion
            
            #region Step_5
            songs_with_matching_terms = list(last_fm_query)
            #endregion
        #endregion

        return songs_with_matching_terms
