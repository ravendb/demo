package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.indexes.FieldTermVector;
import net.ravendb.demo.entities.Address;
import net.ravendb.demo.entities.Company;

public class CompaniesAndAddresses extends AbstractIndexCreationTask {

    public static class Result {
        private Company company;
        private Address address;

        public Company getCompany() {
            return company;
        }

        public void setCompany(Company company) {
            this.company = company;
        }

        public Address getAddress() {
            return address;
        }

        public void setAddress(Address address) {
            this.address = address;
        }
    }

    public CompaniesAndAddresses() {
        map = "from company in docs.Companies " +
                "select new { " +
                "  Company = company, " +
                "  Address = new string[] { " +
                "    company.Address.Line1, " +
                "    company.Address.Line2, " +
                "    company.Address.City, " +
                "    company.Address.Country " +
                "  } " +
                "}";

        index("address", FieldIndexing.SEARCH);
        store("address", FieldStorage.YES);
        termVector("address", FieldTermVector.WITH_POSITIONS_AND_OFFSETS);
    }
}
