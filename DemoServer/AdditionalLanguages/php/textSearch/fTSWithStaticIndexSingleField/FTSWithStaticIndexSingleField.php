<?php

namespace RavenDB\Demo\textSearch\fTSWithStaticIndexSingleField;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Indexes\FieldIndexing;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Category;

//region Demo
//region Step_1
class Categories_DescriptionText extends AbstractIndexCreationTask
{
//endregion
    public function __construct()
    {
        parent::__construct();

        //region Step_2
        $this->map = "docs.Categories.Select(category => new { " .
            "    CategoryDescription = category.Description " .
            "})";
        //endregion

        //region Step_3
        $this->index("CategoryDescription", FieldIndexing::search());
        //endregion
    }
}
//endregion

class FTSWithStaticIndexSingleField
{
    public function __invoke(RunParams $runParams): array
    {
        $searchTerm = $runParams->getSearchTerm();

        //region Demo
        $categoriesWithSearchTerm = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_4
            $categoriesWithSearchTerm = $session->query(Category::class, Categories_DescriptionText::class)
                ->whereEquals("CategoryDescription", $searchTerm)
                ->toList();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return $categoriesWithSearchTerm;
    }
}
