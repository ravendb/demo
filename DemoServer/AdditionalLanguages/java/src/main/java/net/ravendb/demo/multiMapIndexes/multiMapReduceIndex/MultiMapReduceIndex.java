package net.ravendb.demo.multiMapIndexes.multiMapReduceIndex;

//region Usings
import net.ravendb.client.documents.indexes.AbstractMultiMapIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion

import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

public class MultiMapReduceIndex {

    //region demo
    //region Step_1
    public static class CityCommerceDetails extends AbstractMultiMapIndexCreationTask {
    //endregion
        //region Step_2
        public static class IndexEntry {
            private String cityName;
            private int numberOfCompaniesInCity;
            private int numberOfSuppliersInCity;
            private int numberOfItemsShippedToCity;

            public String getCityName() {
                return cityName;
            }

            public void setCityName(String cityName) {
                this.cityName = cityName;
            }

            public int getNumberOfCompaniesInCity() {
                return numberOfCompaniesInCity;
            }

            public void setNumberOfCompaniesInCity(int numberOfCompaniesInCity) {
                this.numberOfCompaniesInCity = numberOfCompaniesInCity;
            }

            public int getNumberOfSuppliersInCity() {
                return numberOfSuppliersInCity;
            }

            public void setNumberOfSuppliersInCity(int numberOfSuppliersInCity) {
                this.numberOfSuppliersInCity = numberOfSuppliersInCity;
            }

            public int getNumberOfItemsShippedToCity() {
                return numberOfItemsShippedToCity;
            }

            public void setNumberOfItemsShippedToCity(int numberOfItemsShippedToCity) {
                this.numberOfItemsShippedToCity = numberOfItemsShippedToCity;
            }
        }
        //endregion
        
        public CityCommerceDetails() {
            //region Step_3
            addMap("docs.Companies.Select(company => new {" +
                "    cityName = company.Address.City," +
                "    numberOfCompaniesInCity = 1," +
                "    numberOfSuppliersInCity = 0," +
                "    numberOfItemsShippedToCity = 0" +
                "})");

            addMap("docs.Suppliers.Select(supplier => new {" +
                "    cityName = supplier.Address.City," +
                "    numberOfCompaniesInCity = 0," +
                "    numberOfSuppliersInCity = 1," +
                "    numberOfItemsShippedToCity = 0" +
                "})");

            addMap("docs.Orders.Select(order => new {" +
                "    cityName = order.ShipTo.City," +
                "    numberOfCompaniesInCity = 0," +
                "    numberOfSuppliersInCity = 0," +
                "    numberOfItemsShippedToCity = Enumerable.Sum(order.Lines, x => ((int) x.Quantity))" +
                "})");
            //endregion
            //region Step_4
            this.reduce = "results.GroupBy(result => result.cityName).Select(g => new {" +
                "    cityName = g.Key," +
                "    numberOfCompaniesInCity = Enumerable.Sum(g, x => ((int) x.numberOfCompaniesInCity))," +
                "    numberOfSuppliersInCity = Enumerable.Sum(g, x0 => ((int) x0.numberOfSuppliersInCity))," +
                "    numberOfItemsShippedToCity = Enumerable.Sum(g, x1 => ((int) x1.numberOfItemsShippedToCity))" +
                "})";
            //endregion
        }
    }
    //endregion

    public List<CityCommerceDetails.IndexEntry> run(RunParams runParams) {
        int minCompaniesCount = ObjectUtils.firstNonNull(runParams.getMinCompaniesCount(), 5);
        int minItemsCount = ObjectUtils.firstNonNull(runParams.getMinItemsCount(), 2000);
        //region Demo
        //region Step_5
        List<CityCommerceDetails.IndexEntry> commerceDetails;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {

            commerceDetails = session.query(CityCommerceDetails.IndexEntry.class, CityCommerceDetails.class)
                .whereGreaterThan("numberOfCompaniesInCity", minCompaniesCount)
                .orElse()
                .whereGreaterThan("numberOfItemsShippedToCity", minItemsCount)
                .orderBy("cityName")
                .toList();
        }
        //endregion
        //endregion

        return commerceDetails;
    }

    public static class RunParams {
        private Integer minCompaniesCount;
        private Integer minItemsCount;

        public Integer getMinCompaniesCount() {
            return minCompaniesCount;
        }

        public void setMinCompaniesCount(Integer minCompaniesCount) {
            this.minCompaniesCount = minCompaniesCount;
        }

        public Integer getMinItemsCount() {
            return minItemsCount;
        }

        public void setMinItemsCount(Integer minItemsCount) {
            this.minItemsCount = minItemsCount;
        }
    }
}
