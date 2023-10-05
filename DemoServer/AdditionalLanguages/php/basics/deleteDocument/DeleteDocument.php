<?php

namespace RavenDB\Demo\basics\deleteDocument;

//region Usings
use RavenDB\Documents\Session\DocumentSessionInterface;
//endregion
use RavenDB\Demo\common\DocumentStoreHolder;

class DeleteDocument
{
    public function __invoke(RunParams $runParams)
    {
        $documentID = $runParams->getDocumentId();

        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $session->delete($documentID);
            //endregion

            //region Step_2
            $session->saveChanges();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion
    }
}
