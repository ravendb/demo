<?php

namespace RavenDB\Demo\staticIndexes\storeFieldsInIndex;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Indexes\FieldStorage;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region Demo
//region Step_1
class OrdersQuantity_ByCompany extends AbstractIndexCreationTask
//endregion
{
    public function __construct()
    {
        //region Step_2
        parent::__construct();
        $this->map = "docs.Orders.Select(order => new { " .
            "   Company = order.Company, " .
            "   TotalItemsOrdered = Enumerable.Sum(order.Lines, orderLine => ((int) orderLine.Quantity)) " .
            "})";
        //endregion

        //region Step_3
        $this->store("TotalItemsOrdered", FieldStorage::yes());
        //endregion
    }
}
//endregion

//region Demo
//region Step_4
class IndexEntry
{
    private ?string $Company = null;
    private ?int $TotalItemsOrdered = null;

    public function getCompany(): ?string
    {
        return $this->Company;
    }

    public function setCompany(?string $Company): void
    {
        $this->Company = $Company;
    }

    public function getTotalItemsOrdered(): ?int
    {
        return $this->TotalItemsOrdered;
    }

    public function setTotalItemsOrdered(?int $TotalItemsOrdered): void
    {
        $this->TotalItemsOrdered = $TotalItemsOrdered;
    }
}
//endregion
//endregion

//region Demo
//region Step_5
class OrderProjectedDetails
{
    private ?DateTime $OrderedAt = null;
    private ?int $TotalItemsOrdered = null;

    public function getOrderedAt(): ?DateTime
    {
        return $this->OrderedAt;
    }

    public function setOrderedAt(?DateTime $OrderedAt): void
    {
        $this->OrderedAt = $OrderedAt;
    }

    public function getTotalItemsOrdered(): ?int
    {
        return $this->TotalItemsOrdered;
    }

    public function setTotalItemsOrdered(null|int|string $TotalItemsOrdered): void
    {
        $this->TotalItemsOrdered = is_string($TotalItemsOrdered) ? intval($TotalItemsOrdered) : $TotalItemsOrdered;
    }
}
//endregion
//endregion

class StoreFieldsInIndex
{
    public function __invoke(RunParams $runParams): array
    {
        $companyId = $runParams->getCompanyId();
        (new OrdersQuantity_ByCompany())->execute(DocumentStoreHolder::getStore());

        //region Demo
        $ordersDetails = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {

            $ordersQuery = $session
                //region Step_6
                ->query(IndexEntry::class, OrdersQuantity_ByCompany::class)
                ->whereEquals("Company", $companyId)
                //endregion
                //region Step_7
                ->selectFields(OrderProjectedDetails::class);
                //endregion

            //region Step_8
            $ordersDetails = $ordersQuery->toList();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $ordersDetails;
    }
}
