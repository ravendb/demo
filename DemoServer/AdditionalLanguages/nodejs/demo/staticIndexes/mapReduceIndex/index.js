//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

//region Demo
//region Step_1
class Employees_ByCountry extends AbstractJavaScriptIndexCreationTask {
//endregion

    constructor () {
        super();

        //region Step_2
        this.map("Employees", employee => {
            return {
                country: employee.Address.Country,
                countryCount: 1
            }
        });
        //endregion

        //region Step_3
        this.reduce(results => results.groupBy(x => x.country).aggregate(g => {
            return {
                country: g.key,
                countryCount: g.values.reduce((p, c) => p + c.countryCount, 0)
            }
        }));
        //endregion
    }
}
//endregion

async function run ({ country }) {
    await documentStore.executeIndex(new Employees_ByCountry());

    //region Demo
    const session = documentStore.openSession();
    //region Step_4
    const queryResult = await session.query({ indexName: 'Employees/ByCountry' })
        .whereEquals('country', country)
        .firstOrNull();

    const numberOfEmployeesFromCountry = queryResult != null ? queryResult.countryCount : 0;
    //endregion
    //endregion

    return numberOfEmployeesFromCountry;
}

module.exports = { run };
