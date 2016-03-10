package net.ravendb.demo.indexes;

import net.ravendb.abstractions.data.Facet;
import net.ravendb.demo.entities.QProduct;

import java.util.Arrays;
import java.util.List;

public class FacetRangeCreation {

    public static List<Facet> createFacets(double from, double to) {

        QProduct product = QProduct.product;

        Facet facet1 = new Facet();
        facet1.setName("Products");

        Facet facet2 = new Facet();
        facet2.setName(product.pricePerUnit);
        facet2.setRanges(product.pricePerUnit.lt(from),
                product.pricePerUnit.goe(from).and(product.pricePerUnit.lt(to)),
                product.pricePerUnit.goe(to));

        Facet facet3 = new Facet();
        facet3.setName(product.unitsInStock);
        facet3.setRanges(product.unitsInStock.lt(10),
                product.unitsInStock.goe(10));

        return Arrays.asList(facet1, facet2, facet3);
    }

    public static class FacetsRangesResults {
        private String key;
        private String value;
        private int count;

        public String getKey() {
            return key;
        }

        public void setKey(String key) {
            this.key = key;
        }

        public String getValue() {
            return value;
        }

        public void setValue(String value) {
            this.value = value;
        }

        public int getCount() {
            return count;
        }

        public void setCount(int count) {
            this.count = count;
        }
    }
}
