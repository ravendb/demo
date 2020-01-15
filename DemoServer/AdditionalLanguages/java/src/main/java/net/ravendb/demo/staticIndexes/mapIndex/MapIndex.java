package net.ravendb.demo.staticIndexes.mapIndex;

//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

public class MapIndex {
    //region Demo
    //region Step_1
    public static class Employees_ImportantDetails extends AbstractIndexCreationTask {
    //endregion
        //region Step_2
        public static class Result {
            private String fullName;
            private String country;
            private int workingInCompanySince;
            private int numberOfTerritories;

            public String getFullName() {
                return fullName;
            }

            public void setFullName(String fullName) {
                this.fullName = fullName;
            }

            public String getCountry() {
                return country;
            }

            public void setCountry(String country) {
                this.country = country;
            }

            public int getWorkingInCompanySince() {
                return workingInCompanySince;
            }

            public void setWorkingInCompanySince(int workingInCompanySince) {
                this.workingInCompanySince = workingInCompanySince;
            }

            public int getNumberOfTerritories() {
                return numberOfTerritories;
            }

            public void setNumberOfTerritories(int numberOfTerritories) {
                this.numberOfTerritories = numberOfTerritories;
            }
        }
        //endregion

        //region Step_3
        public Employees_ImportantDetails() {
            map = "docs.Employees.Select(employee => new { " +
                "    FullName = (employee.FirstName + \" \") + employee.LastName, " +
                "    Country = employee.Address.Country, " +
                "    WorkingInCompanySince = employee.HiredAt.Year, " +
                "    NumberOfTerritories = employee.Territories.Count " +
                "})";
        }
        //endregion
    }
    //endregion

    public List<Employee> run(RunParams runParams) {
        int startYear = runParams.getStartYear();

        //region Demo
        List<Employee> employeesFromUSA;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            employeesFromUSA = session.query(Employees_ImportantDetails.Result.class, Employees_ImportantDetails.class)
                .whereEquals("Country", "USA")
                .whereGreaterThan("WorkingInCompanySince", startYear)
                .ofType(Employee.class)
                .toList();
            //endregion
        }
        //endregion

        return employeesFromUSA;
    }

    public static class RunParams {
        private int startYear;

        public int getStartYear() {
            return startYear;
        }

        public void setStartYear(int startYear) {
            this.startYear = startYear;
        }
    }
}
