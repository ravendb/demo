<?php

namespace RavenDB\Demo\textSearch\fTSQuerySearchBasics;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class FTSQuerySearchBasics
{
    public function __invoke(RunParams $runParams): array
    {
        $term1 = $runParams->getTerm1();
        $term2 = $runParams->getTerm2();

        //region Demo
        $employeesWithMatchingTerms = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $employeesWithMatchingTerms = $session->query(Employee::class)
            //endregion
                //region Step_2
                ->search("Notes", $term1 . " " . $term2)
                //endregion
                //region Step_3
                ->toList();
                //endregion
        } finally {
            $session->close();
        }
        //endregion
        return $employeesWithMatchingTerms;
    }
}
