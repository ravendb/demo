package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.LastFm;
import net.ravendb.demo.textSearch.fTSWithStaticIndexMultipleFields.FTSWithStaticIndexMultipleFields;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FTSWithStaticIndexMultipleFieldsTest {
    @Test
    public void test() {
        DocumentStoreHolder.mediaStore.executeIndex(new FTSWithStaticIndexMultipleFields.Song_TextData());

        FTSWithStaticIndexMultipleFields.RunParams runParams = new FTSWithStaticIndexMultipleFields.RunParams();
        runParams.setSearchTerm("Floyd");

        List<LastFm> results = new FTSWithStaticIndexMultipleFields().run(runParams);

        Assert.assertEquals(15, results.size());
    }
}
