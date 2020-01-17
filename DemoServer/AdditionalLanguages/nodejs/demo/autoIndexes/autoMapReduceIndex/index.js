const { store } = require('../../common/docStoreHolder');

async function run () {
    /**
     * TODO:
     *
     * please update steps
     */

    //region Demo

    const session = store.openSession();

    const numberOfEmployeesPerCountry = await session.query({ collection: 'employees' })
        .groupBy('Address.Country')
        .selectKey('Address.Country', 'Country')
        .selectCount('NumberOfEmployees')
        .orderByDescending('NumberOfEmployees')
        .all();
        // [
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

    return numberOfEmployeesPerCountry;
}

module.exports = { run };
