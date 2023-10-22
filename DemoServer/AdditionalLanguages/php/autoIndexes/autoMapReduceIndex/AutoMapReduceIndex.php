<?php

namespace RavenDB\Demo\autoIndexes\autoMapReduceIndex;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

//region Demo
//region Step_1
class CountryDetails
{
    private ?string $country = null;
    private ?int $numberOfEmployees = null;

    public function getCountry(): ?string
    {
        return $this->country;
    }

    public function setCountry(?string $country): void
    {
        $this->country = $country;
    }

    public function getNumberOfEmployees(): ?int
    {
        return $this->numberOfEmployees;
    }

    public function setNumberOfEmployees(?int $numberOfEmployees): void
    {
        $this->numberOfEmployees = $numberOfEmployees;
    }
}
//endregion
//endregion

class AutoMapReduceIndex
{
    public function __invoke(): array
    {
        //region Demo
        $numberOfEmployeesPerCountry = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {

            //region Step_2
            $numberOfEmployeesPerCountry = $session->query(Employee::class)
            //endregion
                //region Step_3
                ->groupBy("Address.Country")
                //endregion
                //region Step_4
                ->selectKey("Address.Country", "Country")
                ->selectCount("NumberOfEmployees")
                //endregion
                //region Step_5
                ->orderByDescending("NumberOfEmployees")
                //endregion
                //region Step_6
                ->ofType(CountryDetails::class)
                //endregion
                //region Step_7
                ->toList();
                //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $numberOfEmployeesPerCountry;
    }
}
