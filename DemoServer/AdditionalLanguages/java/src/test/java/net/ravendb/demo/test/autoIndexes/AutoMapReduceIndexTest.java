package net.ravendb.demo.test.autoIndexes;

import net.ravendb.demo.autoIndexes.autoMapReduceIndex.AutoMapReduceIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class AutoMapReduceIndexTest {

    @Test
    public void test() {
        List<AutoMapReduceIndex.CountryDetails> result =
            new AutoMapReduceIndex().run();

        Assert.assertNotNull(result);
        Assert.assertNotNull(result.get(0).getCountry());
        Assert.assertTrue(result.get(0).getNumberOfEmployees() > 0);
    }
}
