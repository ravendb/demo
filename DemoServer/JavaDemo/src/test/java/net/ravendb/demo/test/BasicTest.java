package net.ravendb.demo.test;

import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.basic.*;
import net.ravendb.demo.entities.Company;
import net.ravendb.demo.entities.Order;
import net.ravendb.demo.indexes.ProductSales;
import org.junit.Assert;
import org.junit.BeforeClass;
import org.junit.Test;

import java.util.List;

public class BasicTest {

    @BeforeClass
    public static void setup() {
        DocumentStoreHolder.setDbInfo("http://localhost:8080", "Demo", "Media");
    }

    @Test
    public void testStore() {
        Store store = new Store();
        String result = store.store();
        Assert.assertNotNull(result);
    }

    @Test
    public void testEdit() {
        Edit edit = new Edit();
        String result = edit.edit("companies/1-A");
        Assert.assertNotNull(result);
    }

    @Test
    public void testDelete() {
        Delete delete = new Delete();
        delete.delete("companies/2-A");
    }

    @Test
    public void testQuery1() {
        Query1 query1 = new Query1();
        Company company = query1.query1("UK");
        Assert.assertEquals("UK", company.getAddress().getCountry());
    }

    @Test
    public void testQuery2() {
        Query2 query2 = new Query2();
        Company company = query2.query2("ALFKI");
        Assert.assertEquals("ALFKI", company.getExternalId());
    }

    @Test
    public void testQuery3() {
        Query3 query3 = new Query3();
        Company berlin = query3.query3("Berlin");
        Assert.assertEquals("Berlin", berlin.getAddress().getCity());
    }

    @Test
    public void testStaticQuery() {
        StaticQuery staticQuery = new StaticQuery();
        List<Order> orders = staticQuery.staticQuery("500", "35");
        Assert.assertEquals(1, orders.size());
    }

    @Test
    public void testMultiMapIndexingQuery() {
        MultiMapIndexingQuery multiMapIndexingQuery = new MultiMapIndexingQuery();
        List<MultiMapIndexingQuery.IdAndName> result = multiMapIndexingQuery.multiMapIndexingQuery("USA");
        Assert.assertNotNull(result);
    }

    @Test
    public void testMapReduceUsingDocumentQuery() {
        MapReduceUsingDocumentQuery mapReduceUsingDocumentQuery = new MapReduceUsingDocumentQuery();
        List<ProductSales.Result> results = mapReduceUsingDocumentQuery.mapReduceUsingDocumentQuery();
        Assert.assertTrue(results.size() > 10);
    }

    @Test
    public void testBootingEnabled() {
        BoostingEnabled boostingEnabled = new BoostingEnabled();
        List<Order> orders = boostingEnabled.boostingEnabled("London", "Denmark");
        Assert.assertTrue(orders.size() > 0);
    }

    @Test
    public void testBootingDisabled() {
        BoostingDisabled boostingDisabled = new BoostingDisabled();
        List<Order> orders = boostingDisabled.boostingDisabled("London", "Denmark");
        Assert.assertTrue(orders.size() > 0);
    }
}
