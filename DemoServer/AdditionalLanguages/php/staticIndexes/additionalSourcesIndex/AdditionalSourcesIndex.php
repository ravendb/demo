<?php

namespace RavenDB\Demo\staticIndexes\additionalSourcesIndex;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Indexes\AdditionalSourcesArray;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;

//region Demo
//region Step_1
class Products_ByPrice extends AbstractIndexCreationTask
//endregion
{
    public function __construct()
    {
        parent::__construct();

        //region Step_2
        $this->map = "docs.Products.Select(product => new {" .
            "    ProductName = product.Name," .
            "    OriginalPrice = product.PricePerUnit," .
            "    SalePrice = DiscountUtils.CalcSalePrice(product.PricePerUnit)," .
            "    ProfitPrice = DiscountUtils.CalcProfitPrice(product.PricePerUnit)" .
            "})";
        //endregion

        //region Step_3
        $this->setAdditionalSources(["DiscountLogic" => self::ADDITIONAL_SOURCE]);
        //endregion
    }

    //region Step_4
    public const ADDITIONAL_SOURCE = "public static class DiscountUtils" .
        "{" .
        "    public static decimal CalcSalePrice(decimal price)" .
        "    {" .
        "        return price - price / 100M * 25M;" .
        "    }" .
        "     " .
        "    public static decimal CalcProfitPrice(decimal price)" .
        "    {" .
        "        return price + price / 100M * 25M;" .
        "    }" .
        "}";
    //endregion
}
//endregion

//region Demo
//region Step_5
class IndexEntry
{
    private ?string $productName = null;
    private ?float $originalPrice = null;
    private ?float $salePrice = null;
    private ?float $profitPrice = null;

    public function getOriginalPrice(): ?float
    {
        return $this->originalPrice;
    }

    public function setOriginalPrice(?float $originalPrice): void
    {
        $this->originalPrice = $originalPrice;
    }

    public function getSalePrice(): ?float
    {
        return $this->salePrice;
    }

    public function setSalePrice(?float $salePrice): void
    {
        $this->salePrice = $salePrice;
    }

    public function getProfitPrice(): ?float
    {
        return $this->profitPrice;
    }

    public function setProfitPrice(?float $profitPrice): void
    {
        $this->profitPrice = $profitPrice;
    }
}
//endregion
//endregion

class AdditionalSourcesIndex
{
    public function __invoke(RunParams $runParams): array
    {
        $price = $runParams->getPrice() ?? 5;

        //region Demo
        $lowCostProducts = [];
        
        //region Step_6
        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $lowCostProducts = $session->query(Product::class, Products_ByPrice::class)
                ->whereLessThan("SalePrice", $price)
                ->orderBy("SalePrice")
                ->toList();
                
        } finally {
            $session->close();
        }
        //endregion
        //endregion

        // Manipulate results to show because index fields are Not stored.
        $productsData = [];
        
        /** @var Product $item */
        foreach ($lowCostProducts as $item) {
            $dataToShow = new DataToShow();
            $dataToShow->setProductName($item->getName());
            $dataToShow->setOriginalPrice($item->getPricePerUnit());
            $dataToShow->setSalesPrice($item->getPricePerUnit() - $item->getPricePerUnit() / 100 * 25);
            $dataToShow->setProfitPrice($item->getPricePerUnit() + $item->getPricePerUnit() / 100 * 25);
            $productsData[] = $dataToShow;
        }

        return $productsData;
    }
}
