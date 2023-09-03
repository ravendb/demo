from demo_example import Example, RunParamsBase
from models import Employee

class QueryOverview(Example):
    def run(self, run_params: RunParamsBase = None) -> None:
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            
            #region Step_1
            query_definition = session.query(object_type=Employee)
            #endregion

            #region Step_2
            # Define actions such as:

            # Filter documents by documents fields
            # Filter documents by text criteria
            # Include related documents
            # Get the query stats
            # Sort results
            # Customize the returned entity fields (Projections)
            # Control results paging
            #endregion

            #region Step_3
            query_results = list(query_definition)
            #endregion
        #endregion
