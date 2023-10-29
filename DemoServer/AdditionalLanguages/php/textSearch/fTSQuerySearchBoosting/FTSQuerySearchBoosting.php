<?php

namespace RavenDB\Demo\textSearch\fTSQuerySearchBoosting;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class FTSQuerySearchBoosting
{
    public function __invoke(RunParams $runParams): array
    {
        $boost1 = $runParams->getBoost1();
        $boost2 = $runParams->getBoost2();
        $boost3 = $runParams->getBoost3();

        //region Demo
        $employeesWithMatchingTerms = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $employeesWithMatchingTerms = $session->query(Employee::class)
            //endregion
                //region Step_2
                ->search("Notes", "ph.d.")->boost($boost1)
                ->search("Notes", "university")->boost($boost2)
                ->search("Notes", "college")->boost($boost3)
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
