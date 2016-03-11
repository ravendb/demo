package net.ravendb.demo.basic;

import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class Delete {

    @RequestMapping("/Basic/Delete")
    public String delete(
            @RequestParam (value = "companyId", defaultValue = "2") int companyId) {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            session.delete(session.load(Company.class, companyId));
            session.saveChanges();

            return String.format("Company %s Deleted Successfully", companyId);
        }
    }
}
