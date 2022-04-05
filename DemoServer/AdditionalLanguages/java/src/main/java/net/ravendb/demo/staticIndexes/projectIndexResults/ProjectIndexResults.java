package net.ravendb.demo.staticIndexes.projectIndexResults;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.queries.QueryData;
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;

import org.junit.Test;

import java.util.List;

public class ProjectIndexResults {
    public static class Employees_ByWorkPeriod extends AbstractIndexCreationTask {
        public static class IndexEntry {
            private int workingInCompanySince;

            public int getWorkingInCompanySince() {
                return workingInCompanySince;
            }

            public void setWorkingInCompanySince(int workingInCompanySince) {
                this.workingInCompanySince = workingInCompanySince;
            }
        }

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

        public Employees_ByWorkPeriod() {
            map =
                "docs.Employees.Select(employee => new {\n" +
                    "      WorkingInCompanySince = employee.HiredAt.Year\n" +
                    "   })";
        }
    }


    public List<Employees_ByWorkPeriod.EmployeeProjectedDetails> run(RunParams runParams) {
        int startYear = runParams.getStartYear();


        //region Demo
        List<Employees_ByWorkPeriod.EmployeeProjectedDetails> employeesSinceYear;
        new Employees_ByWorkPeriod().execute(DocumentStoreHolder.store);

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {

            QueryData queryData = QueryData.customFunction("employee ",
                "{ FirstName: employee.FirstName,\n" +
                "   Phone: employee.HomePhone,\n" +
                "   Location: employee.Address.City + ' ' + employee.Address.Country}");
            //region Step_3
            IDocumentQuery<Employees_ByWorkPeriod.EmployeeProjectedDetails> employeesQuery = session
                .query(Employees_ByWorkPeriod.IndexEntry.class, Employees_ByWorkPeriod.class)
                //.whereGreaterThan("WorkingInCompanySince", startYear)
                .selectFields(Employees_ByWorkPeriod.EmployeeProjectedDetails.class, queryData).whereGreaterThan("WorkingInCompanySince", startYear);

            employeesSinceYear = employeesQuery.toList();
            //endregion

        }
        //endregion

        return employeesSinceYear;
        //return null;
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

    public String run(String s) {
        return s;
    }
}
