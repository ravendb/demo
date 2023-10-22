<?php

namespace RavenDB\Demo\multiMapIndex\multiMapReduceIndex;

//region Usings
use RavenDB\Documents\Indexes\AbstractMultiMapIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region demo
//region Step_1
class CityCommerceDetails extends AbstractMultiMapIndexCreationTask
//endregion
{
    public function __construct()
    {
        parent::__construct();

        //region Step_2
        $this->addMap("docs.Companies.Select(company => new {" .
            "    CityName = company.Address.City," .
            "    NumberOfCompaniesInCity = 1," .
            "    NumberOfSuppliersInCity = 0," .
            "    NumberOfItemsShippedToCity = 0" .
            "})");

        $this->addMap("docs.Suppliers.Select(supplier => new {" .
            "    CityName = supplier.Address.City," .
            "    NumberOfCompaniesInCity = 0," .
            "    NumberOfSuppliersInCity = 1," .
            "    NumberOfItemsShippedToCity = 0" .
            "})");

        $this->addMap("docs.Orders.Select(order => new {" .
            "    CityName = order.ShipTo.City," .
            "    NumberOfCompaniesInCity = 0," .
            "    NumberOfSuppliersInCity = 0," .
            "    NumberOfItemsShippedToCity = Enumerable.Sum(order.Lines, x => ((int) x.Quantity))" .
            "})");
        //endregion
        
        //region Step_3
        $this->reduce = "results.GroupBy(result => result.CityName).Select(g => new {" .
            "    CityName = g.Key," .
            "    NumberOfCompaniesInCity = Enumerable.Sum(g, x => ((int) x.NumberOfCompaniesInCity))," .
            "    NumberOfSuppliersInCity = Enumerable.Sum(g, x0 => ((int) x0.NumberOfSuppliersInCity))," .
            "    NumberOfItemsShippedToCity = Enumerable.Sum(g, x1 => ((int) x1.NumberOfItemsShippedToCity))" .
            "})";
        //endregion
    }

}
//endregion

//region demo
//region Step_4
class IndexEntry
{
    private ?string $cityName = null;
    private ?int $numberOfCompaniesInCity = null;
    private ?int $numberOfSuppliersInCity = null;
    private ?int $numberOfItemsShippedToCity = null;

    public function getCityName(): ?string
    {
        return $this->cityName;
    }

    public function setCityName(?string $cityName): void
    {
        $this->cityName = $cityName;
    }

    public function getNumberOfCompaniesInCity(): ?int
    {
        return $this->numberOfCompaniesInCity;
    }

    public function setNumberOfCompaniesInCity(?int $numberOfCompaniesInCity): void
    {
        $this->numberOfCompaniesInCity = $numberOfCompaniesInCity;
    }

    public function getNumberOfSuppliersInCity(): ?int
    {
        return $this->numberOfSuppliersInCity;
    }

    public function setNumberOfSuppliersInCity(?int $numberOfSuppliersInCity): void
    {
        $this->numberOfSuppliersInCity = $numberOfSuppliersInCity;
    }

    public function getNumberOfItemsShippedToCity(): ?int
    {
        return $this->numberOfItemsShippedToCity;
    }

    public function setNumberOfItemsShippedToCity(?int $numberOfItemsShippedToCity): void
    {
        $this->numberOfItemsShippedToCity = $numberOfItemsShippedToCity;
    }
}
//endregion
//endregion

class MultiMapReduceIndex
{
    public function __invoke(RunParams $runParams): array
    {
        $minCompaniesCount = $runParams->getMinCompaniesCount() ?? 5;
        $minItemsCount = $runParams->getMinItemsCount() ?? 2000;
        
        //region Demo
        //region Step_5
        $commerceDetails = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $commerceDetails = $session->query(IndexEntry::class, CityCommerceDetails::class)
                ->whereGreaterThan("NumberOfCompaniesInCity", $minCompaniesCount)
                ->orElse()
                ->whereGreaterThan("NumberOfItemsShippedToCity", $minItemsCount)
                ->orderBy("CityName")
                ->toList();
                
        } finally {
            $session->close();
        }
        //endregion
        //endregion

        return $commerceDetails;
    }
}
