package net.ravendb.demo.indexes;

import net.ravendb.client.indexes.AbstractTransformerCreationTask;

public class TransformerNameAndCountry extends AbstractTransformerCreationTask {

    public TransformerNameAndCountry() {
        transformResults = "from result in results " +
                "select new { " +
                "  Name = result.Name " +
                "}";
    }

}
