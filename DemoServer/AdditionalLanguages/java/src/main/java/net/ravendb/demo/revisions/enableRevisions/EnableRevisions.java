package net.ravendb.demo.revisions.enableRevisions;

//region Usings
import net.ravendb.client.documents.operations.revisions.ConfigureRevisionsOperation;
import net.ravendb.client.documents.operations.revisions.RevisionsCollectionConfiguration;
import net.ravendb.client.documents.operations.revisions.RevisionsConfiguration;
import java.time.Duration;
import java.util.HashMap;
import java.util.Map;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;

public class EnableRevisions {

    public void run(RunParams runParams) {
        String collection1 = runParams.getCollection1();
        String collection2 = runParams.getCollection2();

        //region Demo
        //region Step_1
        RevisionsConfiguration myRevisionsConfiguration = new RevisionsConfiguration();
        //endregion

        //region Step_2
        RevisionsCollectionConfiguration defaultConfiguration = new RevisionsCollectionConfiguration();
        defaultConfiguration.setDisabled(false);
        defaultConfiguration.setPurgeOnDelete(false);
        defaultConfiguration.setMinimumRevisionsToKeep(5L);
        defaultConfiguration.setMinimumRevisionAgeToKeep(Duration.ofDays(14));

        myRevisionsConfiguration.setDefaultConfig(defaultConfiguration);
        //endregion

        //region Step_3
        RevisionsCollectionConfiguration collection1Configuration = new RevisionsCollectionConfiguration();
        collection1Configuration.setDisabled(true);

        RevisionsCollectionConfiguration collection2Configuration = new RevisionsCollectionConfiguration();
        collection2Configuration.setPurgeOnDelete(true);

        Map<String, RevisionsCollectionConfiguration> perCollectionConfig = new HashMap<>();
        perCollectionConfig.put(collection1, collection1Configuration);
        perCollectionConfig.put(collection2, collection2Configuration);

        myRevisionsConfiguration.setCollections(perCollectionConfig);
        //endregion

        //region Step_4
        ConfigureRevisionsOperation revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
        DocumentStoreHolder.store.maintenance().send(revisionsConfigurationOperation);
        //endregion
        //endregion
    }

    public static class RunParams {
        private String collection1;
        private String collection2;

        public String getCollection1() {
            return collection1;
        }

        public void setCollection1(String collection1) {
            this.collection1 = collection1;
        }

        public String getCollection2() {
            return collection2;
        }

        public void setCollection2(String collection2) {
            this.collection2 = collection2;
        }
    }
}
