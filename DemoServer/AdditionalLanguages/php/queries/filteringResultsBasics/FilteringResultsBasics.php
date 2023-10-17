<?php

namespace RavenDB\Demo\queries\filteringResultsBasics;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class FilteringResultsBasics
{
    public function __invoke(): array
    {
        //region Demo
        $filteredEmployees = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
        //region Step_1
        $filteredQuery = $session->query(Employee::class)
        //endregion
                //region Step_2
                ->whereEquals("FirstName", "Anne");
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
