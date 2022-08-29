package net.ravendb.demo.test.multiMapIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.multiMapIndexes.multiMapIndexCustomizedFields.MultiMapIndexCustomizedFields;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class MultiMapIndexCustomizedFieldsTest {

    @Test
    public void test() throws Exception {
        DocumentStoreHolder.store.executeIndex(new MultiMapIndexCustomizedFields.Contacts_ByNameAndTitle());

        List<MultiMapIndexCustomizedFields.Contacts_ByNameAndTitle.ProjectedEntry> list =
            new MultiMapIndexCustomizedFields().run(new MultiMapIndexCustomizedFields.RunParams());

        // The properties in the documents returned are null due to bug when using selectFields() together with
        // store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        Assert.assertEquals(3, list.size());
    }
}
