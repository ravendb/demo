package net.ravendb.demo.test;

import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.advanced.*;
import net.ravendb.demo.entities.LastFm;
import net.ravendb.demo.entities.Order;
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

        Assert.assertEquals(20, results.size());
    }

    @Test
    public void testSetBasedScripted() {
        SetBasedScripted setBasedScripted = new SetBasedScripted();
        List<Order> usa = setBasedScripted.setBasedScripted("employees/1-A", 5);

        Assert.assertTrue(usa.size() > 100);
    }

}
