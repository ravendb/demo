package net.ravendb.demo.indexes;

import net.ravendb.client.indexes.AbstractTransformerCreationTask;

public class TransformerLastFm extends AbstractTransformerCreationTask {

    public TransformerLastFm() {
        transformResults = "from result in results " +
                "select new { " +
                "  Artist = result.Artist, " +
                "  TimeStamp = result.TimeStamp, " +
                "  Tags = result.Tags, " +
                "  TrackId = result.TrackId, " +
                "  Title = result.Title " +
                "}";
    }

}
