<?php

namespace RavenDB\Demo\queries\filteringResultsMultipleConditions;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;
use RavenDB\Type\Collection;

class FilteringResultsMultipleConditions
{
    public function __invoke(RunParams $runParams): array
    {
        $country = $runParams->getCountry();

        //region Demo
        $filteredEmployees = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_1
            $filteredQuery = $session->query(Employee::class)
            //endregion
                //region Step_2
                ->whereIn("FirstName", [ "Anne", "John" ])
                ->orElse()
                ->openSubclause()
                ->whereEquals("Address.Country", $country)
                ->whereGreaterThan("Territories.Count", 2)
                ->whereStartsWith("Title", "Sales")
                ->closeSubclause();
                //endregion

            //region Step_3
            $filteredEmployees = $filteredQuery->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $filteredEmployees;
    }
}
