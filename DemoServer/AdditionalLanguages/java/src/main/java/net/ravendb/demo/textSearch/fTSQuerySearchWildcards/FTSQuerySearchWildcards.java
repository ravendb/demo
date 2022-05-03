package net.ravendb.demo.textSearch.fTSQuerySearchWildcards;

//region Usings

import net.ravendb.client.documents.queries.SearchOperator;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.LastFm;

import java.util.List;

public class FTSQuerySearchWildcards {

    public List<LastFm> run(RunParams runParams) {
        String start = runParams.getStart();
        String end = runParams.getEnd();
        String middle = runParams.getMiddle();
        int numberOfResults = runParams.getNumberOfResults();

        //region Demo
        List<LastFm> songsWithMatchingTerms;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            songsWithMatchingTerms = session.query(LastFm.class)
            //endregion
                //region Step_2
                .search("Artist", start + "* *" + end, SearchOperator.AND)
                //endregion
                //region Step_3
                .search("Title", "*" + middle + "*")
                //endregion
                //region Step_4
                .take(numberOfResults)
                .orderBy("Artist")
                //endregion
                //region Step_5
                .toList();
                //endregion
        }
        //endregion
        return songsWithMatchingTerms;
    }

    public static class RunParams {
        private String start;
        private String end;
        private String middle;
        private int numberOfResults;


        public String getStart() {
            return start;
        }

        public void setStart(String start) {
            this.start = start;
        }

        public String getEnd() {
            return end;
        }

        public void setEnd(String end) {
            this.end = end;
        }

        public String getMiddle() {
            return middle;
        }

        public void setMiddle(String middle) {
            this.middle = middle;
        }

        public int getNumberOfResults() {
            return numberOfResults;
        }

        public void setNumberOfResults(int numberOfResults) {
            this.numberOfResults = numberOfResults;
        }
    }
}
