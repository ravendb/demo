<?php

namespace RavenDB\Demo\relatedDocuments\queryRelatedDocuments;

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Order;
use RavenDB\Demo\common\models\Product;

class QueryRelatedDocuments
{
    public function __invoke(): string
    {
        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $shippedOrders = $session->query(Order::class)
                ->include("Lines.Product")
                ->whereExists("ShippedAt")
                ->toList();
            //endregion

            //region Step_2
            /** @var Order $shippedOrder */
            foreach ($shippedOrders as $shippedOrder) {
                $productIds = array_map(
                    function ($x) {
                        return $x->getProduct();
                    },
                    $shippedOrder->getLines()->getArrayCopy()
                );
            //endregion

                for ($i = 0; $i < count($productIds); $i++) {
                    //region Step_3
                    $product = $session->load(Product::class, $productIds[$i]);
                    $product->setUnitsOnOrder($product->getUnitsOnOrder() + $shippedOrder->getLines()[$i]->getQuantity());
                    //endregion
                }
            }

            //region Step_4
            $session->saveChanges();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return "The Product Documents were updated successfully";
    }
}
