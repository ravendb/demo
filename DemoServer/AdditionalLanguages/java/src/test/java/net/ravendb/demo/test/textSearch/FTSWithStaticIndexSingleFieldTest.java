package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Category;
import net.ravendb.demo.textSearch.fTSWithStaticIndexSingleField.FTSWithStaticIndexSingleField;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FTSWithStaticIndexSingleFieldTest {
    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new FTSWithStaticIndexSingleField.Categories_DescriptionText());

        FTSWithStaticIndexSingleField.RunParams runParams = new FTSWithStaticIndexSingleField.RunParams();
        runParams.setSearchTerm("pasta");

        List<Category> categories = new FTSWithStaticIndexSingleField().run(runParams);

        Assert.assertEquals(1, categories.size());
    }
}
