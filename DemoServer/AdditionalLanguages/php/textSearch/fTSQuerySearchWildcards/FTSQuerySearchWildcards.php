<?php

namespace RavenDB\Demo\textSearch\fTSQuerySearchWildcards;

//region Usings
use RavenDB\Documents\Queries\SearchOperator;
//endregion

use RavenDB\Demo\common\models\LastFm;
use RavenDB\Demo\common\DocumentStoreHolder;

class FTSQuerySearchWildcards
{
    public function __invoke(RunParams $runParams): array
    {
        $start = $runParams->getStart();
        $end = $runParams->getEnd();
        $middle = $runParams->getMiddle();
        $numberOfResults = $runParams->getNumberOfResults();

        //region Demo
        $songsWithMatchingTerms = [];

        $session = DocumentStoreHolder::getMediaStore()->openSession();
        try {
            //region Step_1
            $songsWithMatchingTerms = $session->query(LastFm::class)
            //endregion
                //region Step_2
                ->search("Artist", $start . "* *" . $end, SearchOperator::and())
                //endregion
                //region Step_3
                ->search("Title", "*" . $middle . "*")
                //endregion
                //region Step_4
                ->take($numberOfResults)
                ->orderBy("Artist")
                //endregion
                //region Step_5
                ->toList();
                //endregion
        } finally {
            $session->close();
        }
        //endregion
        return $songsWithMatchingTerms;
    }
}
