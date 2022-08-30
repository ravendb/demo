package net.ravendb.demo.queries.sortingQueryResults;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.OrderingType;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;

public class SortingQueryResults {
    public List<Product> run(RunParams runParams) {
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

        return sortedProducts;
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
