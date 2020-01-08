package net.ravendb.demo.queries.sortingQueryResults;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.OrderingType;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;

import java.util.List;

public class SortingQueryResults {
    public void run(RunParams runParams) {
        double numberOfUnits = runParams.getNumberOfUnits();

        //region Demo
        List<Product> sortedProducts;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            sortedProducts = session.query(Product.class)
            //endregion
                //region Step_2
                .whereGreaterThan("UnitsInStock", numberOfUnits)
                //endregion
                //region Step_3
                .orderByDescending("UnitsInStock")
                //endregion
                //region Step_4
                .orderBy("Name", OrderingType.ALPHA_NUMERIC)
                //endregion
                //region Step_5
                .toList();
                //endregion
        }
        //endregion
    }

    public static class RunParams {
        private double numberOfUnits;

        public double getNumberOfUnits() {
            return numberOfUnits;
        }

        public void setNumberOfUnits(double numberOfUnits) {
            this.numberOfUnits = numberOfUnits;
        }
    }
}
