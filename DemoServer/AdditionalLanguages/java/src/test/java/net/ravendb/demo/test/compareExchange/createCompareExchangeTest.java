package net.ravendb.demo.test.compareExchange;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.compareExchange.createCompareExchange.CreateCompareExchange;
import net.ravendb.demo.javascriptIndexes.javascriptMapIndex.JavascriptMapIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class createCompareExchangeTest {
    @Test
    public void test() {
        CreateCompareExchange.RunParams runParams = new CreateCompareExchange.RunParams();
        runParams.setCmpXchgKey("abc@gmail.com");
        runParams.setCmpXchgValue("employee/1-A");
        String result = new CreateCompareExchange().run(runParams);
        Assert.assertNotNull(result);
    }
}
