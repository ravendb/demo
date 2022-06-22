package net.ravendb.demo.multiMapIndexes.multiMapReduceIndex;

import net.ravendb.client.documents.indexes.AbstractMultiMapIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

import java.util.List;

public class MultiMapReduceIndex {

    public static class CityCommerceDetails extends AbstractMultiMapIndexCreationTask {
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

        public CityCommerceDetails() {
            this.addMap("docs.Companies.Select(company => new {\n" +
                "    CityName = company.Address.City,\n" +
                "    NumberOfCompaniesInCity = 1,\n" +
                "    NumberOfSuppliersInCity = 0,\n" +
                "    NumberOfItemsShippedToCity = 0\n" +
                "})");

            this.addMap("docs.Suppliers.Select(supplier => new {\n" +
                "    CityName = supplier.Address.City,\n" +
                "    NumberOfCompaniesInCity = 0,\n" +
                "    NumberOfSuppliersInCity = 1,\n" +
                "    NumberOfItemsShippedToCity = 0\n" +
                "})");

            this.addMap("docs.Orders.Select(order => new {\n" +
                "    CityName = order.ShipTo.City,\n" +
                "    NumberOfCompaniesInCity = 0,\n" +
                "    NumberOfSuppliersInCity = 0,\n" +
                "    NumberOfItemsShippedToCity = Enumerable.Sum(order.Lines, x => ((int) x.Quantity))\n" +
                "})");

            this.reduce = "results.GroupBy(result => result.CityName).Select(g => new {\n" +
                "    CityName = g.Key,\n" +
                "    NumberOfCompaniesInCity = Enumerable.Sum(g, x => ((int) x.NumberOfCompaniesInCity)),\n" +
                "    NumberOfSuppliersInCity = Enumerable.Sum(g, x0 => ((int) x0.NumberOfSuppliersInCity)),\n" +
                "    NumberOfItemsShippedToCity = Enumerable.Sum(g, x1 => ((int) x1.NumberOfItemsShippedToCity))\n" +
                "})";
        }
    }

    public List<CityCommerceDetails.IndexEntry> run(RunParams runParams) {
        int minCompaniesCount = ObjectUtils.firstNonNull(runParams.getMinCompaniesCount(), 5);
        int minItemsCount = ObjectUtils.firstNonNull(runParams.getMinItemsCount(), 2000);

        List<CityCommerceDetails.IndexEntry> commerceDetails;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {

            commerceDetails = session.query(CityCommerceDetails.IndexEntry.class, CityCommerceDetails.class)
                .whereGreaterThan("NumberOfCompaniesInCity", minCompaniesCount)
                .orElse()
                .whereGreaterThan("NumberOfItemsShippedToCity", minItemsCount)
                .orderBy("CityName")
                .toList();
        }

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
