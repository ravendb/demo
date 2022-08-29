package net.ravendb.demo.staticIndexes.projectIndexResults;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.queries.QueryData;
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import java.util.List;


public class ProjectIndexResults {
    //region Demo
    //region Step_1
    public static class Employees_ByWorkPeriod extends AbstractIndexCreationTask {
    //endregion
        //region Step_2
        public static class IndexEntry {
            private int workingInCompanySince;

            public int getWorkingInCompanySince() {
                return workingInCompanySince;
            }

            public void setWorkingInCompanySince(int workingInCompanySince) {
                this.workingInCompanySince = workingInCompanySince;
            }
        }
        //endregion

        //region Step_3
        public static class EmployeeProjectedDetails {
            private String firstName;
            private String phone;
            private String location;

            public String getFirstName() {
                return firstName;
            }

            public void setFirstName(String firstName) {
                this.firstName = firstName;
            }

            public String getPhone() {
                return phone;
            }

            public void setPhone(String phone) {
                this.phone = phone;
            }

            public String getLocation() {
                return location;
            }

            public void setLocation(String location) {
                this.location = location;
            }
        }
        //endregion

        //region Step_4
        public Employees_ByWorkPeriod() {
            map =
                "docs.Employees.Select(employee => new {" +
                "      workingInCompanySince = employee.HiredAt.Year" +
                "})";
        }
        //endregion
    }
    //endregion


    public List<Employees_ByWorkPeriod.EmployeeProjectedDetails> run(RunParams runParams) {
        int startYear = runParams.getStartYear();

        new Employees_ByWorkPeriod().execute(DocumentStoreHolder.store);
        //region Demo
        List<Employees_ByWorkPeriod.EmployeeProjectedDetails> employeesSinceYear;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            IDocumentQuery<Employees_ByWorkPeriod.EmployeeProjectedDetails> employeesQuery = session
                //region Step_5
                .query(Employees_ByWorkPeriod.IndexEntry.class, Employees_ByWorkPeriod.class)
                .whereGreaterThan("workingInCompanySince", startYear)
                //endregion
                //region Step_6
                .selectFields(Employees_ByWorkPeriod.EmployeeProjectedDetails.class,
                    QueryData.customFunction("employee ",
                        "{ firstName: employee.FirstName," +
                        "  phone: employee.HomePhone," +
                        "  location: employee.Address.City + ' ' + employee.Address.Country }"));
                //endregion

            //region Step_7
            employeesSinceYear = employeesQuery.toList();
            //endregion
        }
        //endregion

        return employeesSinceYear;
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
