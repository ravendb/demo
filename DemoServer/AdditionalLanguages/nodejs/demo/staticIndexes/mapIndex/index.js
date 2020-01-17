/* eslint-disable camelcase */
const { AbstractIndexCreationTask } = require('ravendb');
const { store } = require('../../common/docStoreHolder');

//region Demo
//region Step_1
class Employees_ImportantDetails extends AbstractIndexCreationTask {
//endregion

    //region Step_2
    constructor () {
        super();
        this.map =
            `docs.Employees.Select(employee => new { 
                FullName = (employee.FirstName + " ") + employee.LastName, 
                Country = employee.Address.Country, 
                WorkingInCompanySince = employee.HiredAt.Year, 
                NumberOfTerritories = employee.Territories.Count 
            })`;
    }
    //endregion
}
//endregion

async function run ({ startYear }) {
    await store.executeIndex(new Employees_ImportantDetails());

    //region Demo
    const session = store.openSession();
    //region Step_3
    const employeesFromUSA = await session.query({ indexName: 'Employees/ImportantDetails' })
        .whereEquals('Country', 'USA')
        .whereGreaterThan('WorkingInCompanySince', startYear)
        .all();
    //endregion
    //endregion

    return employeesFromUSA;
}

module.exports = { run };
