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
            private String FullName;
            private String Country;
            private int WorkingInCompanySince;
            private int NumberOfTerritories;

            public int getNumberOfTerritories() {
                return NumberOfTerritories;
            }

            public void setNumberOfTerritories(int numberOfTerritories) {
                this.NumberOfTerritories = numberOfTerritories;
            }

            public int getWorkingInCompanySince() {
                return WorkingInCompanySince;
            }

            public void setWorkingInCompanySince(int workingInCompanySince) {
                this.WorkingInCompanySince = workingInCompanySince;
            }

            public String getCountry() {
                return Country;
            }

            public void setCountry(String country) {
                this.Country = country;
            }

            public String getFullName() {
                return FullName;
            }

            public void setFullName(String fullName) {
                this.FullName = fullName;
            }
        }
        //endregion
        //region Step_3
        public Employees_ByImportantDetailsJS() {
            setMaps(Sets.newHashSet(
                "map('Employees', function (employee) {\n" +
                "   return {\n" +
                "       FullName: employee.FirstName + ' ' + employee.LastName,\n" +
                "       Country: employee.Address.Country,\n" +
                "       WorkingInCompanySince: new Date(employee.HiredAt).getFullYear(),\n" +
                "       NumberOfTerritories: employee.Territories.length\n" +
                "   };\n" +
                "})"
            ));
        }
        //endregion
    }
    //endregion

    public List<Employee> run(RunParams runParams) {
        int startYear = runParams.getStartYear();
        new Employees_ByImportantDetailsJS().execute(DocumentStoreHolder.store);
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
