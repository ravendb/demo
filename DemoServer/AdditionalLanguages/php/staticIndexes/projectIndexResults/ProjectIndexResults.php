<?php

namespace RavenDB\Demo\staticIndexes\projectIndexResults;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Queries\QueryData;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region Demo
//region Step_1
class Employees_ByWorkPeriod extends AbstractIndexCreationTask
//endregion
{
    //region Step_2
    public function __construct()
    {
        parent::__construct();
        $this->map =
            "docs.Employees.Select(employee => new {" .
            "      WorkingInCompanySince = employee.HiredAt.Year" .
            "})";
    }
    //endregion
}
//endregion

//region Demo
//region Step_3
class IndexEntry
{
    private ?int $workingInCompanySince = null;

    public function getWorkingInCompanySince(): ?int
    {
        return $this->workingInCompanySince;
    }

    public function setWorkingInCompanySince(?int $workingInCompanySince): void
    {
        $this->workingInCompanySince = $workingInCompanySince;
    }
}
//endregion
//endregion

//region Demo
//region Step_4
class EmployeeProjectedDetails
{
    private ?string $firstName = null;
    private ?string $phone = null;
    private ?string $location = null;

    public function getFirstName(): ?string
    {
        return $this->firstName;
    }

    public function setFirstName(?string $firstName): void
    {
        $this->firstName = $firstName;
    }

    public function getPhone(): ?string
    {
        return $this->phone;
    }

    public function setPhone(?string $phone): void
    {
        $this->phone = $phone;
    }

    public function getLocation(): ?string
    {
        return $this->location;
    }

    public function setLocation(?string $location): void
    {
        $this->location = $location;
    }
}
//endregion
//endregion

class ProjectIndexResults
{
    public function __invoke(RunParams $runParams): array
    {
        $startYear = $runParams->getStartYear();

        //region Demo
        $employeesSinceYear = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $employeesQuery = $session
                //region Step_5
                ->query(IndexEntry::class, Employees_ByWorkPeriod::class)
                ->whereGreaterThan("WorkingInCompanySince", $startYear)
                //endregion
                //region Step_6
                ->selectFields(EmployeeProjectedDetails::class,
                    QueryData::customFunction("employee ",
                        "{ FirstName: employee.FirstName," .
                        "  Phone: employee.HomePhone," .
                        "  Location: employee.Address.City + ' ' + employee.Address.Country }"));
                //endregion

            //region Step_7
            $employeesSinceYear = $employeesQuery->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $employeesSinceYear;
    }
}
