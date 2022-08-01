package net.ravendb.demo.facetedSearch.facetsBasics;
import com.fasterxml.jackson.databind.node.ArrayNode;
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

public class FacetsBasics {
    //region demo
    //region Step_1
    public static class Products_ByCategoryAndPrice extends AbstractIndexCreationTask {
    
        public Products_ByCategoryAndPrice() {
            map = "docs.Products.Select(product => new {" +
                  "    CategoryName = (this.LoadDocument(product.Category, \"Categories\")).Name," +
                  "    PricePerUnit = product.PricePerUnit" +
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
        //region Step_2
        Facet categoryNameFacet = new Facet();
        categoryNameFacet.setFieldName("CategoryName");
        categoryNameFacet.setDisplayFieldName("Product Category");
        //endregion
        
        //region Step_3
        RangeFacet rangeFacet = new RangeFacet();
        rangeFacet.setRanges(Arrays.asList(
            "PricePerUnit < " + range1,
            "PricePerUnit >= " + range1 + " and PricePerUnit < " + range2,
            "PricePerUnit >= " + range2 + " and PricePerUnit < " + range3,
            "PricePerUnit >= " + range3
        ));
        rangeFacet.setDisplayFieldName("Price per Unit");
        //endregion

        //region Step_4
        FacetBase[] facets = new FacetBase[] { categoryNameFacet, rangeFacet };
        //endregion

        Map<String, FacetResult> queryResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_5
            IAggregationDocumentQuery<Product> query = session.query(Product.class, Products_ByCategoryAndPrice.class)
            //endregion
                //region Step_6
                .aggregateBy(facets);
                //endregion

            //region Step_7
            queryResults = query.execute();
            //endregion
        }
        //endregion

        for (Map.Entry<String, FacetResult> result : queryResults.entrySet()) {
            String facetName = result.getValue().getName();

            for (FacetValue item : result.getValue().getValues()) {
                MyFacetResult facetResult = new MyFacetResult();
                facetResult.setFacetName(facetName); // i.e. PricePerUnit
                facetResult.setFacetRange(item.getRange()); // i.e. PricePerUnit < 50
                facetResult.setFacetCount(item.getCount());

                facetsResults.add(facetResult);
            }
        }

        return facetsResults;
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
}
