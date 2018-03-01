package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldStorage;

public class CategoryDescription extends AbstractIndexCreationTask {

    public CategoryDescription() {
        map = "from category in docs.Categories " +
                "select new {" +
                "   Description = category.Description" +
                "}";

        store("Description", FieldStorage.YES);
        analyze("Description", "Lucene.Net.Analysis.Standard.StandardAnalyzer");
    }
}
