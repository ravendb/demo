<?php

namespace RavenDB\Demo\multiMapIndex\multiMapIndexBasic;

//region Usings
use RavenDB\Documents\Indexes\AbstractMultiMapIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region Demo
//region Step_1
class CompaniesAndSuppliers_ByName extends AbstractMultiMapIndexCreationTask
{
//endregion

    //region Step_2
    public function __construct()
    {
        parent::__construct();

        $this->addMap("docs.Companies.Select(company => new {" .
            "    Name = company.Name" .
            "})");

        $this->addMap("docs.Suppliers.Select(supplier => new {" .
            "    Name = supplier.Name" .
            "})");
    }
    //endregion
}
//endregion

//region Demo
//region Step_3
class IndexEntry
{
    private ?string $name = null;

    public function getName(): ?string
    {
        return $this->name;
    }

    public function setName(?string $name): void
    {
        $this->name = $name;
    }
}
//endregion
//endregion

class MultiMapIndexBasic
{
    public function __invoke(RunParams $runParams): array
    {
        $namePrefix = $runParams->getNamePrefix() ?? "A";

        //region Demo
        $companiesAndSuppliersNames = [];

        //region Step_4
        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $companiesAndSuppliersNames = $session->query(IndexEntry::class, CompaniesAndSuppliers_ByName::class)
                ->whereStartsWith("Name", $namePrefix)
                ->toList();
                
        } finally {
            $session->close();
        }
        //endregion
        //endregion

        return $companiesAndSuppliersNames;
    }
}
