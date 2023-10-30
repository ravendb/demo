<?php

namespace RavenDB\Demo\textSearch\highlightQueryResultsCustomized;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Indexes\FieldIndexing;
use RavenDB\Documents\Indexes\FieldStorage;
use RavenDB\Documents\Indexes\FieldTermVector;
use RavenDB\Documents\Queries\Highlighting\HighlightingOptions;
use RavenDB\Documents\Queries\Highlighting\Highlightings;
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
            "    Title = employee.Title," .
            "    Notes = employee.Notes[0]" .
            // employee.Notes is a string array,
            // indexing only the first element for this example
            "})";
        //endregion

        //region Step_3
        $this->store("Title", FieldStorage::yes());
        $this->index("Title", FieldIndexing::search());
        $this->termVector("Title", FieldTermVector::withPositionsAndOffsets());

        $this->store("Notes", FieldStorage::yes());
        $this->index("Notes", FieldIndexing::search());
        $this->termVector("Notes", FieldTermVector::withPositionsAndOffsets());
        //endregion
    }
}
//endregion

class HighlightQueryResultsCustomized
{
    public function __invoke(RunParams $runParams)
    {
        $fragmentLength = $runParams->getFragmentLength() ?? 100;
        $fragmentCount = $runParams->getFragmentCount() ?? 1;

        $tag1 = $runParams->getTag1() ?? "+++";
        $tag2 = $runParams->getTag2() ?? "+++";
        $tag3 = $runParams->getTag3() ?? "<<<";
        $tag4 = $runParams->getTag4() ?? ">>>";

        $titleHighlightings = new Highlightings();
        $notesHighlightings = new Highlightings();
        //region Demo
        $employeesResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {

        //region Step_4
        $tagsToUse1 = new HighlightingOptions();
            $tagsToUse1->setPreTags([ $tag1 ]);
            $tagsToUse1->setPostTags([ $tag2 ]);

            $tagsToUse2 = new HighlightingOptions();
            $tagsToUse2->setPreTags([ $tag3 ]);
            $tagsToUse2->setPostTags([ $tag4 ]);
            //endregion

            //region Step_5
            $employeesResults = $session->query(Employee::class, EmployeesDetails::class)
                ->highlight("Title", $fragmentLength, $fragmentCount, $tagsToUse1, $titleHighlightings)
                ->highlight("Notes", $fragmentLength, $fragmentCount, $tagsToUse2, $notesHighlightings)
                ->search("Title", "manager")
                ->search("Notes", "sales")
                ->toList();
            //endregion

            //region Step_6
            if (count($employeesResults) > 0) {
                $employeeId = $employeesResults[0]->getId();
                $titleFragments = $titleHighlightings->getFragments($employeeId);
                $notesFragments = $notesHighlightings->getFragments($employeeId);
            }
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        $highlightResults = [];

        /** @var Employee $employee */
        foreach ($employeesResults as $employee) {
            $titleFragments = $titleHighlightings->getFragments($employee->getId());

            foreach ($titleFragments as $item) {
                $itemResults = new DataToShow();
                $itemResults->setDocumentId($employee->getId());
                $itemResults->setIndexField($titleHighlightings->getFieldName());
                $itemResults->setFragment($item);

                $highlightResults[] = $itemResults;
            }

            $notesFragments = $notesHighlightings->getFragments($employee->getId());
            foreach ($notesFragments as $item) {
                $itemResults = new DataToShow();
                $itemResults->setDocumentId($employee->getId());
                $itemResults->setIndexField($notesHighlightings->getFieldName());
                $itemResults->setFragment($item);

                $highlightResults[] = $itemResults;
            }
        }

        usort($highlightResults, function(DataToShow $a, DataToShow $b) {
            return $a->getIndexField() < $b->getIndexField();
        });
        return $highlightResults;
    }
}

class DataToShow
{
    private ?string $documentId = null;
    private ?string $indexField = null;
    private ?string $fragment = null;

    public function getDocumentId(): ?string
    {
        return $this->documentId;
    }

    public function setDocumentId(?string $documentId): void
    {
        $this->documentId = $documentId;
    }

    public function getIndexField(): ?string
    {
        return $this->indexField;
    }

    public function setIndexField(?string $indexField): void
    {
        $this->indexField = $indexField;
    }

    public function getFragment(): ?string
    {
        return $this->fragment;
    }

    public function setFragment(?string $fragment): void
    {
        $this->fragment = $fragment;
    }
}
