package net.ravendb.demo.advanced;

import com.google.common.collect.ImmutableMap;
import net.ravendb.abstractions.data.IndexQuery;
import net.ravendb.abstractions.data.PatchCommandType;
import net.ravendb.abstractions.data.PatchRequest;
import net.ravendb.abstractions.data.ScriptedPatchRequest;
import net.ravendb.abstractions.json.linq.RavenJToken;
import net.ravendb.client.IDocumentSession;
import net.ravendb.client.connection.Operation;
import net.ravendb.client.document.DocumentQueryCustomizationFactory;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Company;
import net.ravendb.demo.indexes.CompaniesAndCountry;
import net.ravendb.demo.indexes.QCompaniesAndCountry_Result;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class SetBasedScripted {

    @RequestMapping("/Advanced/SetBasedScripted")
    public List<Company> setBasedScripted(
            @RequestParam(value = "original", defaultValue = "USA") String original,
            @RequestParam(value = "newVal", defaultValue = "United States of America") String newVal) {

        ScriptedPatchRequest scriptedPatchRequest = new ScriptedPatchRequest();
        scriptedPatchRequest.setScript("this.Address.Country = newVal");
        scriptedPatchRequest.setValues(ImmutableMap.of("newVal", newVal));


        Operation updateByIndex = DocumentStoreHolder.getStore()
                .getDatabaseCommands()
                .updateByIndex(new CompaniesAndCountry().getIndexName(),
                               new IndexQuery("Address_Country:" + original),
                        scriptedPatchRequest);

        updateByIndex.waitForCompletion();

        QCompaniesAndCountry_Result r = QCompaniesAndCountry_Result.result;

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            List<Company> results = session
                    .query(CompaniesAndCountry.Result.class, CompaniesAndCountry.class)
                    .customize(new DocumentQueryCustomizationFactory().waitForNonStaleResults())
                    .where(r.address_Country.eq(newVal))
                    .as(Company.class)
                    .toList();

            return results;
        }
    }
}
