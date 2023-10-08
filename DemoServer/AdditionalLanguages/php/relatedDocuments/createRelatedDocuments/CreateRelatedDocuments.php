<?php

namespace RavenDB\Demo\relatedDocuments\createRelatedDocuments;

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Category;
use RavenDB\Demo\common\models\Product;
use RavenDB\Demo\common\models\Supplier;

class CreateRelatedDocuments
{
    public function __invoke(RunParams $runParams): Product
    {
        $supplierName = $runParams->getSupplierName();
        $supplierPhone = $runParams->getSupplierPhone();
        $productName = $runParams->getProductName();

        //region Demo
        //region Step_1
        $supplier = new Supplier();
        $supplier->setName($supplierName);
        $supplier->setPhone($supplierPhone);

        $category = new Category();
        $category->setName("NoSQL Databases");
        $category->setDescription("Non-relational databases");
        //endregion

        //region Step_2
        $product = new Product();
        $product->setName($productName);
        //endregion

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_3
            $session->store($supplier);
            $session->store($category);
            //endregion

            //region Step_4
            $product->setSupplier($supplier->getId());
            $product->setCategory($category->getId());
            //endregion

            //region Step_5
            $session->store($product);
            //endregion

            //region Step_6
            $session->saveChanges();
            //endregion
            
        } finally {
            $session->close();
        }
        //endregion

        return $product;
    }
}
