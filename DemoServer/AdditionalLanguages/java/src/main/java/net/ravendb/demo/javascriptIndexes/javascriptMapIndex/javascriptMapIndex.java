

import net.ravendb.client.documents.indexes.AbstractJavaScriptIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;

import net.ravendb.demo.common.DocumentStoreHolder;
import java.util.List;
import com.google.common.collect.Sets;

public class javascriptMapIndex {
    //region Demo
    //region Step_1
    public class Employees_ByImportantDetailsJS extends AbstractJavaScriptIndexCreationTask {
    //endregion
        //region Step_2
        public class IndexEntry {
            public String FullName;
            public String Country;
            public int    WorkingInCompanySince;
            public int    NumberOfTerritories;
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

    public void run( int startYear ){
        //region Demo
        List<Test.Employee> employeesFromUSA;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            employeesFromUSA = session.query(Employees_ByImportantDetailsJS.IndexEntry.class, Employees_ByImportantDetailsJS.class)
                .whereEquals("Country","USA")
                .whereGreaterThan("WorkingInCompanySince", startYear)
                .selectFields(Test.Employee.class)
                .toList();
            //endregion
        }
        //endregion
    }
}
