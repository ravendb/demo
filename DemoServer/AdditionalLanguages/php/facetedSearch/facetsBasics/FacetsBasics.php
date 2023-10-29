<?php

namespace RavenDB\Demo\facetedSearch\facetsBasics;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Queries\Facets\Facet;
use RavenDB\Documents\Queries\Facets\FacetResult;
use RavenDB\Documents\Queries\Facets\FacetValue;
use RavenDB\Documents\Queries\Facets\RangeFacet;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;

//region demo
//region Step_1
class Products_ByCategoryAndPrice extends AbstractIndexCreationTask
{
    public function __construct()
    {
        parent::__construct();

        $this->map = "docs.Products.Select(product => new {" .
            "    CategoryName = (this.LoadDocument(product.Category, \"Categories\")).Name," .
            "    PricePerUnit = product.PricePerUnit" .
            "})";
    }
}
//endregion
//endregion

class FacetsBasics
{
    public function __invoke(RunParams $runParams): array
    {
        $range1 = $runParams->getRange1() ?? 25;
        $range2 = $runParams->getRange2() ?? 50;
        $range3 = $runParams->getRange3() ?? 100;

        $facetsResults = [];

        //region Demo
        //region Step_2
        $categoryNameFacet = new Facet();
        $categoryNameFacet->setFieldName("CategoryName");
        $categoryNameFacet->setDisplayFieldName('Product Category');
        //endregion

        //region Step_3
        $rangeFacet = new RangeFacet();
        $rangeFacet->setRanges([
                "PricePerUnit < "  . $range1,
                "PricePerUnit >= " . $range1 . " and PricePerUnit < " . $range2,
                "PricePerUnit >= " . $range2 . " and PricePerUnit < " . $range3,
                "PricePerUnit >= " . $range3
            ]);
        $rangeFacet->setDisplayFieldName("Price per Unit");
        //endregion

        //region Step_4
        $facets = [ $categoryNameFacet, $rangeFacet ];
        //endregion

        $queryResults = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_5
            $query = $session->query(Product::class, Products_ByCategoryAndPrice::class)
            //endregion
                //region Step_6
                ->aggregateBy($facets);
                //endregion

            //region Step_7
            $queryResults = $query->execute();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        /** @var FacetResult $result */
        foreach ($queryResults as $result) {
            $facetName = $result->getName();

            echo 'TTT: ' . $facetName . PHP_EOL;

            /** @var FacetValue $item */
            foreach ($result->getValues() as $item) {
                $facetResult = new MyFacetResult();
                $facetResult->setFacetName($facetName); // i.e. PricePerUnit
                $facetResult->setFacetRange($item->getRange()); // i.e. PricePerUnit < 50
                $facetResult->setFacetCount($item->getCount());

                $facetsResults[] = $facetResult;
            }
        }

        return $facetsResults;
    }
}

class MyFacetResult
{
    private ?string $facetName = null;
    private ?string $facetRange = null;
    private ?float $facetCount = null;

    public function getFacetName(): ?string
    {
        return $this->facetName;
    }

    public function setFacetName(?string $facetName): void
    {
        $this->facetName = $facetName;
    }

    public function getFacetRange(): ?string
    {
        return $this->facetRange;
    }

    public function setFacetRange(?string $facetRange): void
    {
        $this->facetRange = $facetRange;
    }

    public function getFacetCount(): ?float
    {
        return $this->facetCount;
    }

    public function setFacetCount(?float $facetCount): void
    {
        $this->facetCount = $facetCount;
    }

