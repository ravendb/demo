package net.ravendb.demo.facetedSearch.facetsFromDocument;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.queries.facets.*;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import org.apache.commons.lang3.ObjectUtils;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Map;

public class FacetsFromDocument {

    //region Demo
    //region Step_1
    public static class Products_ByCategoryAndPrice extends AbstractIndexCreationTask {
        public Products_ByCategoryAndPrice() {
            map = "docs.Products.Select(product => new {" +
                  "    category = (this.LoadDocument(product.Category, \"Categories\")).Name," +
                  "    pricePerUnit = product.PricePerUnit" +
                  "})";
        }
    }
    //endregion
    //endregion

    public List<MyFacetResult> run(RunParams runParams) {
        int range1 = ObjectUtils.firstNonNull(runParams.getRange1(), 25);
        int range2 = ObjectUtils.firstNonNull(runParams.getRange2(), 50);
        int range3 = ObjectUtils.firstNonNull(runParams.getRange3(), 100);
        
        List<MyFacetResult> facetsResults = new ArrayList<>();
        
        //region Demo
        Map<String, FacetResult> queryResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
        
            //region Step_2
            FacetSetup facetSetup = new FacetSetup();
            facetSetup.setId("myFacetSetupDocumentID");
            
            Facet facet = new Facet();
            facet.setFieldName("category");
            facetSetup.setFacets(Arrays.asList(facet));

            RangeFacet rangeFacet = new RangeFacet();
            rangeFacet.setRanges(Arrays.asList(
                "pricePerUnit < " + range1,
                "pricePerUnit >= " + range1 + " and pricePerUnit < " + range2,
                "pricePerUnit >= " + range2 + " and pricePerUnit < " + range3,
                "pricePerUnit >= " + range3
            ));
            facetSetup.setRangeFacets(Arrays.asList(rangeFacet));
            //endregion

            //region Step_3
            session.store(facetSetup);
            session.saveChanges();
            //endregion
            
            //region Step_4
            queryResults = session.query(Product.class, Products_ByCategoryAndPrice.class)
                .aggregateUsing("myFacetSetupDocumentID")
                .execute();
            //endregion
        }
        //endregion

        for (Map.Entry<String, FacetResult> result : queryResults.entrySet()) {
            String facetName = result.getValue().getName();
            for (FacetValue item : result.getValue().getValues()) {
                MyFacetResult facetResult = new MyFacetResult();
                facetResult.setFacetName(facetName); // i.e. pricePerUnit
                facetResult.setFacetRange(item.getRange()); // i.e. pricePerUnit < 50
                facetResult.setFacetCount(item.getCount());

                facetsResults.add(facetResult);
            }
        }

        return facetsResults;
    }

    public static class RunParams {
        private Integer range1;
        private Integer range2;
        private Integer range3;

        public Integer getRange1() {
            return range1;
        }

        public void setRange1(Integer range1) {
            this.range1 = range1;
        }

        public Integer getRange2() {
            return range2;
        }

        public void setRange2(Integer range2) {
            this.range2 = range2;
        }

        public Integer getRange3() {
            return range3;
        }

        public void setRange3(Integer range3) {
            this.range3 = range3;
        }
    }

    public static class MyFacetResult {

        private String facetName;
        private String facetRange;
        private double facetCount;

        public String getFacetName() {
            return facetName;
        }

        public void setFacetName(String facetName) {
            this.facetName = facetName;
        }

        public String getFacetRange() {
            return facetRange;
        }

        public void setFacetRange(String facetRange) {
            this.facetRange = facetRange;
        }

        public double getFacetCount() {
            return facetCount;
        }

        public void setFacetCount(double facetCount) {
            this.facetCount = facetCount;
        }
    }
}
