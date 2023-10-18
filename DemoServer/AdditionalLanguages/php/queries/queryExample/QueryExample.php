<?php

namespace RavenDB\Demo\queries\queryExample;

//region Usings
use RavenDB\Documents\Session\QueryStatistics;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class QueryExample
{
    public function __invoke(): array
    {
        //region Demo
        $queryResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $statisticsRef = new QueryStatistics();

            //region Step_1
            $query = $session->query(Employee::class)
            //endregion
                //region Step_2
                ->whereEquals("FirstName", "Steven")
                ->orElse()
                ->whereEquals("Title", "Sales Representative")
                //endregion
                //region Step_3
                ->include("ReportsTo")
                //endregion
                //region Step_4
                ->statistics($statisticsRef)
                //endregion
                //region Step_5
                ->orderByDescending("HiredAt")
                //endregion
                //region Step_6
                ->take(5);
                //endregion

            //region Step_7
            $queryResults = $query->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $queryResults;
    }
}
