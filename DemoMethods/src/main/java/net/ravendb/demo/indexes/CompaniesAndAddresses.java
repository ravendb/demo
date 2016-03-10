package net.ravendb.demo.indexes;

import net.ravendb.abstractions.indexing.FieldIndexing;
import net.ravendb.abstractions.indexing.FieldStorage;
import net.ravendb.abstractions.indexing.FieldTermVector;
import net.ravendb.client.indexes.AbstractIndexCreationTask;
import net.ravendb.demo.entities.Address;
import net.ravendb.demo.entities.Company;
import net.ravendb.demo.entities.QCompany;

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
        QCompany company = QCompany.company;
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

        index(company.address(), FieldIndexing.ANALYZED);
        store(company.address(), FieldStorage.YES);
        termVector(company.address(), FieldTermVector.WITH_POSITIONS_AND_OFFSETS);
    }
}
