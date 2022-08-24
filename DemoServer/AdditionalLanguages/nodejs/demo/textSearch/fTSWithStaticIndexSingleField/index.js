//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

module.exports = { run };

//region Demo
//region Step_1
class Categories_DescriptionText extends AbstractJavaScriptIndexCreationTask {
//endregion

    constructor () {
        super();
        //region Step_2
        this.map('Categories', category => {
            return {
                categoryDescription: category.Description
            }
        });
        //endregion

        //region Step_3
        this.index('categoryDescription', 'Search');
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
        .whereEquals('categoryDescription', searchTerm)
        .all();
    //endregion
    //endregion

    return categoriesWithSearchTerm;
}
