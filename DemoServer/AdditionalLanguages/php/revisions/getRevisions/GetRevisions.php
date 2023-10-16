<?php

namespace RavenDB\Demo\revisions\getRevisions;

//region Usings
use RavenDB\Documents\Operations\Revisions\ConfigureRevisionsOperation;
use RavenDB\Documents\Operations\Revisions\RevisionsCollectionConfiguration;
use RavenDB\Documents\Operations\Revisions\RevisionsConfiguration;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Company;

class GetRevisions
{
    public function __invoke(): array
    {
        //region Demo
        //region Step_1
        $myRevisionsConfiguration = new RevisionsConfiguration();

        $defaultConfiguration = new RevisionsCollectionConfiguration();
        $defaultConfiguration->setDisabled(false);
        $myRevisionsConfiguration->setDefaultConfig($defaultConfiguration);

        $revisionsConfigurationOperation = new ConfigureRevisionsOperation($myRevisionsConfiguration);
        DocumentStoreHolder::getStore()->maintenance()->send($revisionsConfigurationOperation);
        //endregion

        $revisions = [];
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
        
            //region Step_2
            $company = $session->load(Company::class, "companies/7-A");

            $company->setName("Name 1");
            $session->countersFor("companies/7-A")->increment("MyCounter", 100);
            $session->saveChanges();

            $company->setName("Name 2");
            $company->setPhone("052-1234-567");
            $session->saveChanges();
            //endregion

            //region Step_3
            $revisions = $session
                ->advanced()
                ->revisions()
                ->getFor(Company::class, "companies/7-A", 0, 25);
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $revisions;
    }
}
