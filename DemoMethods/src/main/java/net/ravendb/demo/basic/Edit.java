package net.ravendb.demo.basic;

import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class Edit {

    @RequestMapping("/Basic/Edit")
    public String edit(@RequestParam (value = "companyId", defaultValue = "1") int companyId) {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            Company company = session.load(Company.class, companyId);
            company.getAddress().setLine2("Zip 12345");
            session.saveChanges();

            return String.format("Company %s Edited Successfully", companyId);
        }
    }
}
