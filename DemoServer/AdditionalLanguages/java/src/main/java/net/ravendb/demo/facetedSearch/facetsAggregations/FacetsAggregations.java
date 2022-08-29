package net.ravendb.demo.facetedSearch.facetsAggregations;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.queries.facets.*;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import org.apache.commons.lang3.ObjectUtils;

import java.util.*;

public class FacetsAggregations {

    //region demo
    //region Step_1
    public static class Products_ByCategoryPriceAndUnits extends AbstractIndexCreationTask {
        
        public Products_ByCategoryPriceAndUnits() {
            map = "docs.Products.Select(product => new {" +
                  "    categoryName = (this.LoadDocument(product.Category, \"Categories\")).Name," +
                  "    pricePerUnit = product.PricePerUnit," +
                  "    unitsInStock = product.UnitsInStock" +
                  "})";
        }
    }
    //endregion
    //endregion

    public Map<String, FacetResult> run(RunParams runParams) {
        double range1 = ObjectUtils.firstNonNull(runParams.getRange1(), 25.0);
        double range2 = ObjectUtils.firstNonNull(runParams.getRange2(), 50.0);
        double range3 = ObjectUtils.firstNonNull(runParams.getRange3(), 100.0);

        //region Demo
        //region Step_2
        Facet facet = new Facet();
        facet.setFieldName("categoryName");
        
        FacetAggregationField pricePerUnitAggregationField = new FacetAggregationField();
        pricePerUnitAggregationField.setName("pricePerUnit");

        FacetAggregationField unitsInStockAggregationField = new FacetAggregationField();
        unitsInStockAggregationField.setName("unitsInStock");

        facet.getAggregations().put(FacetAggregation.AVERAGE, Collections.singleton(pricePerUnitAggregationField));
        facet.getAggregations().put(FacetAggregation.SUM, Collections.singleton(unitsInStockAggregationField));
        facet.getAggregations().put(FacetAggregation.MAX, Collections.singleton(pricePerUnitAggregationField));
        
        Set<FacetAggregationField> fields = new HashSet<>();
        fields.add(pricePerUnitAggregationField);
        fields.add(unitsInStockAggregationField);
        
        facet.getAggregations().put(FacetAggregation.MIN, fields);
        //endregion
        
        //region Step_3
        RangeFacet rangeFacet = new RangeFacet();
        rangeFacet.setRanges(Arrays.asList(
            "pricePerUnit < " + range1,
            "pricePerUnit >= " + range1 + " and pricePerUnit < " + range2,
            "pricePerUnit >= " + range2 + " and pricePerUnit < " + range3,
            "pricePerUnit >= " + range3
        ));

        rangeFacet.getAggregations().put(FacetAggregation.AVERAGE, Collections.singleton(pricePerUnitAggregationField));
        rangeFacet.getAggregations().put(FacetAggregation.SUM, Collections.singleton(unitsInStockAggregationField));
        rangeFacet.getAggregations().put(FacetAggregation.MAX, Collections.singleton(pricePerUnitAggregationField));
        rangeFacet.getAggregations().put(FacetAggregation.MIN, fields);
        //endregion

        //region Step_4
        FacetBase[] facets = new FacetBase[] {
            facet,
            rangeFacet
        };
        //endregion

        Map<String, FacetResult> queryResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_5
            queryResults = session.query(Product.class, Products_ByCategoryPriceAndUnits.class)
                .aggregateBy(facets)
                .execute();
            //endregion
        }
        //endregion

        return queryResults;
    }

    public static class RunParams {
        private Double range1;
        private Double range2;
        private Double range3;

        public Double getRange1() {
            return range1;
        }

        public void setRange1(Double range1) {
            this.range1 = range1;
        }

        public Double getRange2() {
            return range2;
        }

        public void setRange2(Double range2) {
            this.range2 = range2;
        }

        public Double getRange3() {
            return range3;
        }

        public void setRange3(Double range3) {
            this.range3 = range3;
        }
    }
}
