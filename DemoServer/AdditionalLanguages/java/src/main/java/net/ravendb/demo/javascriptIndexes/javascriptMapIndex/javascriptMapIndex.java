//region using
import net.ravendb.client.documents.indexes.AbstractJavaScriptIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.common.DocumentStoreHolder;

import java.util.List;

import com.google.common.collect.Sets;

public class JavascriptMapIndex {
    //region Demo
    //region Step_1
    public class Employees_ByImportantDetailsJS extends AbstractJavaScriptIndexCreationTask {
        //endregion
        //region Step_2
        public class IndexEntry {
            String fullName;
            String country;
            int workingInCompanySince;
            int numberOfTerritories;

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

    public void run(int startYear) {
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
    }
}
