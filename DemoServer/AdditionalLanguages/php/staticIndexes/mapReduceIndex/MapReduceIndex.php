<?php

namespace RavenDB\Demo\staticIndexes\mapReduceIndex;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region Demo
//region Step_1
class Employees_ByCountry extends AbstractIndexCreationTask
//endregion
{
    public function __construct()
    {
        //region Step_2
        parent::__construct();

        $this->map = "docs.Employees.Select(employee => new { " .
            "    Country = employee.Address.Country, " .
            "    CountryCount = 1 " .
            "})";
        //endregion

        //region Step_3
        $this->reduce = "results.GroupBy(result => result.Country).Select(g => new { " .
            "    Country = g.Key, " .
            "    CountryCount = Enumerable.Sum(g, x => x.CountryCount) " .
            "})";
        //endregion
    }
}
//endregion

//region Demo
//region Step_4
class Employees_ByCountryResult
{
    private ?String $country = null;
    private ?int $countryCount = null;

    public function getCountry(): ?string
    {
        return $this->country;
    }

    public function setCountry(?string $country): void
    {
        $this->country = $country;
    }

    public function getCountryCount(): ?int
    {
        return $this->countryCount;
    }

    public function setCountryCount(?int $countryCount): void
    {
        $this->countryCount = $countryCount;
    }
}
//endregion
//endregion

class MapReduceIndex
{
    public function __invoke(RunParams $runParams)
    {
        $country = $runParams->getCountry();
        $numberOfEmployeesFromCountry = 0;

        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_5
            $queryResult = $session->query(Employees_ByCountryResult::class, Employees_ByCountry::class)
                ->whereEquals("Country", $country)
                ->firstOrDefault();

            $numberOfEmployeesFromCountry = $queryResult != null ? $queryResult->getCountryCount() : 0;
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return $numberOfEmployeesFromCountry;
    }
}
