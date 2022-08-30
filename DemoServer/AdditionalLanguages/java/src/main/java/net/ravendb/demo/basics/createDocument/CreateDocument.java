package net.ravendb.demo.basics.createDocument;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.common.models.Contact;
import net.ravendb.demo.common.DocumentStoreHolder;

public class CreateDocument {

    public String run(RunParams runParams) {
        String companyName = runParams.getCompanyName();
        String companyPhone = runParams.getCompanyPhone();
        String contactName = runParams.getContactName();
        String contactTitle = runParams.getContactTitle();

        String theNewDocumentId;

        //region Demo
        //region Step_1
        Company newCompany = new Company();
        newCompany.setName(companyName);
        newCompany.setPhone(companyPhone);

        Contact newContact = new Contact();
        newContact.setName(contactName);
        newContact.setTitle(contactTitle);
        newCompany.setContact(newContact);
        //endregion

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_2
            session.store(newCompany);
            //endregion

            //region Step_3
            theNewDocumentId = newCompany.getId();
            //endregion

            //region Step_4
            session.saveChanges();
            //endregion
        }
        //endregion

        return theNewDocumentId;
    }

    public static class RunParams {
        private String companyName;
        private String companyPhone;
        private String contactName;
        private String contactTitle;

        public String getCompanyName() {
            return companyName;
        }

        public void setCompanyName(String companyName) {
            this.companyName = companyName;
        }

        public String getCompanyPhone() {
            return companyPhone;
        }

        public void setCompanyPhone(String companyPhone) {
            this.companyPhone = companyPhone;
        }

        public String getContactName() {
            return contactName;
        }

        public void setContactName(String contactName) {
            this.contactName = contactName;
        }

        public String getContactTitle() {
            return contactTitle;
        }

        public void setContactTitle(String contactTitle) {
            this.contactTitle = contactTitle;
        }
    }
}
