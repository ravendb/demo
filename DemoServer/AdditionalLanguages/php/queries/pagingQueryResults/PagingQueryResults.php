<?php

namespace RavenDB\Demo\queries\pagingQueryResults;

//region Usings
use RavenDB\Demo\common\models\Company;
use RavenDB\Documents\Session\QueryStatistics;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

class PagingQueryResults
{
    public function __invoke(RunParams $runParams)
    {
        $resultsToSkip = $runParams->getResultsToSkip();
        $resultsToTake = $runParams->getResultsToTake();

        //region Demo
        $pagedResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $statsRef = new QueryStatistics();

            //region Step_1
            $pagedResults = $session->query(Company::class)
            //endregion
                //region Step_2
                ->statistics($statsRef)
                //endregion
                //region Step_3
                ->skip($resultsToSkip)
                //endregion
                //region Step_4
                ->take($resultsToTake)
                //endregion
                //region Step_5
                ->toList();
                //endregion

            //region Step_6
            $totalResults = $statsRef->getTotalResults();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $pagedResults;
    }
}
