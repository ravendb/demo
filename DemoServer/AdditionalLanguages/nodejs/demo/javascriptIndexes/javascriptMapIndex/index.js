const {
    AbstractJavaScriptIndexCreationTask
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

class Employees_ByImportantDetailsJS extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        this.map('Employees', employee => {
            return {
                fullName: employee.FirstName + ' ' + employee.LastName,
                country: employee.Address.Country,
                workingInCompanySince: new Date(employee.HiredAt).getFullYear(),
                numberOfTerritories: employee.Territories.length
            };
        });
    }
}

async function run ({ startYear }) {
    await new Employees_ByImportantDetailsJS().execute(documentStore);

    const session = documentStore.openSession();

    const employeesFromUSA = session.query(Employee, Employees_ByImportantDetailsJS)
        .whereEquals('country', 'USA')
        .whereGreaterThan('workingInCompanySince', startYear)
        .all();

    return employeesFromUSA;
}

module.exports = { run };
