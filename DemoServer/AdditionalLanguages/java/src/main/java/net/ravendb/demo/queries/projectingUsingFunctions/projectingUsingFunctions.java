//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.IRawDocumentQuery;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import java.util.List;

public class projectingUsingFunctions {

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
            //region Step_1
            String rawQueryString = 
                "declare function output(employee) {\n" +
                "   var formatName  = function(employee) { return 'FullName: ' + employee.FirstName + ' ' + employee.LastName; };\n" +
                "   var formatTitle = function(employee) { return 'Title: ' + employee.Title };\n" +
                "   return { Title : formatTitle(employee), FullName : formatName(employee) };\n" +
                "}\n"+
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

