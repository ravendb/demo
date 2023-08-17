from typing import Optional
from demo_example import Example
from models import Employee

#region Usings
from ravendb.documents.queries.highlighting import Highlightings
from ravendb import AbstractIndexCreationTask
from ravendb.documents.indexes.definitions import (
    FieldStorage,
    FieldIndexing,
    FieldTermVector,
)
#endregion

class RunParams:
    def __init__(self, fragment_length: int = None, fragment_count: int = None):
        self.fragment_length = fragment_length
        self.fragment_count = fragment_count

#region Demo
#region Step_1
class EmployeesDetails(AbstractIndexCreationTask):
#endregion
    def __init__(self):
        #region Step_2
        super().__init__()
        self.map = (
            "docs.Employees.Select(employee => new {"
            "    Notes = employee.Notes[0]"
            # employee.Notes is a string array,
            # indexing only the first element for this example
            "})"
        )
        #endregion

        #region Step_3
        self._store("Notes", FieldStorage.YES)
        self._index("Notes", FieldIndexing.SEARCH)
        self._term_vector("Notes", FieldTermVector.WITH_POSITIONS_AND_OFFSETS)
        #endregion
#endregion

class HighlightQueryResultsBasic(Example):
    def run(self, run_params: RunParams):
        fragmentLength = run_params.fragment_length
        fragmentCount = run_params.fragment_count

        #region Demo
        notes_highlightings: Optional[Highlightings] = None

        def __highlight_callback(result):
            nonlocal notes_highlightings
            notes_highlightings = result
        
        with self.document_store_holder.store().open_session() as session:
            #region Step_4
            employees_results = list(
                session.query_index("EmployeesDetails", Employee)
                .highlight("Notes", fragmentLength, fragmentCount, __highlight_callback)
                .search("Notes", "sales")
            )
            #endregion

            #region Step_5
            if len(employees_results) > 0:
                employee_id = employees_results[0].Id
                notes_fragments = notes_highlightings.get_fragments(employee_id)
            #endregion
        #endregion
        
        return employees_results
