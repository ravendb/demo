package net.ravendb.demo.test.util;

import com.google.common.base.Stopwatch;
import net.ravendb.client.Constants;
import net.ravendb.client.documents.IDocumentStore;
import net.ravendb.client.documents.indexes.IndexErrors;
import net.ravendb.client.documents.indexes.IndexState;
import net.ravendb.client.documents.operations.DatabaseStatistics;
import net.ravendb.client.documents.operations.GetStatisticsOperation;
import net.ravendb.client.documents.operations.IndexInformation;
import net.ravendb.client.documents.operations.MaintenanceOperationExecutor;
import net.ravendb.client.documents.operations.indexes.GetIndexErrorsOperation;
import net.ravendb.client.exceptions.TimeoutException;

import java.time.Duration;
import java.util.Arrays;
import java.util.List;
import java.util.concurrent.TimeUnit;
import java.util.function.Function;
import java.util.stream.Collectors;

public class TestUtils {
    public static void waitForIndexing(IDocumentStore store) {
        waitForIndexing(store, null, null);
    }

    public static void waitForIndexing(IDocumentStore store, String database) {
        waitForIndexing(store, database, null);
    }

    public static void waitForIndexing(IDocumentStore store, String database, Duration timeout) {
        waitForIndexing(store, database, timeout, null);
    }

    public static void waitForIndexing(IDocumentStore store, String database, Duration timeout, String nodeTag) {
        MaintenanceOperationExecutor admin = store.maintenance().forDatabase(database);

        if (timeout == null) {
            timeout = Duration.ofMinutes(1);
        }

        Stopwatch sp = Stopwatch.createStarted();

        while (sp.elapsed(TimeUnit.MILLISECONDS) < timeout.toMillis()) {
            DatabaseStatistics databaseStatistics = admin.send(new GetStatisticsOperation("wait-for-indexing", nodeTag));

            List<IndexInformation> indexes = Arrays.stream(databaseStatistics.getIndexes())
                .filter(x -> !IndexState.DISABLED.equals(x.getState()))
                .collect(Collectors.toList());

            if (indexes.stream().allMatch(x -> !x.isStale() &&
                !x.getName().startsWith(Constants.Documents.Indexing.SIDE_BY_SIDE_INDEX_NAME_PREFIX))) {
                return;
            }

            if (Arrays.stream(databaseStatistics.getIndexes()).anyMatch(x -> IndexState.ERROR.equals(x.getState()))) {
                break;
            }

            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }


        IndexErrors[] errors = admin.send(new GetIndexErrorsOperation());
        String allIndexErrorsText = "";
        Function<IndexErrors, String> formatIndexErrors = indexErrors -> {
            String errorsListText = Arrays.stream(indexErrors.getErrors()).map(x -> "-" + x).collect(Collectors.joining(System.lineSeparator()));
            return "Index " + indexErrors.getName() + " (" + indexErrors.getErrors().length + " errors): "+ System.lineSeparator() + errorsListText;
        };
        if (errors != null && errors.length > 0) {
            allIndexErrorsText = Arrays.stream(errors).map(formatIndexErrors).collect(Collectors.joining(System.lineSeparator()));
        }

        throw new TimeoutException("The indexes stayed stale for more than " + timeout + "." + allIndexErrorsText);
    }
}
