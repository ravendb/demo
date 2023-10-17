<?php

namespace RavenDB\Demo\queries\fullCollectionQuery;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Company;

class FullCollectionQuery
{
    public function __invoke()
    {
        //region Demo
        $collectionResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_1
            $fullCollectionQuery = $session->query(Company::class);
            //endregion

            //region Step_2
            $collectionResults = $fullCollectionQuery->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $collectionResults;
    }
}
