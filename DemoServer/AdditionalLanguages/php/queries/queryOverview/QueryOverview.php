<?php

namespace RavenDB\Demo\queries\queryOverview;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class QueryOverview
{
    public function __invoke(): void
    {
        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_1
            $queryDefinition = $session->query(Employee::class);
            //endregion

            //region Step_2
            // Define actions such as:

            // Filter documents by documents fields
            // Filter documents by text criteria
            // Include related documents
            // Get the query stats
            // Sort results
            // Customize the returned entity fields (Projections)
            // Control results paging
            //endregion

            //region Step_3
            $queryResults = $queryDefinition->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion
    }
}
