package net.ravendb.demo.staticIndexes.mapReduceIndex;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;

public class MapReduceIndex {

    //region Demo
    //region Step_1
    public static class Employees_ByCountry extends AbstractIndexCreationTask {
    //endregion

        public Employees_ByCountry() {
            //region Step_2
            map = "docs.Employees.Select(employee => new { " +
                "    Country = employee.Address.Country, " +
                "    CountryCount = 1 " +
                "})";
            //endregion

            //region Step_3
            reduce = "results.GroupBy(result => result.Country).Select(g => new { " +
                "    Country = g.Key, " +
                "    CountryCount = Enumerable.Sum(g, x => x.CountryCount) " +
                "})";
            //endregion
        }

        //region Step_4
        public static class Result {
            private String country;
            private int countryCount;

            public String getCountry() {
                return country;
            }

            public void setCountry(String country) {
                this.country = country;
            }

            public int getCountryCount() {
                return countryCount;
            }

            public void setCountryCount(int countryCount) {
                this.countryCount = countryCount;
            }
        }
        //endregion
    }
    //endregion

    public void run(RunParams runParams) {
        String country = runParams.getCountry();
        int numberOfEmployeesFromCountry;

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_5
            Employees_ByCountry.Result queryResult = session.query(Employees_ByCountry.Result.class, Employees_ByCountry.class)
                .whereEquals("Country", country)
                .firstOrDefault();

            numberOfEmployeesFromCountry = queryResult != null ? queryResult.getCountryCount() : 0;
            //endregion
        }
        //endregion
    }
    
    public static class RunParams {
        private String country;

        public String getCountry() {
            return country;
        }

        public void setCountry(String country) {
            this.country = country;
        }
    }
}
