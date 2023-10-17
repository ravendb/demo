<?php

namespace RavenDB\Demo\revisions\enableRevisions;

//region Usings
use RavenDB\Documents\Operations\Revisions\ConfigureRevisionsOperation;
use RavenDB\Documents\Operations\Revisions\RevisionsCollectionConfiguration;
use RavenDB\Documents\Operations\Revisions\RevisionsConfiguration;
use RavenDB\Type\Duration;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

class EnableRevisions
{
    public function __invoke(RunParams $runParams): string
    {
        $collection1 = $runParams->getCollection1() ?? "Orders";
        $collection2 = $runParams->getCollection2() ?? "Companies";

        //region Demo
        //region Step_1
        $myRevisionsConfiguration = new RevisionsConfiguration();
        //endregion

        //region Step_2
        $defaultConfiguration = new RevisionsCollectionConfiguration();
        $defaultConfiguration->setDisabled(false);
        $defaultConfiguration->setPurgeOnDelete(false);
        $defaultConfiguration->setMinimumRevisionsToKeep(5);
        $defaultConfiguration->setMinimumRevisionAgeToKeep(Duration::ofDays(14));

        $myRevisionsConfiguration->setDefaultConfig($defaultConfiguration);
        //endregion

        //region Step_3
        $collection1Configuration = new RevisionsCollectionConfiguration();
        $collection1Configuration->setDisabled(true);

        $collection2Configuration = new RevisionsCollectionConfiguration();
        $collection2Configuration->setPurgeOnDelete(true);

        $perCollectionConfig = [];
        $perCollectionConfig[$collection1] = $collection1Configuration;
        $perCollectionConfig[$collection2] = $collection2Configuration;

        $myRevisionsConfiguration->setCollections($perCollectionConfig);
        //endregion

        //region Step_4
        $revisionsConfigurationOperation = new ConfigureRevisionsOperation($myRevisionsConfiguration);
        DocumentStoreHolder::getStore()->maintenance()->send($revisionsConfigurationOperation);
        //endregion
        //endregion

        return "Revisions on $collection1 & $collection2 collections were successfully defined";
    }
}
