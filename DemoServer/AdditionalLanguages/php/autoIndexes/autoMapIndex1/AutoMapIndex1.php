<?php

namespace RavenDB\Demo\autoIndexes\autoMapIndex1;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class AutoMapIndex1
{
    public function __invoke(RunParams $runParams): ?Employee
    {
        $firstName = $runParams->getFirstName();

        //region Demo
        $employeeResult = null;

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_1
            $findEmployeeQuery = $session->query(Employee::class)
                ->whereEquals("FirstName", $firstName);
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
