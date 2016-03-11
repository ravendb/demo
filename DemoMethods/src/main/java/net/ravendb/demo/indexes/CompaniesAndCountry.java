package net.ravendb.demo.indexes;

import com.mysema.query.annotations.QueryEntity;
import net.ravendb.client.indexes.AbstractIndexCreationTask;
import net.ravendb.demo.entities.Company;

public class CompaniesAndCountry extends AbstractIndexCreationTask {

    @QueryEntity
    public static class Result {
        private Company company;
        private String address_Country;

        public String getAddress_Country() {
            return address_Country;
        }

        public void setAddress_Country(String address_Country) {
            this.address_Country = address_Country;
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
