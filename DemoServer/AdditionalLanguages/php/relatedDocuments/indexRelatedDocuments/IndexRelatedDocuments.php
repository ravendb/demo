<?php

namespace RavenDB\Demo\relatedDocuments\indexRelatedDocuments;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Product;

//region Demo
//region Step_1
class Products_ByCategoryName extends AbstractIndexCreationTask
//endregion
{

    //region Step_2
    public function __construct() {
        parent::__construct();

        $this->map = 'docs.products.Select(product => new { ' .
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name ' .
            '})';
    }
    //endregion
}
//endregion


class IndexRelatedDocuments
{
    public function __invoke(RunParams $runParams): array
    {
        $categoryName = $runParams->getCategoryName();

        //region Demo
        $productsWithCategoryName = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_3
            $productsWithCategoryName = $session
                ->query(Product::class, Products_ByCategoryName::class)
                    ->whereEquals("CategoryName", $categoryName)
                ->toList();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return $productsWithCategoryName;
    }
}
