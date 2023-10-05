<?php

namespace RavenDB\Demo\basics\theSession;

//region Usings
use RavenDB\Documents\Session\DocumentSessionInterface;
//endregion
use RavenDB\Demo\common\DocumentStoreHolder;

class TheSession
{
    public function __invoke(): void
    {
        //region Demo
        //region Step_1
        $session = DocumentStoreHolder::getStore()->openSession("YourDatabaseName");        
        //endregion
        try {

            //region Step_2
            //   Run your business logic:
            //
            //   Store documents
            //   Load and Modify documents
            //   Query indexes & collections
            //   Delete document
            //   .... etc.
            //endregion

            //region Step_3
            $session->saveChanges();
            //endregion
        
        } finally {
            //region Step_4
            $session->close();
            //endregion
        }
        //endregion
    }
}
