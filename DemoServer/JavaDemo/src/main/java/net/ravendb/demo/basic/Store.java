package net.ravendb.demo.basic;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class Store {

    @GetMapping("/basic/store")
    public String store()
    {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            Company company = new Company();
            company.setName("Hibernating Rhinos");
            company.setExternalId("HR");
            company.setPhone("+972 4 622 7811");
            company.setFax("+972 153 4 622 7811");

            session.store(company);
            session.saveChanges();
        }

        return "New Company Created Successfully";
    }
}
