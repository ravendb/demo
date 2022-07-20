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
            private int WorkingInCompanySince;

            public int getWorkingInCompanySince() {
                return WorkingInCompanySince;
            }

            public void setWorkingInCompanySince(int workingInCompanySince) {
                this.WorkingInCompanySince = workingInCompanySince;
            }
        }
        //endregion

        //region Step_3
        public static class EmployeeProjectedDetails {
            private String FirstName;
            private String Phone;
            private String Location;

            public String getFirstName() {
                return FirstName;
            }

            public void setFirstName(String firstName) {
                this.FirstName = firstName;
            }

            public String getPhone() {
                return Phone;
            }

            public void setPhone(String phone) {
                this.Phone = phone;
            }

            public String getLocation() {
                return Location;
            }

            public void setLocation(String location) {
                this.Location = location;
            }
        }
        //endregion

        //region Step_4
        public Employees_ByWorkPeriod() {
            map =
                "docs.Employees.Select(employee => new {\n" +
                "      WorkingInCompanySince = employee.HiredAt.Year\n" +
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
                .whereGreaterThan("WorkingInCompanySince", startYear)
                //endregion
                //region Step_6
                .selectFields(Employees_ByWorkPeriod.EmployeeProjectedDetails.class,
                    QueryData.customFunction("employee ",
                        "{ FirstName: employee.FirstName,\n" +
                        "  Phone: employee.HomePhone,\n" +
                        "  Location: employee.Address.City + ' ' + employee.Address.Country }"));
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
