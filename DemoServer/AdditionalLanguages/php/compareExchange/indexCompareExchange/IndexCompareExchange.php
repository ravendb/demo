<?php

namespace RavenDB\Demo\compareExchange\indexCompareExchange;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;

//region demo
//region Step_1
class Products_ByUnitsInStock extends AbstractIndexCreationTask
//endregion
{
    //region Step_2
    public function __construct()
    {
        parent::__construct();

        $this->map = "docs.Products.Select(product => new {" .
            "    UnitsInStock = this.LoadCompareExchangeValue(Id(product))" .
            "})";
    }
    //endregion
}
//endregion

//region demo
//region Step_3
class IndexEntry
{
    private ?int $unitsInStock = null;

    public function getUnitsInStock(): ?int
    {
        return $this->unitsInStock;
    }

    public function setUnitsInStock(?int $unitsInStock): void
    {
        $this->unitsInStock = $unitsInStock;
    }
}
//endregion
//endregion

class IndexCompareExchange
{
    public function __invoke(RunParams $runParams): array
    {
        $minValue = $runParams->getMinValue();

        //region demo
        $products = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            //region Step_4
            $products = $session->query(IndexEntry::class, Products_ByUnitsInStock::class)
                ->whereGreaterThan("UnitsInStock" , $minValue)
                ->ofType(Product::class)
                ->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $products;
    }
}
