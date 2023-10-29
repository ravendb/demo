<?php

namespace RavenDB\Demo\textSearch\fTSQuerySearchOperators;

//region Usings
use RavenDB\Documents\Queries\SearchOperator;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class FTSQuerySearchOperators
{
    public function __invoke(RunParams $runParams): array
    {
        $term1 = $runParams->getTerm1();
        $term2 = $runParams->getTerm2();
        $term3 = $runParams->getTerm3();

        //region Demo
        $employeesWithMatchingTerms = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $employeesWithMatchingTerms = $session->query(Employee::class)
            //endregion
                //region Step_2
                ->search("Notes", $term1 . " " . $term2, SearchOperator::and())
                //endregion
                //region Step_3
                ->search("Notes", $term3, SearchOperator::or())
                //endregion
                //region Step_4
                ->toList();
                //endregion
        } finally {
            $session->close();
        }
        //endregion
        return $employeesWithMatchingTerms;
    }
}
