<?php

namespace RavenDB\Demo\queries\projectingIndividualFields;

//region Usings
use RavenDB\Documents\Queries\QueryData;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Company;

class ProjectingIndividualFields
{
    public function __invoke(): array
    {
        $projectedResults = [];

        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_1
            $projectedQuery = $session->query(Company::class)
            //endregion
                //region Step_2
                    ->selectFields(CompanyDetails::class,
                        new QueryData(
                            [ "Name", "Address.City", "Address.Country" ],
                            [ "CompanyName", "City", "Country" ]));
                //endregion

            //region Step_3
            $projectedResults = $projectedQuery->toList();
            //endregion
        } finally {
            $session->close();
        }
        //endregion
        return $projectedResults;
    }
}
