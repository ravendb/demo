/* eslint-disable camelcase */
const { AbstractIndexCreationTask } = require('ravendb');
const { store } = require('../../common/docStoreHolder');

//region Demo
//region Step_1
class Employees_ByCountry extends AbstractIndexCreationTask {
//endregion

    constructor () {
        super();

        //region Step_2
        this.map = `docs.Employees.Select(employee => new { 
                        Country = employee.Address.Country, 
                        CountryCount = 1 
                    })`;
        //endregion

        //region Step_3
        this.reduce = `results.GroupBy(result => result.Country).Select(g => new { 
                            Country = g.Key, 
                            CountryCount = Enumerable.Sum(g, x => x.CountryCount) 
                        })`;
        //endregion
    }

    //region Step_4
    // TODO
    //endregion
}
//endregion

async function run ({ country }) {
    await store.executeIndex(new Employees_ByCountry());

    //region Demo
    const session = store.openSession();
    //region Step_5
    const queryResult = await session.query({ indexName: 'Employees/ByCountry' })
        .whereEquals('Country', country)
        .firstOrNull();

    const numberOfEmployeesFromCountry = queryResult != null ? queryResult.CountryCount : 0;
    //endregion
    //endregion

    return numberOfEmployeesFromCountry;
}

module.exports = { run };
