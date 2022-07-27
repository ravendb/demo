package net.ravendb.demo.facetedSearch.facetsOptions;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.queries.facets.*;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import org.apache.commons.lang3.ObjectUtils;
import java.util.Map;

public class FacetsOptions {

    //region Demo
    //region Step_1
    public static class Products_ByCategoryAndSupplier extends AbstractIndexCreationTask {
        public Products_ByCategoryAndSupplier() {
            map = "docs.Products.Select(product => new {" +
                  "    CategoryName = (this.LoadDocument(product.Category, \"Categories\")).Name," +
                  "    Supplier = product.Supplier" +
                  "})";
        }
    }
    //endregion
    //endregion

    public Map<String, FacetResult> run(RunParams runParams) {
        int start = ObjectUtils.firstNonNull(runParams.getStart(), 3);
        int pageSize = ObjectUtils.firstNonNull(runParams.getPageSize(), 2);

        String include = runParams.getIncludeRemainingTerms();
        boolean includeRemainingTerms = include == null || "true".equals(include);

        //region Demo
        //region Step_2
        Facet facet1 = new Facet();
        facet1.setFieldName("CategoryName");
                
        FacetOptions facet1Options = new FacetOptions();        
        facet1Options.setStart(start);
        facet1Options.setPageSize(pageSize);
        facet1Options.setIncludeRemainingTerms(includeRemainingTerms);
        facet1Options.setTermSortMode(FacetTermSortMode.COUNT_DESC);
        facet1.setOptions(facet1Options);
        //endregion

        Facet facet2 = new Facet();
        facet2.setFieldName("Supplier");
        
        FacetOptions facet2Options = new FacetOptions();
        facet2Options.setStart(start);
        facet2Options.setPageSize(pageSize);
        facet2Options.setIncludeRemainingTerms(includeRemainingTerms);
        facet2Options.setTermSortMode(FacetTermSortMode.VALUE_ASC);
        facet2.setOptions(facet2Options);
      
        //region Step_3 
        FacetBase[] facets = new FacetBase[] { facet1, facet2 };
        //endregion

        Map<String, FacetResult> queryResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            queryResults = session.query(Product.class, Products_ByCategoryAndSupplier.class)
                .aggregateBy(facets)
                .execute();
            //endregion
        }
        //endregion

        return queryResults;
    }

    public static class RunParams {
        private Integer start;
        private Integer pageSize;
        private String includeRemainingTerms;

        public Integer getStart() {
            return start;
        }

        public void setStart(Integer start) {
            this.start = start;
        }

        public Integer getPageSize() {
            return pageSize;
        }

        public void setPageSize(Integer pageSize) {
            this.pageSize = pageSize;
        }

        public String getIncludeRemainingTerms() {
            return includeRemainingTerms;
        }

        public void setIncludeRemainingTerms(String includeRemainingTerms) {
            this.includeRemainingTerms = includeRemainingTerms;
        }
    }
}
