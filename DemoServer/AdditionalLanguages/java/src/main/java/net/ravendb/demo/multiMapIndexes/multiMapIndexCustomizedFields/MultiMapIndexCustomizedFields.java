package net.ravendb.demo.multiMapIndexes.multiMapIndexCustomizedFields;

import net.ravendb.client.documents.indexes.AbstractMultiMapIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

import java.util.List;

public class MultiMapIndexCustomizedFields {

    public static class Contacts_ByNameAndTitle extends AbstractMultiMapIndexCreationTask {
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

        public static class ProjectedEntry extends IndexEntry {
            private String phone;

            public String getPhone() {
                return phone;
            }

            public void setPhone(String phone) {
                this.phone = phone;
            }
        }

        public Contacts_ByNameAndTitle() {
            this.addMap("docs.Employees.Select(employee => new {\n" +
                "    ContactName = (employee.FirstName + \" \") + employee.LastName,\n" +
                "    ContactTitle = employee.Title,\n" +
                "    Collection = this.MetadataFor(employee)[\"@collection\"]\n" +
                "})");

            this.addMap("docs.Companies.Select(company => new {\n" +
                "    ContactName = company.Contact.Name,\n" +
                "    ContactTitle = company.Contact.Title,\n" +
                "    Collection = this.MetadataFor(company)[\"@collection\"]\n" +
                "})");

            this.addMap("docs.Suppliers.Select(supplier => new {\n" +
                "    ContactName = supplier.Contact.Name,\n" +
                "    ContactTitle = supplier.Contact.Title,\n" +
                "    Collection = this.MetadataFor(supplier)[\"@collection\"]\n" +
                "})");

            store("ContractName", FieldStorage.YES);
            store("ContactTitle", FieldStorage.YES);
            store("Collection", FieldStorage.YES);
        }
    }

    public List<Contacts_ByNameAndTitle.ProjectedEntry> run(RunParams runParams) {
        String namePrefix = ObjectUtils.firstNonNull(runParams.getNamePrefix(), "Michael");
        String titlePrefix = ObjectUtils.firstNonNull(runParams.getTitlePrefix(), "Sales");

        List<Contacts_ByNameAndTitle.ProjectedEntry> contacts;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            contacts = session.query(Contacts_ByNameAndTitle.IndexEntry.class, Contacts_ByNameAndTitle.class)
                .whereStartsWith("ContactName", namePrefix)
                .whereStartsWith("ContactTitle", titlePrefix)
                .selectFields(Contacts_ByNameAndTitle.ProjectedEntry.class)
                .toList();
        }

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
