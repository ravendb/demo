package net.ravendb.demo.revisions.getRevisions;

//region Usings
import net.ravendb.client.documents.operations.revisions.ConfigureRevisionsOperation;
import net.ravendb.client.documents.operations.revisions.RevisionsCollectionConfiguration;
import net.ravendb.client.documents.operations.revisions.RevisionsConfiguration;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;

public class GetRevisions {

    public List<Company> run() {
        //region Demo
        //region Step_1
        RevisionsConfiguration myRevisionsConfiguration = new RevisionsConfiguration();

        RevisionsCollectionConfiguration defaultConfiguration = new RevisionsCollectionConfiguration();
        defaultConfiguration.setDisabled(false);
        myRevisionsConfiguration.setDefaultConfig(defaultConfiguration);

        ConfigureRevisionsOperation revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
        DocumentStoreHolder.store.maintenance().send(revisionsConfigurationOperation);
        //endregion

        List<Company> revisions;
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_2
            Company company = session.load(Company.class, "companies/7-A");

            company.setName("Name 1");
            session.countersFor("companies/7-A").increment("MyCounter", 100);
            session.saveChanges();

            company.setName("Name 2");
            company.setPhone("052-1234-567");
            session.saveChanges();
            //endregion

            //region Step_3
            revisions =session
                .advanced()
                .revisions()
                .getFor(Company.class, "companies/7-A", 0, 25);
            //endregion
        }

        //endregion

        return revisions;
    }
}
