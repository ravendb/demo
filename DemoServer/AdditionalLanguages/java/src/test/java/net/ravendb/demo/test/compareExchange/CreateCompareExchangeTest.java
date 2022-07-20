package net.ravendb.demo.test.compareExchange;

import net.ravendb.demo.compareExchange.createCompareExchange.CreateCompareExchange;
import org.junit.Assert;
import org.junit.Test;

public class CreateCompareExchangeTest {
    @Test
    public void test() {
        CreateCompareExchange.RunParams runParams = new CreateCompareExchange.RunParams();
        String result = new CreateCompareExchange().run(runParams);

        Assert.assertNotNull(result);
    }
}
