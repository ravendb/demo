<?php

namespace RavenDB\Demo\facetedSearch\facetsAggregations;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Queries\Facets\AggregationArray;
use RavenDB\Documents\Queries\Facets\Facet;
use RavenDB\Documents\Queries\Facets\FacetAggregation;
use RavenDB\Documents\Queries\Facets\FacetAggregationField;
use RavenDB\Documents\Queries\Facets\FacetAggregationFieldSet;
use RavenDB\Documents\Queries\Facets\FacetResultArray;
use RavenDB\Documents\Queries\Facets\RangeFacet;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;

//region demo
//region Step_1
class Products_ByCategoryPriceAndUnits extends AbstractIndexCreationTask
{
    public function __construct()
    {
        parent::__construct();

        $this->map = "docs.Products.Select(product => new {" .
            "    CategoryName = (this.LoadDocument(product.Category, \"Categories\")).Name," .
            "    PricePerUnit = product.PricePerUnit," .
            "    UnitsInStock = product.UnitsInStock" .
            "})";
    }
}
//endregion
//endregion


class FacetsAggregations
{
    public function __invoke(RunParams $runParams): FacetResultArray
    {
        $range1 = $runParams->getRange1() ?? 25.0;
        $range2 = $runParams->getRange2() ?? 50.0;
        $range3 = $runParams->getRange3() ?? 100.0;

        //region Demo
        //region Step_2
        $facet = new Facet();
        $facet->setFieldName("CategoryName");

        $pricePerUnitAggregationField = new FacetAggregationField();
        $pricePerUnitAggregationField->setName("PricePerUnit");

        $unitsInStockAggregationField = new FacetAggregationField();
        $unitsInStockAggregationField->setName("UnitsInStock");

        $facet->getAggregations()->set(FacetAggregation::average(), FacetAggregationFieldSet::fromArray([ $pricePerUnitAggregationField ]));
        $facet->getAggregations()->set(FacetAggregation::sum(), FacetAggregationFieldSet::fromArray([ $unitsInStockAggregationField ]));
        $facet->getAggregations()->set(FacetAggregation::max(), FacetAggregationFieldSet::fromArray([ $pricePerUnitAggregationField ]));

        $fields = [];
        $fields[] = $pricePerUnitAggregationField;
        $fields[] = $unitsInStockAggregationField;

        $facet->getAggregations()->set(FacetAggregation::min(), FacetAggregationFieldSet::fromArray($fields));
        //endregion

        //region Step_3
        $rangeFacet = new RangeFacet();
        $rangeFacet->setRanges([
                "PricePerUnit < " . $range1,
                "PricePerUnit >= " . $range1 . " and PricePerUnit < " . $range2,
                "PricePerUnit >= " . $range2 . " and PricePerUnit < " . $range3,
                "PricePerUnit >= " . $range3
            ]);

        $aggregations = new AggregationArray();

        $aggregations->offsetSet(FacetAggregation::AVERAGE, FacetAggregationFieldSet::fromArray([ $pricePerUnitAggregationField ]));
        $aggregations->offsetSet(FacetAggregation::SUM, FacetAggregationFieldSet::fromArray([ $unitsInStockAggregationField ]));
        $aggregations->offsetSet(FacetAggregation::MAX, FacetAggregationFieldSet::fromArray([ $pricePerUnitAggregationField ]));
        $aggregations->offsetSet(FacetAggregation::MIN, FacetAggregationFieldSet::fromArray($fields));

        $rangeFacet->setAggregations($aggregations);

        //endregion

        //region Step_4
        $facets = [
            $facet,
            $rangeFacet
        ];
        //endregion

        $queryResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_5
            $queryResults = $session->query(Product::class, Products_ByCategoryPriceAndUnits::class)
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
