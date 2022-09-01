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
            private String contactName;
            private String contactTitle;
            private Object collection;

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
                "    ContactName = (employee.FirstName + \" \") + employee.LastName," +
                "    ContactTitle = employee.Title," +
                "    Collection = this.MetadataFor(employee)[\"@collection\"]" +
                "})");

            addMap("docs.Companies.Select(company => new {" +
                "    ContactName = company.Contact.Name," +
                "    ContactTitle = company.Contact.Title," +
                "    Collection = this.MetadataFor(company)[\"@collection\"]" +
                "})");

            addMap("docs.Suppliers.Select(supplier => new {" +
                "    ContactName = supplier.Contact.Name," +
                "    ContactTitle = supplier.Contact.Title," +
                "    Collection = this.MetadataFor(supplier)[\"@collection\"]" +
                "})");
            //endregion
            //region Step_5
            store("ContactName", FieldStorage.YES);
            store("ContactTitle", FieldStorage.YES);
            store("Collection", FieldStorage.YES);
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
                .whereStartsWith("ContactName", namePrefix)
                .whereStartsWith("ContactTitle", titlePrefix)
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
