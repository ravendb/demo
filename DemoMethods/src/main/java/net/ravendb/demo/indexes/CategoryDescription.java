package net.ravendb.demo.indexes;

import net.ravendb.abstractions.indexing.FieldStorage;
import net.ravendb.client.indexes.AbstractIndexCreationTask;
import net.ravendb.demo.entities.QCategory;

public class CategoryDescription extends AbstractIndexCreationTask {

    public CategoryDescription() {
        QCategory category = QCategory.category;

        map = "from category in docs.Categories " +
                "select new {" +
                "   Description = category.Description" +
                "}";

        store(category.description, FieldStorage.YES);
        analyze(category.description, "Lucene.Net.Analysis.Standard.StandardAnalyzer");
    }
}
