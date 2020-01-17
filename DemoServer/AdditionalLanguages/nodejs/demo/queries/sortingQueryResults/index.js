const { store } = require('../../common/docStoreHolder');

async function run ({ numberOfUnits }) {
    //region Demo
    const session = store.openSession();
    //region Step_1
    const sortedProducts = await session.query({ collection: 'products' })
    //endregion
        //region Step_2
        .whereGreaterThan('UnitsInStock', numberOfUnits)
        //endregion
        //region Step_3
        .orderByDescending('UnitsInStock')
        //endregion
        //region Step_4
        .orderBy('Name', 'AlphaNumeric')
        //endregion
        //region Step_5
        .all();
        //endregion
    //endregion
}

module.exports = { run };
