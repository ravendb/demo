package net.ravendb.demo.advanced;

import net.ravendb.client.Parameters;
import net.ravendb.client.documents.operations.Operation;
import net.ravendb.client.documents.operations.PatchByQueryOperation;
import net.ravendb.client.documents.queries.IndexQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Order;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class SetBasedScripted {

    @GetMapping("/advanced/setBasedScripted")
    public List<Order> setBasedScripted(
            @RequestParam(value = "employee", defaultValue = "employees/1-A") String employee,
            @RequestParam(value = "discount", defaultValue = "5") int discount) {

        IndexQuery updateQuery = new IndexQuery();
        updateQuery.setQuery("FROM INDEX 'Orders/Totals' \n" +
                "WHERE Employee = $emp \n" +
                "UPDATE {\n" +
                "    for(var i = 0; i < this.Lines.length; i++)\n" +
                "    {\n" +
                "        this.Lines[i].Discount = Math.max(this.Lines[i].Discount || 0, args.discount);\n" +
                "    }\n" +
                "}");

        Parameters parameters = new Parameters();
        parameters.put("emp", employee);
        parameters.put("discount", discount);

        updateQuery.setQueryParameters(parameters);


        Operation updateByIndex = DocumentStoreHolder.getStore().operations().sendAsync(new PatchByQueryOperation(updateQuery));
        updateByIndex.waitForCompletion();

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            List<Order> results = session
                    .query(Order.class)
                    .waitForNonStaleResults()
                    .whereEquals("Employee", employee)
                    .toList();

            return results;
        }
    }
}
