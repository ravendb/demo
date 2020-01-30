//region Usings
const { AbstractIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

module.exports = { run };

//region Demo
//region Step_1
class Categories_DescriptionText extends AbstractIndexCreationTask {
//endregion

    constructor () {
        super();
        //region Step_2
        this.map = `docs.Categories.Select(category => new { 
                        CategoryDescription = category.Description 
                    })`;
        //endregion

        //region Step_3
        this.index('CategoryDescription', 'Search');
        //endregion
    }
}
//endregion

async function run ({ searchTerm }) {
    await documentStore.executeIndex(new Categories_DescriptionText());

    //region Demo
    const session = documentStore.openSession();
    //region Step_4
    const categoriesWithSearchTerm = await session.query({ indexName: 'Categories/DescriptionText' })
        .whereEquals('CategoryDescription', searchTerm)
        .all();
    //endregion
    //endregion

    return categoriesWithSearchTerm;
}
