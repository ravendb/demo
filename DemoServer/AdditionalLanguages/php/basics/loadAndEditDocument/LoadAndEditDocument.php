<?php

namespace RavenDB\Demo\basics\loadAndEditDocument;

//region Usings
use RavenDB\Documents\Session\DocumentSessionInterface;
//endregion
Use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Company;

class LoadAndEditDocument
{
    public function __invoke(RunParams $runParams)
    {
        $companyName = $runParams->getCompanyName();

        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $company = $session->load(Company::class, "companies/5-A");
            //endregion

            //region Step_2
            $company->setName($companyName);
            //endregion

            //region Step_3
            $session->saveChanges();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion
    }
}
