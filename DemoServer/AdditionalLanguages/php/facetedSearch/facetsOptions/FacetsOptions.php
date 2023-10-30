<?php

namespace RavenDB\Demo\facetedSearch\facetsOptions;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Queries\Facets\Facet;
use RavenDB\Documents\Queries\Facets\FacetOptions;
use RavenDB\Documents\Queries\Facets\FacetResultArray;
use RavenDB\Documents\Queries\Facets\FacetTermSortMode;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;

//region Demo
//region Step_1
class Products_ByCategoryAndSupplier extends AbstractIndexCreationTask
{
    public function __construct()
    {
        parent::__construct();

        $this->map = "docs.Products.Select(product => new {" .
            "    CategoryName = (this.LoadDocument(product.Category, \"Categories\")).Name," .
            "    Supplier = product.Supplier" .
            "})";
    }
}
//endregion
//endregion

class FacetsOptions
{
    public function __invoke(RunParams $runParams): FacetResultArray
    {
        $start = $runParams->getStart() ?? 3;
        $pageSize = $runParams->getPageSize() ?? 2;

        $include = $runParams->getIncludeRemainingTerms();
        $includeRemainingTerms = $include == null || "true" == $include;

        //region Demo
        //region Step_2
        $facet1 = new Facet();
        $facet1->setFieldName("CategoryName");

        $facet1Options = new FacetOptions();
        $facet1Options->setStart($start);
        $facet1Options->setPageSize($pageSize);
        $facet1Options->setIncludeRemainingTerms($includeRemainingTerms);
        $facet1Options->setTermSortMode(FacetTermSortMode::countDesc());
        $facet1->setOptions($facet1Options);
        //endregion

        $facet2 = new Facet();
        $facet2->setFieldName("Supplier");

        $facet2Options = new FacetOptions();
        $facet2Options->setStart($start);
        $facet2Options->setPageSize($pageSize);
        $facet2Options->setIncludeRemainingTerms($includeRemainingTerms);
        $facet2Options->setTermSortMode(FacetTermSortMode::valueAsc());
        $facet2->setOptions($facet2Options);

        //region Step_3
        $facets = [ $facet1, $facet2 ];
        //endregion

        $queryResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_4
            $queryResults = $session->query(Product::class, Products_ByCategoryAndSupplier::class)
                ->aggregateBy($facets)
                ->execute();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return $queryResults;
    }
}
