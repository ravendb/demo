from typing import Optional, List
from demo_example import Example
from models import Employee

#region Usings
from ravendb import QueryStatistics
#endregion

class QueryExample(Example):
    def run(self, run_params=None) -> List[Employee]:
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            stats: Optional[QueryStatistics] = None

            def __stats_callback(statistics: QueryStatistics):
                nonlocal stats
                stats = statistics

            #region Step_1
            query = session.query(object_type=Employee)
            #endregion
            #region Step_2
            query = query.where_equals("FirstName", "Steven")\
                .or_else()\
                .where_equals("Title", "Sales Representative")
            #endregion
            #region Step_3
            query = query.include("ReportsTo")
            #endregion
            #region Step_4
            query = query.statistics(__stats_callback)
            #endregion
            #region Step_5
            query = query.order_by_descending("HiredAt")
            #endregion
            #region Step_6
            query = query.take(5)
            #endregion

            #region Step_7
            query_results = list(query)
            #endregion
        #endregion

        return query_results
