package net.ravendb.demo.multiMapIndexes.multiMapIndexCustomizedFields;

//region Usings
import net.ravendb.client.documents.indexes.AbstractMultiMapIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion

import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

public class MultiMapIndexCustomizedFields {

    //region Demo
    //region Step_1
    public static class Contacts_ByNameAndTitle extends AbstractMultiMapIndexCreationTask {
    //endregion
    
        //region Step_2
        public static class IndexEntry {
            private String contractName;
            private String contractTitle;
            private Object collection;

            public String getContractName() {
                return contractName;
            }

            public void setContractName(String contractName) {
                this.contractName = contractName;
            }

            public String getContractTitle() {
                return contractTitle;
            }

            public void setContractTitle(String contractTitle) {
                this.contractTitle = contractTitle;
            }

            public Object getCollection() {
                return collection;
            }

            public void setCollection(Object collection) {
                this.collection = collection;
            }
        }
        //endregion

        //region Step_3
        public static class ProjectedEntry extends IndexEntry {
            private String phone;

            public String getPhone() {
                return phone;
            }

            public void setPhone(String phone) {
                this.phone = phone;
            }
        }
        //endregion
       
        public Contacts_ByNameAndTitle() {
            //region Step_4
            addMap("docs.Employees.Select(employee => new {" +
                "    contactName = (employee.FirstName + \" \") + employee.LastName," +
                "    contactTitle = employee.Title," +
                "    collection = this.MetadataFor(employee)[\"@collection\"]" +
                "})");

            addMap("docs.Companies.Select(company => new {" +
                "    contactName = company.Contact.Name," +
                "    contactTitle = company.Contact.Title," +
                "    collection = this.MetadataFor(company)[\"@collection\"]" +
                "})");

            addMap("docs.Suppliers.Select(supplier => new {" +
                "    contactName = supplier.Contact.Name," +
                "    contactTitle = supplier.Contact.Title," +
                "    collection = this.MetadataFor(supplier)[\"@collection\"]" +
                "})");
            //endregion
            //region Step_5
            store("contractName", FieldStorage.YES);
            store("contactTitle", FieldStorage.YES);
            store("collection", FieldStorage.YES);
            //endregion
        }
    }
    //endregion

    public List<Contacts_ByNameAndTitle.ProjectedEntry> run(RunParams runParams) {
        String namePrefix = ObjectUtils.firstNonNull(runParams.getNamePrefix(), "Michael");
        String titlePrefix = ObjectUtils.firstNonNull(runParams.getTitlePrefix(), "Sales");
        
        //region Demo
        //region Step_6
        List<Contacts_ByNameAndTitle.ProjectedEntry> contacts;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            contacts = session.query(Contacts_ByNameAndTitle.IndexEntry.class, Contacts_ByNameAndTitle.class)
                .whereStartsWith("contactName", namePrefix)
                .whereStartsWith("contactTitle", titlePrefix)
                .selectFields(Contacts_ByNameAndTitle.ProjectedEntry.class)
                .toList();
        }
        //endregion
        //endregion
        return contacts;
    }

    public static class RunParams {
        private String namePrefix;
        private String titlePrefix;

        public String getNamePrefix() {
            return namePrefix;
        }

        public void setNamePrefix(String namePrefix) {
            this.namePrefix = namePrefix;
        }

        public String getTitlePrefix() {
            return titlePrefix;
        }

        public void setTitlePrefix(String titlePrefix) {
            this.titlePrefix = titlePrefix;
        }
    }
}
