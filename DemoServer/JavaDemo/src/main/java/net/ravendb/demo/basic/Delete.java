package net.ravendb.demo.basic;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class Delete {

    @GetMapping("/basic/delete")
    public String delete(
            @RequestParam (value = "companyId", defaultValue = "companies/2-A") String companyId) {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            session.delete(session.load(Company.class, companyId));
            session.saveChanges();

            return String.format("%s Deleted Successfully", companyId);
        }
    }
}
