<?php

namespace RavenDB\Demo\queries\sortingQueryResults;

//region Usings
use RavenDB\Documents\Session\OrderingType;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;

class SortingQueryResults
{
    public function __invoke(RunParams $runParams): array
    {
        $numberOfUnits = $runParams->getNumberOfUnits();

        //region Demo
        $sortedProducts = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_1
            $sortedProducts = $session->query(Product::class)
            //endregion
            //region Step_2
                ->whereGreaterThan("UnitsInStock", $numberOfUnits)
                //endregion
                //region Step_3
                ->orderByDescending("UnitsInStock")
                //endregion
                //region Step_4
                ->orderBy("Name", OrderingType::alphaNumeric())
                //endregion
                //region Step_5
                ->toList();
                //endregion
                
        } finally {
            $session->close();
        }
        //endregion

        return $sortedProducts;
    }
}
