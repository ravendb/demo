<?php

namespace RavenDB\Demo\autoIndexes\autoMapIndex2;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class AutoMapIndex2
{
    public function __invoke(RunParams  $runParams): ?Employee
    {
        $country = $runParams->getCountry();

        //region Demo
        $employeeResult = null;

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_1
            $findEmployeeQuery = $session->query(Employee::class)
                    ->whereEquals("Address.Country", $country)
                    ->whereStartsWith("Title", "Sales");
            //endregion

            //region Step_2
            $employeeResult = $findEmployeeQuery->firstOrDefault();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $employeeResult;
    }
}
