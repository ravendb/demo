from typing import List
from demo_example import Example
from models import Company

class FullCollectionQuery(Example):
    def run(self, run_params=None) -> List[Company]:
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            full_collection_query = session.query(object_type=Company)
            #endregion

            #region Step_2
            collection_results = list(full_collection_query)
            #endregion
        #endregion

        return collection_results
