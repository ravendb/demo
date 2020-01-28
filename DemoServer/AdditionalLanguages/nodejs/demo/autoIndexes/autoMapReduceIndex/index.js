const { documentStore } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const numberOfEmployeesPerCountry = await session.query({ collection: 'employees' })
    //endregion
        //region Step_2    
        .groupBy('Address.Country')
        //endregion
        //region Step_3
        .selectKey('Address.Country', 'Country')
        .selectCount('NumberOfEmployees')
        //endregion
        //region Step_4
        .orderByDescending('NumberOfEmployees')
        //endregion
        //region Step_5
        .all();
        //endregion    
    //endregion

    return numberOfEmployeesPerCountry;
}

module.exports = { run };
