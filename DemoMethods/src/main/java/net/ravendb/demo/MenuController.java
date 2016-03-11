package net.ravendb.demo;

import net.ravendb.abstractions.data.Facet;
import net.ravendb.abstractions.data.FacetSetup;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.indexes.FacetRangeCreation;

import java.util.List;

public class MenuController {
    private static List<Facet> fixedFacet;

    public static List<Facet> getFixedFacet() {
        if (fixedFacet == null) {
            fixedFacet = FacetRangeCreation.createFacets(10, 20);
        }
        return fixedFacet;
    }

    public static void setFixedFacet(List<Facet> fixedFacet) {
        MenuController.fixedFacet = fixedFacet;
    }

    public void createFixedFacet() {
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            session.store(new FacetSetup("facets/ProductFacet", getFixedFacet()));
            session.saveChanges();
        }
    }
}
