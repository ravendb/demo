package net.ravendb.demo.queries.pagingQueryResults;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;

public class PagingQueryResults {

    public List<Company> run(RunParams runParams) {
        int resultsToSkip = runParams.getResultsToSkip();
        int resultsToTake = runParams.getResultsToTake();

        //region Demo
        List<Company> pagedResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            Reference<QueryStatistics> statsRef = new Reference<>();

            //region Step_1
            pagedResults = session.query(Company.class)
            //endregion
                //region Step_2
                .statistics(statsRef)
                //endregion
                //region Step_3
                .skip(resultsToSkip)
                //endregion
                //region Step_4
                .take(resultsToTake)
                //endregion
                //region Step_5
                .toList();
                //endregion

            //region Step_6
            int totalResults = statsRef.value.getTotalResults();
            //endregion
        }
        //endregion

        return pagedResults;
    }

    public static class RunParams {
        private int resultsToSkip;
        private int resultsToTake;

        public int getResultsToSkip() {
            return resultsToSkip;
        }

        public void setResultsToSkip(int resultsToSkip) {
            this.resultsToSkip = resultsToSkip;
        }

        public int getResultsToTake() {
            return resultsToTake;
        }

        public void setResultsToTake(int resultsToTake) {
            this.resultsToTake = resultsToTake;
        }
    }
}
