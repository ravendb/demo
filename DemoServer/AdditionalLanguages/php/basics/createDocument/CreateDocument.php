<?php

namespace RavenDB\Demo\basics\createDocument;

//region Usings
use RavenDB\Documents\Session\DocumentSessionInterface;
//endregion
use RavenDB\Demo\common\models\Company;
use RavenDB\Demo\common\models\Contact;
use RavenDB\Demo\common\DocumentStoreHolder;

class CreateDocument
{
    public function __invoke(RunParams $runParams): ?string
    {
        $companyName = $runParams->getCompanyName();
        $companyPhone = $runParams->getCompanyPhone();
        $contactName = $runParams->getContactName();
        $contactTitle = $runParams->getContactTitle();

        $theNewDocumentId = "";

        //region Demo
        //region Step_1
        $newCompany = new Company();
        $newCompany->setName($companyName);
        $newCompany->setPhone($companyPhone);

        $newContact = new Contact();
        $newContact->setName($contactName);
        $newContact->setTitle($contactTitle);
        $newCompany->setContact($newContact);
        //endregion

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_2
            $session->store($newCompany);
            //endregion

            //region Step_3
            $theNewDocumentId = $newCompany->getId();
            //endregion

            //region Step_4
            $session->saveChanges();
            //endregion

        } finally {
            $session->close();
        }
        //endregion

        return $theNewDocumentId;
    }
}
