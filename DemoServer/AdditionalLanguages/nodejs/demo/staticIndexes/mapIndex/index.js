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
                FullName: employee.FirstName + " " + employee.LastName,
                Country: employee.Address.Country,
                WorkingInCompanySince: new Date(employee.HiredAt).getFullYear(),
                NumberOfTerritories: employee.Territories.length
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
        .whereEquals('Country', 'USA')
        .whereGreaterThan('WorkingInCompanySince', startYear)
        .all();
    //endregion
    //endregion

    return employeesFromUSA;
}

module.exports = { run };
