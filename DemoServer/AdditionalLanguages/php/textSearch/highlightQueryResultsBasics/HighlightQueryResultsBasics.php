<?php

namespace RavenDB\Demo\textSearch\highlightQueryResultsBasics;

//region Usings
use RavenDB\Documents\Queries\Highlighting\Highlightings;
use RavenDB\Documents\Queries\Query;
use RavenDB\Documents\Session\DocumentQuery;
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Indexes\FieldIndexing;
use RavenDB\Documents\Indexes\FieldStorage;
use RavenDB\Documents\Indexes\FieldTermVector;
//endregion

//region Demo
//region Step_1
class EmployeesDetails extends AbstractIndexCreationTask
{
//endregion
    public function __construct()
    {
        parent::__construct();

        //region Step_2
        $this->map = "docs.Employees.Select(employee => new {" .
            "    Notes = employee.Notes[0]" .
            // employee.Notes is a string array,
            // indexing only the first element for this example
            "})";
        //endregion

        //region Step_3
        $this->store("Notes", FieldStorage::yes());
        $this->index("Notes", FieldIndexing::search());
        $this->termVector("Notes", FieldTermVector::withPositionsAndOffsets());
        //endregion
    }

}
//endregion

class HighlightQueryResultsBasics
{
    public function __invoke(RunParams $runParams): array
    {
        $fragmentLength = $runParams->getFragmentLength();
        $fragmentCount = $runParams->getFragmentCount();
        $notesHighlightings = new Highlightings();

        //region Demo
        $employeesResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_4
            $employeesResults = $session->query(Employee::class, Query::index("EmployeesDetails"))
                    ->highlight("Notes", $fragmentLength, $fragmentCount, null, $notesHighlightings)
                    ->search("Notes", "sales")
                    ->toList();
            //endregion

            //region Step_5
            if (count($employeesResults) > 0) {
                $employeeId = $employeesResults[0]->getId();
                $notesFragments = $notesHighlightings->getFragments($employeeId);
            }
            //endregion
        } finally {
            $session->close();
        }
        //endregion
        return $employeesResults;
    }
}
