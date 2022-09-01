package net.ravendb.demo.javascriptIndexes.javascriptMapIndex;
//region using
import net.ravendb.client.documents.indexes.AbstractJavaScriptIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import com.google.common.collect.Sets;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.common.DocumentStoreHolder;
import java.util.List;

public class JavascriptMapIndex {
    //region Demo
    //region Step_1
    public static class Employees_ByImportantDetailsJS extends AbstractJavaScriptIndexCreationTask {
    //endregion
        //region Step_2
        public static class IndexEntry {
            private String fullName;
            private String country;
            private int workingInCompanySince;
            private int numberOfTerritories;

            public int getNumberOfTerritories() {
                return numberOfTerritories;
            }

            public void setNumberOfTerritories(int numberOfTerritories) {
                numberOfTerritories = numberOfTerritories;
            }

            public int getWorkingInCompanySince() {
                return workingInCompanySince;
            }

            public void setWorkingInCompanySince(int workingInCompanySince) {
                this.workingInCompanySince = workingInCompanySince;
            }

            public String getCountry() {
                return country;
            }

            public void setCountry(String country) {
                this.country = country;
            }

            public String getFullName() {
                return fullName;
            }

            public void setFullName(String fullName) {
                this.fullName = fullName;
            }
        }
        //endregion
        //region Step_3
        public Employees_ByImportantDetailsJS() {
            setMaps(Sets.newHashSet(
                "map('Employees', function (employee) {" +
                "   return {" +
                "       FullName: employee.FirstName + ' ' + employee.LastName," +
                "       Country: employee.Address.Country," +
                "       WorkingInCompanySince: new Date(employee.HiredAt).getFullYear()," +
                "       NumberOfTerritories: employee.Territories.length" +
                "   };" +
                "})"
            ));
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
            employeesFromUSA = session.query(Employees_ByImportantDetailsJS.IndexEntry.class, Employees_ByImportantDetailsJS.class)
                .whereEquals("Country", "USA")
                .whereGreaterThan("WorkingInCompanySince", startYear)
                .selectFields(Employee.class)
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
