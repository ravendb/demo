from ravendb.documents.queries.highlighting import Highlightings, HighlightingOptions
from demo_example import RunParamsBase, Example
from models import Employee

#region Usings
from typing import Optional
from ravendb import AbstractIndexCreationTask
from ravendb.documents.indexes.definitions import (
    FieldStorage,
    FieldIndexing,
    FieldTermVector,
)
#endregion

class RunParams(RunParamsBase):
    def __init__(
        self,
        fragment_length: int = None,
        fragment_count: int = None,
        tag1: str = None,
        tag2: str = None,
        tag3: str = None,
        tag4: str = None,
    ):
        self.fragment_length = fragment_length
        self.fragment_count = fragment_count
        self.tag1 = tag1
        self.tag2 = tag2
        self.tag3 = tag3
        self.tag4 = tag4

class DataToShow:
    def __init__(self, document_id: str = None, index_field: str = None, fragment: str = None):
        self.document_id = document_id
        self.index_field = index_field
        self.fragment = fragment

#region Demo
#region Step_1
class EmployeesDetails(AbstractIndexCreationTask):
#endregion
    
    def __init__(self):
        #region Step_2
        super().__init__()
        self.map = (
            "docs.Employees.Select(employee => new {"
            "    Title = employee.Title,"
            "    Notes = employee.Notes[0]"
            # employee.Notes is a string array,
            # indexing only the first element for this example
            "})"
        )
        #endregion
        
        #region Step_3
        self._store("Title", FieldStorage.YES)
        self._index("Title", FieldIndexing.SEARCH)
        self._term_vector("Title", FieldTermVector.WITH_POSITIONS_AND_OFFSETS)

        self._store("Notes", FieldStorage.YES)
        self._index("Notes", FieldIndexing.SEARCH)
        self._term_vector("Notes", FieldTermVector.WITH_POSITIONS_AND_OFFSETS)
        #endregion
#endregion

class HighlightQueryResultsCustomized(Example):
    def run(self, run_params: RunParams):
        fragmentLength = run_params.fragment_length or 100
        fragmentCount = run_params.fragment_count or 1

        tag1 = run_params.tag1 or "+++"
        tag2 = run_params.tag2 or "+++"
        tag3 = run_params.tag3 or "<<<"
        tag4 = run_params.tag4 or ">>>"
        EmployeesDetails().execute(self.document_store_holder.store())

        #region Demo
        title_highlightings: Optional[Highlightings] = None

        def __title_highlight_callback(result):
            nonlocal title_highlightings
            title_highlightings = result
        
        notes_highlightings: Optional[Highlightings] = None

        def __notes_highlight_callback(result):
            nonlocal notes_highlightings
            notes_highlightings = result
        
        with self.document_store_holder.store().open_session() as session:
            #region Step_4
            tags_to_use1 = HighlightingOptions()
            tags_to_use1.pre_tags = [tag1]
            tags_to_use1.post_tags = [tag2]

            tags_to_use2 = HighlightingOptions()
            tags_to_use2.pre_tags = [tag3]
            tags_to_use2.post_tags = [tag4]
            #endregion

            #region Step_5
            employees_results = list(
                session.query_index_type(EmployeesDetails, Employee)
                .highlight(
                    "Title",
                    fragmentLength,
                    fragmentCount,
                    __title_highlight_callback,
                    tags_to_use1,
                )
                .highlight(
                    "Notes",
                    fragmentLength,
                    fragmentCount,
                    __notes_highlight_callback,
                    tags_to_use2,
                )
                .search("Title", "manager")
                .search("Notes", "sales")
            )
            #endregion

            #region Step_6
            if len(employees_results) > 0:
                employee_id = employees_results[0].Id
                title_fragments = title_highlightings.get_fragments(employee_id)
                notes_fragments = notes_highlightings.get_fragments(employee_id)
            #endregion
        #endregion
        
        highlight_results = []
        for employee in employees_results:
            title_fragments = title_highlightings.get_fragments(employee.Id)

            for item in title_fragments:
                item_results = DataToShow(employee.Id, title_highlightings.field_name, item)
                highlight_results.append(item_results)

            notes_fragments = notes_highlightings.get_fragments(employee.Id)
            for item in notes_fragments:
                item_results = DataToShow(employee.Id, notes_highlightings.field_name, item)
                highlight_results.append(item_results)

        return sorted(highlight_results, key=lambda x: x.index_field, reverse=True)
