//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

//region Demo
//region Step_1
class Employees_ByImportantDetailsJS extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();
        //region Step_2
        this.map('Employees', employee => {
            return {
                fullName: employee.FirstName + ' ' + employee.LastName,
                country: employee.Address.Country,
                workingInCompanySince: new Date(employee.HiredAt).getFullYear(),
                numberOfTerritories: employee.Territories.length
            };
        });
        //endregion
    }
}
//endregion

async function run ({ startYear }) {
    await new Employees_ByImportantDetailsJS().execute(documentStore);

    //region Demo
    const session = documentStore.openSession();

    //region Step_3
    const employeesFromUSA = session.query(Employee, Employees_ByImportantDetailsJS)
        .whereEquals('country', 'USA')
        .whereGreaterThan('workingInCompanySince', startYear)
        .all();
    //endregion
    //endregion

    return employeesFromUSA;
}

module.exports = { run };
