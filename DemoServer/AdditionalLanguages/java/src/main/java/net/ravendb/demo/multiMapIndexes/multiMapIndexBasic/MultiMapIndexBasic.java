package net.ravendb.demo.multiMapIndexes.multiMapIndexBasic;

import net.ravendb.client.documents.indexes.AbstractMultiMapIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

import java.util.List;

public class MultiMapIndexBasic {

    public static class CompaniesAndSuppliers_ByName extends AbstractMultiMapIndexCreationTask {

        public static class IndexEntry {
            private String name;

            public String getName() {
                return name;
            }

            public void setName(String name) {
                this.name = name;
            }
        }

        public CompaniesAndSuppliers_ByName() {
            this.addMap("docs.Companies.Select(company => new {\n" +
                "    Name = company.Name\n" +
                "})");

            this.addMap("docs.Suppliers.Select(supplier => new {\n" +
                "    Name = supplier.Name\n" +
                "})");
        }
    }

    public List<CompaniesAndSuppliers_ByName.IndexEntry> run(RunParams runParams) {
        String namePrefix = ObjectUtils.firstNonNull(runParams.getNamePrefix(), "A");

        List<CompaniesAndSuppliers_ByName.IndexEntry> companiesAndSuppliersNames;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            companiesAndSuppliersNames = session.query(CompaniesAndSuppliers_ByName.IndexEntry.class, CompaniesAndSuppliers_ByName.class)
                .whereStartsWith("Name", namePrefix)
                .toList();
        }

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
