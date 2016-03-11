package net.ravendb.demo.test;

import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.advanced.*;
import net.ravendb.demo.entities.Company;
import net.ravendb.demo.entities.LastFm;
import org.junit.Assert;
import org.junit.BeforeClass;
import org.junit.Test;

import java.util.List;

public class AdvancedTest {
    @BeforeClass
    public static void setup() {
        DocumentStoreHolder.setDbInfo("http://localhost:8080", "Demo", "Media");
    }


    @Test
    public void testFullTextSearch() {
        FullTextSearch fullTextSearch = new FullTextSearch();
        List<LastFm> results = fullTextSearch.fullTextSearch("Jazz");

        Assert.assertTrue(results.size() > 100);
    }

    @Test
    public void testStreamingApi() {
        StreamingApi streamingApi = new StreamingApi();
        int count = streamingApi.streamingApi();
        Assert.assertEquals(830, count);
    }

    @Test
    public void testLazyFunctionality() {
        LazyFunctionality lazyFunctionality = new LazyFunctionality();
        LazyFunctionality.Result result = lazyFunctionality.lazyFunctionality("companies/20");

        Assert.assertEquals("Ernst Handel", result.getCompanyName());
        Assert.assertEquals(30, result.getNumberOfOrders());
        Assert.assertEquals(1, result.getNumberOfRequests());
    }

    @Test
    public void testSetBased() {
        SetBased setBased = new SetBased();
        List<Company> usa = setBased.setBased("USA", "United States of America");

        Assert.assertEquals(13, usa.size());
    }

    @Test
    public void testSetBasedScripted() {
        SetBasedScripted setBasedScripted = new SetBasedScripted();
        List<Company> usa = setBasedScripted.setBasedScripted("USA", "United States of America");

        Assert.assertEquals(13, usa.size());
    }

}
