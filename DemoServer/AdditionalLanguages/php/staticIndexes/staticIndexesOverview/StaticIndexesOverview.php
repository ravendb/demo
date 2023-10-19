<?php

namespace RavenDB\Demo\staticIndexes\staticIndexesOverview;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

//region Demo
//region Step_1
class Employees_ByLastName extends AbstractIndexCreationTask
//endregion
{
    //region Step_2
    public function __construct()
    {
        parent::__construct();
        // Define:
        //    Map(s) functions
        //    Reduce function
        //    Additional indexing options per field
    }
    //endregion
}
//endregion

class StaticIndexesOverview
{
    public function __invoke()
    {
        //region Demo
        $queryResults = [];

        //region Step_3
        (new Employees_ByLastName())->execute(DocumentStoreHolder::getStore());
        //endregion

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_4
            $queryOnIndex = $session->query(Employee::class, Employees_ByLastName::class)
                ->whereEquals("LastName", "SomeName");

            $queryResults = $queryOnIndex->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion
    }
}
