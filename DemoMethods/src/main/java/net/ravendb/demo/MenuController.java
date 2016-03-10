package net.ravendb.demo;

import net.ravendb.abstractions.data.Facet;
import net.ravendb.abstractions.data.FacetSetup;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.indexes.FacetRangeCreation;

import java.util.List;

public class MenuController {
    private static List<Facet> fixedFacet;

    public static List<Facet> getFixedFacet() {
        return fixedFacet;
    }

    public static void setFixedFacet(List<Facet> fixedFacet) {
        MenuController.fixedFacet = fixedFacet;
    }

    public void createFixedFacet() {
        double fromVal = 10;
        double toVal = 20;

        setFixedFacet(FacetRangeCreation.createFacets(fromVal, toVal));

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            session.store(new FacetSetup("facets/ProductFacet", getFixedFacet()));
            session.saveChanges();
        }
    }
}
