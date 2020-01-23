//region Usings
const { AbstractIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

//region Demo
//region Step_1
class Products_ByCategoryName extends AbstractIndexCreationTask {
//endregion

    //region Step_2
    constructor () {
        super();

        this.map =
            `docs.products.Select(product => new { 
                CategoryName = (this.LoadDocument(product.Category, "Categories")).Name 
            })`;
    }
    //endregion
}
//endregion

async function run ({ categoryName }) {
    await documentStore.executeIndex(new Products_ByCategoryName());

    //region Demo
    const session = documentStore.openSession();
    //region Step_3
    const productsWithCategoryName = await session
        .query({ indexName: 'Products/ByCategoryName' })
        .whereEquals('CategoryName', categoryName)
        .all();
    //endregion
    //endregion

    return productsWithCategoryName;
}

module.exports = { run };
