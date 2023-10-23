<?php

namespace RavenDB\Demo\javascriptIndexes\javascriptMapIndex;

//region using
use RavenDB\Documents\Indexes\AbstractJavaScriptIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

//region Demo
//region Step_1
class Employees_ByImportantDetailsJS extends AbstractJavaScriptIndexCreationTask
//endregion
{
    //region Step_2
    public function __construct()
    {
        parent::__construct();

        $this->setMaps([
                "map('Employees', function (employee) {" .
                "   return {" .
                "       FullName: employee.FirstName + ' ' + employee.LastName," .
                "       Country: employee.Address.Country," .
                "       WorkingInCompanySince: new Date(employee.HiredAt).getFullYear()," .
                "       NumberOfTerritories: employee.Territories.length" .
                "   };" .
                "})"
            ]);
    }
    //endregion
}
//endregion

//region Demo
//region Step_3
class IndexEntry
{
    public ?string $FullName = null;
    public ?string $Country = null;
    public ?int $WorkingInCompanySince = null;
    public ?int $NumberOfTerritories = null;

    public function getFullName(): ?string
    {
        return $this->FullName;
    }

    public function setFullName(?string $FullName): void
    {
        $this->FullName = $FullName;
    }

    public function getCountry(): ?string
    {
        return $this->Country;
    }

    public function setCountry(?string $Country): void
    {
        $this->Country = $Country;
    }

    public function getWorkingInCompanySince(): ?int
    {
        return $this->WorkingInCompanySince;
    }

    public function setWorkingInCompanySince(?int $WorkingInCompanySince): void
    {
        $this->WorkingInCompanySince = $WorkingInCompanySince;
    }

    public function getNumberOfTerritories(): ?int
    {
        return $this->NumberOfTerritories;
    }

    public function setNumberOfTerritories(?int $NumberOfTerritories): void
    {
        $this->NumberOfTerritories = $NumberOfTerritories;
    }
}
//endregion
//endregion

class JavascriptMapIndex
{
    public function __invoke(RunParams $runParams): array
    {
        $startYear = $runParams->getStartYear();

        //region Demo
        $employeesFromUSA = [];
        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_4
            $employeesFromUSA = $session->query(IndexEntry::class, Employees_ByImportantDetailsJS::class)
                ->whereEquals("Country", "USA")
                ->whereGreaterThan("WorkingInCompanySince", $startYear)
                ->selectFields(Employee::class)
                ->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion
        
        return $employeesFromUSA;
    }
}
