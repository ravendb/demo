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
        public Employees_ImportantDetails() {
            map = "docs.Employees.Select(employee => new { " +
                "    fullName = (employee.FirstName + \" \") + employee.LastName, " +
                "    country = employee.Address.Country, " +
                "    workingInCompanySince = employee.HiredAt.Year, " +
                "    numberOfTerritories = employee.Territories.Count " +
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
            //region Step_3
            employeesFromUSA = session.query(Employee.class, Employees_ImportantDetails.class)
                .whereEquals("country", "USA")
                .whereGreaterThan("workingInCompanySince", startYear)
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
