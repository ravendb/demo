package net.ravendb.demo.test.facetedSearch;

import net.ravendb.demo.facetedSearch.facetsBasics.FacetsBasics;
import net.ravendb.demo.facetedSearch.facetsFromDocument.FacetsFromDocument;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FacetsFromDocumentTest {

    @Test
    public void test() {
        FacetsFromDocument.RunParams runParams = new FacetsFromDocument.RunParams();
        List<FacetsFromDocument.MyFacetResult> results =
            new FacetsFromDocument().run(runParams);

        Assert.assertNotNull(results);
    }

}
