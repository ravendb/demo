<?php

namespace RavenDB\Demo\staticIndexes\mapIndex;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

//region Demo
//region Step_1
class Employees_ImportantDetails extends AbstractIndexCreationTask
{
//endregion
    //region Step_2
    public function __construct()
    {
        parent::__construct();
        $this->map = "docs.Employees.Select(employee => new { " .
            "    FullName = (employee.FirstName + \" \") + employee.LastName, " .
            "    Country = employee.Address.Country, " .
            "    WorkingInCompanySince = employee.HiredAt.Year, " .
            "    NumberOfTerritories = employee.Territories.Count " .
            "})";
    }
    //endregion
}
//endregion

class MapIndex
{
    public function __invoke(RunParams $runParams): array
    {
        $startYear = $runParams->getStartYear();

        //region Demo
        $employeesFromUSA = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_3
            $employeesFromUSA = $session->query(Employee::class, Employees_ImportantDetails::class)
                ->whereEquals("Country", "USA")
                ->whereGreaterThan("WorkingInCompanySince", $startYear)
                ->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $employeesFromUSA;
    }
}
