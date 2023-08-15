from demo_example import Example
from models import Employee

#region Usings
from ravendb import AbstractIndexCreationTask
#endregion

#region Demo
#region Step_1
class Employees_ByLastName(AbstractIndexCreationTask):
#endregion

    #region Step_2
    def __init__(self):
        super().__init__()
        # Define:
        #    Map(s) functions
        #    Reduce function
        #    Additional indexing options per field
    #endregion
#endregion

class StaticIndexesOverview(Example):
    def run(self, run_params=None):
        #region Demo
        #region Step_3
        Employees_ByLastName().execute(self.document_store_holder.store())
        #endregion

        with (self.document_store_holder.store().open_session() as session):
            #region Step_4
            query_on_index = session.query_index_type(Employees_ByLastName, Employee)\
                .where_equals("LastName", "SomeName")

            query_results = list(query_on_index)
            #endregion
        #endregion
