from typing import Optional
from demo_example import Example, RunParamsBase
from models import Company

#region Usings
from ravendb import QueryStatistics
#endregion

class RunParams(RunParamsBase):
    def __init__(self, results_to_skip: int, results_to_take: int):
        self.results_to_skip = results_to_skip
        self.results_to_take = results_to_take

class PagingQueryResults(Example):
    def run(self, run_params: RunParams):
        resultsToSkip = run_params.results_to_skip
        resultsToTake = run_params.results_to_take

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            stats: Optional[QueryStatistics] = None

            def __stats_callback(statistics: QueryStatistics):
                nonlocal stats
                stats = statistics

            #region Step_1
            paged_query = session.query(object_type=Company)
            #endregion
            
            #region Step_2
            paged_query = paged_query.statistics(__stats_callback)
            #endregion
            #region Step_3
            paged_query.skip(resultsToSkip)
            #endregion
            #region Step_4
            paged_query.take(resultsToTake)
            #endregion
            #region Step_5
            paged_results = list(paged_query)
            #endregion

            #region Step_6
            total_results = stats.total_results
            #endregion
        #endregion

        return paged_results
