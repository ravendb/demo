package net.ravendb.demo.queries.projectingUsingFunctions;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.IRawDocumentQuery;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import java.util.List;

public class ProjectingUsingFunctions {

    //region Demo
    public static class EmployeeDetails {
        public String FullName;
        public String Title;
    }
    //endregion

    public List<EmployeeDetails> run() {
        List<EmployeeDetails> projectedResults;
        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {

            String rawQueryString = 
                //region Step_1
                "declare function output(employee) {" +
                "   var formatName  = function(employee) { return 'FullName: ' + employee.FirstName + ' ' + employee.LastName; };" +
                "   var formatTitle = function(employee) { return 'Title: ' + employee.Title };" +
                "   return { Title : formatTitle(employee), FullName : formatName(employee) };" +
                "}"+
                //endregion
                //region Step_2
                "from Employees as employee select output(employee)";
                //endregion
        
            //region Step_3
            IRawDocumentQuery<EmployeeDetails> projectedQueryWithFunctions = session.advanced()
                .rawQuery(EmployeeDetails.class, rawQueryString);
            //endregion
        
            //region Step_4
            projectedResults = projectedQueryWithFunctions.toList();
            //endregion
        //endregion
        }
        return projectedResults;
    }
}

