package net.ravendb.demo.autoIndexes.autoMapReduceIndex;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

public class AutoMapReduceIndex {

    //region Demo
    //region Step_1
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
    //endregion
    
    public List<CountryDetails> run() {
        //region Demo
        List<CountryDetails> numberOfEmployeesPerCountry;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {

            //region Step_2
            numberOfEmployeesPerCountry = session.query(Employee.class)
            //endregion
                //region Step_3
                .groupBy("Address.Country")
                //endregion
                //region Step_4
                .selectKey("Address.Country", "Country")
                .selectCount("NumberOfEmployees")
                //endregion
                //region Step_5
                .orderByDescending("NumberOfEmployees")
                //endregion
                //region Step_6
                .ofType(CountryDetails.class)
                //endregion
                //region Step_7
                .toList();
                //endregion
        }
        //endregion

        return numberOfEmployeesPerCountry;
    }
}
