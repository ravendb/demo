package net.ravendb.demo.autoIndexes.autoMapReduceIndex;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

import java.util.List;

public class AutoMapReduceIndex {
    //region Demo
    public static class CountryDetails {
        private String country;
        private int numberOfEmployees;

        public String getCountry() {
            return country;
        }

        public void setCountry(String country) {
            this.country = country;
        }

        public int getNumberOfEmployees() {
            return numberOfEmployees;
        }

        public void setNumberOfEmployees(int numberOfEmployees) {
            this.numberOfEmployees = numberOfEmployees;
        }
    }
    //endregion

    public List<CountryDetails> run() {

        /**
         * TODO:
         *
         * please update steps - Java syntax in slight different than c#
         */

        //region Demo
        List<CountryDetails> numberOfEmployeesPerCountry;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {

            numberOfEmployeesPerCountry = session.query(Employee.class)
                .groupBy("Address.Country")
                .selectKey("Address.Country", "Country")
                .selectCount("NumberOfEmployees")
                .orderByDescending("NumberOfEmployees")
                .ofType(CountryDetails.class)
                .toList();
        }
        //endregion

        return numberOfEmployeesPerCountry;
    }
}
