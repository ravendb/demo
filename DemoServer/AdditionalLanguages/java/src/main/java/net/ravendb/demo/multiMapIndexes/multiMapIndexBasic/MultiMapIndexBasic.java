package net.ravendb.demo.multiMapIndexes.multiMapIndexBasic;

//region Usings
import net.ravendb.client.documents.indexes.AbstractMultiMapIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion

import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

public class MultiMapIndexBasic {

    //region Demo
    //region Step_1
    public static class CompaniesAndSuppliers_ByName extends AbstractMultiMapIndexCreationTask {
    //endregion
    
        //region Step_2
        public static class IndexEntry {
            private String Name;

            public String getName() {
                return Name;
            }

            public void setName(String name) {
                this.Name = name;
            }
        }
        //endregion

        //region Step_3
        public CompaniesAndSuppliers_ByName() {
            addMap("docs.Companies.Select(company => new {" +
                "    Name = company.Name" +
                "})");

            addMap("docs.Suppliers.Select(supplier => new {" +
                "    Name = supplier.Name" +
                "})");
        }
        //endregion
    }
    //endregion

    public List<CompaniesAndSuppliers_ByName.IndexEntry> run(RunParams runParams) {
        String namePrefix = ObjectUtils.firstNonNull(runParams.getNamePrefix(), "A");

        //region Demo
        List<CompaniesAndSuppliers_ByName.IndexEntry> companiesAndSuppliersNames;

        //region Step_4
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            companiesAndSuppliersNames = session.query(CompaniesAndSuppliers_ByName.IndexEntry.class, CompaniesAndSuppliers_ByName.class)
                .whereStartsWith("Name", namePrefix)
                .toList();
        }
        //endregion
        //endregion

        return companiesAndSuppliersNames;
    }

    public static class RunParams {
        private String namePrefix;

        public String getNamePrefix() {
            return namePrefix;
        }

        public void setNamePrefix(String namePrefix) {
            this.namePrefix = namePrefix;
        }
    }
}
