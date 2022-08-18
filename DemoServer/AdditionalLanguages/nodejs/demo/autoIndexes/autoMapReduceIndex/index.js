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

    //region Step_6
    //   [
    //     {
    //       Country: 'USA',
    //       NumberOfEmployees: 5,
    //       '@metadata': { '@projection': true, '@change-vector': null, '@index-score': 1 }
    //     },
    //     {
    //       Country: 'UK',
    //       NumberOfEmployees: 4,
    //       '@metadata': { '@projection': true, '@change-vector': null, '@index-score': 1 }
    //     }
    //   ]
    //endregion
    //endregion

    return numberOfEmployeesPerCountry;
}

module.exports = { run };
