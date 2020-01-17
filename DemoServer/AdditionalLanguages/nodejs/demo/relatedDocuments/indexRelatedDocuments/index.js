/* eslint-disable camelcase */
const { AbstractIndexCreationTask } = require('ravendb');
const { store } = require('../../common/docStoreHolder');

//region Demo
//region Step_2
// TODO
//endregion
//region Step_1
class Products_ByCategoryName extends AbstractIndexCreationTask {
    //endregion

    //region Step_3
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
    await store.executeIndex(new Products_ByCategoryName());

    //region Demo
    const session = store.openSession();
    //region Step_4
    const productsWithCategoryName = await session
        .query({ indexName: 'Products/ByCategoryName' })
        .whereEquals('CategoryName', categoryName)
        .all();
    //endregion
    //endregion

    return productsWithCategoryName;
}

module.exports = { run };
