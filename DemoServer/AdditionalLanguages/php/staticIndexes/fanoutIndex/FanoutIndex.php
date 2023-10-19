<?php

namespace RavenDB\Demo\staticIndexes\fanoutIndex;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Order;

//region Demo
//region Step_1
class Orders_ByProductDetails extends AbstractIndexCreationTask
//endregion
{
    //region Step_2
    public function __construct()
    {
        parent::__construct();
            $this->map = "docs.Orders.SelectMany(order => order.Lines, (order, orderLine) => new {" .
                "    ProductId = orderLine.Product," .
                "    ProductName = orderLine.ProductName" .
                "})";
        }
    //endregion
}
//endregion

//region Demo
//region Step_3
class IndexEntry
{
    private ?string $productId = null;
    private ?string $productName = null;

    public function getProductId(): ?string
    {
        return $this->productId;
    }

    public function setProductId(?string $productId): void
    {
        $this->productId = $productId;
    }

    public function getProductName(): ?string
    {
        return $this->productName;
    }

    public function setProductName(?string $productName): void
    {
        $this->productName = $productName;
    }
}
//endregion
//endregion

class FanoutIndex
{
    public function __invoke(RunParams $runParams): array
    {
        $namePrefix = $runParams->getNamePrefix() ?? "Chocolade";

        //region Demo
        $orders = [];

        //region Step_4
        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {

            $orders = $session->query(Order::class, Orders_ByProductDetails::class)
                ->whereStartsWith("ProductName", $namePrefix)
                ->toList();
                
        } finally {
            $session->close();
        }
        //endregion
        //endregion

        return $orders;
    }
}
