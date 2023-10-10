<?php

namespace RavenDB\Demo\relatedDocuments\loadRelatedDocuments;

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;
use RavenDB\Demo\common\models\Supplier;

class LoadRelatedDocuments
{
    public function __invoke(RunParams $runParams): string
    {
        $pricePerUnit = $runParams->getPricePerUnit();
        $phone = $runParams->getPhone();

        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $product = $session
                ->include("Supplier")
                ->load(Product::class, "products/34-A");
            //endregion

            //region Step_2
            $supplier = $session->load(Supplier::class, $product->getSupplier());
            //endregion

            //region Step_3
            $product->setPricePerUnit($pricePerUnit);
            $supplier->setPhone($phone);
            //endregion

            //region Step_4
            $session->saveChanges();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return "Document $documentId was edited successfully";
    }
}
