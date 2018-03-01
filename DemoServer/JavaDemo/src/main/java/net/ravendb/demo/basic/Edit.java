package net.ravendb.demo.basic;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class Edit {

    @GetMapping("/basic/edit")
    public String edit(
            @RequestParam (value = "companyId", defaultValue = "companies/1-A") String companyId) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            Company company = session.load(Company.class, companyId);
            company.getAddress().setLine2("Zip 12345");
            session.saveChanges();

            return String.format("%s Edited Successfully", companyId);
        }
    }
}
