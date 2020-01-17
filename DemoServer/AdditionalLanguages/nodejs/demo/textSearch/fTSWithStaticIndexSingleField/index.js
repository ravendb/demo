/* eslint-disable no-useless-constructor */
/* eslint-disable camelcase */
const { AbstractIndexCreationTask } = require('ravendb');
const { store } = require('../../common/docStoreHolder');

module.exports = { run };

//region Demo
//region Step_1
class Categories_DescriptionText extends AbstractIndexCreationTask {
//endregion
//region Step_2
// TODO
//endregion

    constructor () {
        super();
        //region Step_3
        this.map = `docs.Categories.Select(category => new { 
                        CategoryDescription = category.Description 
                    })`;
        //endregion

        //region Step_4
        this.index('CategoryDescription', 'Search');
        //endregion
    }
}
//endregion

async function run ({ searchTerm }) {
    await store.executeIndex(new Categories_DescriptionText());

    //region Demo
    const session = store.openSession();
    //region Step_5
    const categoriesWithSearchTerm = await session.query({ indexName: 'Categories/DescriptionText' })
        .whereEquals('CategoryDescription', searchTerm)
        .all();
    //endregion
    //endregion

    return categoriesWithSearchTerm;
}
