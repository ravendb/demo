package net.ravendb.demo.basics.loadAndEditDocument;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;

public class LoadAndEditDocument {

    public void run(RunParams runParams) {
        String companyName = runParams.getCompanyName();

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            Company company = session.load(Company.class, "companies/5-A");
            //endregion

            //region Step_2
            company.setName(companyName);
            //endregion

            //region Step_3
            session.saveChanges();
            //endregion
        }
        //endregion
    }

    public static class RunParams {
        private String companyName;

        public String getCompanyName() {
            return companyName;
        }

        public void setCompanyName(String companyName) {
            this.companyName = companyName;
        }
    }
}
