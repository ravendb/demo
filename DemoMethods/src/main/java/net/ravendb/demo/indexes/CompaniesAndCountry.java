package net.ravendb.demo.indexes;

import net.ravendb.client.indexes.AbstractIndexCreationTask;
import net.ravendb.demo.entities.Company;

public class CompaniesAndCountry extends AbstractIndexCreationTask {

    public static class Result {
        private Company company;
        private String address_country;

        public String getAddress_country() {
            return address_country;
        }

        public void setAddress_country(String address_country) {
            this.address_country = address_country;
        }

        public Company getCompany() {
            return company;
        }

        public void setCompany(Company company) {
            this.company = company;
        }
    }

    public CompaniesAndCountry() {
        map = "from company in docs.Companies " +
                "select new { " +
                "  Company = company, " +
                "  Address_Country = company.Address.Country " +
                "}";
    }
}
