<?php

namespace RavenDB\Demo\queries\queryByDocumentId;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class QueryByDocumentId
{
    public function __invoke(RunParams $runParams): ?Employee
    {
        $employeeDocumentId = $runParams->getEmployeeDocumentId();
        //region Demo
        $employee = null;

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
        //region Step_1
        $queryByDocumentId = $session->query(Employee::class)
            //endregion
            //region Step_2
                ->whereEquals("id", $employeeDocumentId);
            //endregion

            //region Step_3
            $employee = $queryByDocumentId->firstOrDefault();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $employee;
    }
}
