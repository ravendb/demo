//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

//region Demo
//region Step_1
class Employees_ImportantDetails extends AbstractJavaScriptIndexCreationTask {
//endregion

    //region Step_2
    constructor () {
        super();

        this.map("Employees", employee => {
            return {
                fullName: employee.FirstName + " " + employee.LastName,
                country: employee.Address.Country,
                workingInCompanySince: new Date(employee.HiredAt).getFullYear(),
                numberOfTerritories: employee.Territories.length
            }
        });
    }
    //endregion
}
//endregion

async function run ({ startYear }) {
    await documentStore.executeIndex(new Employees_ImportantDetails());


    //region Demo
    const session = documentStore.openSession();
    //region Step_3
    const employeesFromUSA = await session.query({ indexName: 'Employees/ImportantDetails' })
        .whereEquals('country', 'USA')
        .whereGreaterThan('workingInCompanySince', startYear)
        .all();
    //endregion
    //endregion

    return employeesFromUSA;
}

module.exports = { run };
