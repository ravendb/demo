<?php

namespace RavenDB\Demo\multiMapIndex\multiMapIndexCustomizedFields;

//region Usings
use RavenDB\Documents\Indexes\AbstractMultiMapIndexCreationTask;
use RavenDB\Documents\Indexes\FieldStorage;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region Demo
//region Step_1
class Contacts_ByNameAndTitle extends AbstractMultiMapIndexCreationTask
{
//endregion
    public function __construct()
    {
        parent::__construct();

        //region Step_2
        $this->addMap("docs.Employees.Select(employee => new {" .
            "    ContactName = (employee.FirstName + \" \") + employee.LastName," .
            "    ContactTitle = employee.Title," .
            "    Collection = this.MetadataFor(employee)[\"@collection\"]" .
            "})");

        $this->addMap("docs.Companies.Select(company => new {" .
            "    ContactName = company.Contact.Name," .
            "    ContactTitle = company.Contact.Title," .
            "    Collection = this.MetadataFor(company)[\"@collection\"]" .
            "})");

        $this->addMap("docs.Suppliers.Select(supplier => new {" .
            "    ContactName = supplier.Contact.Name," .
            "    ContactTitle = supplier.Contact.Title," .
            "    Collection = this.MetadataFor(supplier)[\"@collection\"]" .
            "})");
        //endregion
        
        //region Step_3
        $this->store("ContactName", FieldStorage::yes());
        $this->store("ContactTitle", FieldStorage::yes());
        $this->store("Collection", FieldStorage::yes());
        //endregion
    }
}
//endregion

//region Demo
//region Step_4
class IndexEntry
{
    public ?string $ContactName = null;
    public ?string $ContactTitle = null;
    public ?string $Collection = null;

    public function getContactName(): ?string
    {
        return $this->ContactName;
    }

    public function setContactName(?string $ContactName): void
    {
        $this->ContactName = $ContactName;
    }

    public function getContactTitle(): ?string
    {
        return $this->ContactTitle;
    }

    public function setContactTitle(?string $ContactTitle): void
    {
        $this->ContactTitle = $ContactTitle;
    }

    public function getCollection(): ?string
    {
        return $this->Collection;
    }

    public function setCollection(?string $Collection): void
    {
        $this->Collection = $Collection;
    }
}
//endregion
//endregion

//region Demo
//region Step_5
class ProjectedEntry extends IndexEntry
{
    public ?string $phone = null;

    public function getPhone(): ?string
    {
        return $this->phone;
    }

    public function setPhone(?string $phone): void
    {
        $this->phone = $phone;
    }
}
//endregion
//endregion

class MultiMapIndexCustomizedFields
{
    public function __invoke(RunParams $runParams): array
    {
        $namePrefix = $runParams->getNamePrefix() ?? "Michael";
        $titlePrefix = $runParams->getTitlePrefix() ?? "Sales";

        //region Demo
        //region Step_6
        $contacts = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $contacts = $session->query(IndexEntry::class, Contacts_ByNameAndTitle::class)
                ->whereStartsWith("ContactName", $namePrefix)
                ->whereStartsWith("ContactTitle", $titlePrefix)
                ->selectFields(ProjectedEntry::class)
                ->toList();
                
        } finally {
            $session->close();
        }
        //endregion
        //endregion
        
        return $contacts;
    }
}
