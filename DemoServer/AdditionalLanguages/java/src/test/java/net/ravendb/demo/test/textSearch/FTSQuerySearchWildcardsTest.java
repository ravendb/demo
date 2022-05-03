package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.common.models.LastFm;
import net.ravendb.demo.textSearch.fTSQuerySearchWildcards.FTSQuerySearchWildcards;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FTSQuerySearchWildcardsTest {
    @Test
    public void test() {
        FTSQuerySearchWildcards.RunParams runParams = new FTSQuerySearchWildcards.RunParams();

        runParams.setStart("ma");
        runParams.setEnd("lin");
        runParams.setMiddle("oliv");
        runParams.setNumberOfResults(10);


        List<LastFm> fmList = new FTSQuerySearchWildcards().run(runParams);
        Assert.assertNotNull(fmList);
    }
}
