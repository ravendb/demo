const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');
//region Usings
const { AbstractJavaScriptIndexCreationTask, QueryData } = require('ravendb');
//endregion

//region Demo
//region Step_1
class Employees_ByWorkPeriod extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map('employees', employee => {
            return {
                workingInCompanySince: new Date(employee.HiredAt).getFullYear()
            };
        });
        //endregion
    }
}

//region Step_3
class EmployeeProjectedDetails {
    constructor () {
        this.firstName = null;
        this.phone = null;
        this.location = null;
    }
}
//endregion
//endregion

async function run ({ startYear }) {
    await new Employees_ByWorkPeriod().execute(documentStore);

    //region Demo
    const session = documentStore.openSession();

    const employeesQuery = session
        //region Step_4
        .query(Employee, Employees_ByWorkPeriod)
        .whereGreaterThan('workingInCompanySince', startYear)
        //endregion
        //region Step_5
        .selectFields(QueryData.customFunction('employee',
            '{ firstName: employee.FirstName,' +
            '  phone: employee.HomePhone,' +
            "  location: employee.Address.City + ' ' + employee.Address.Country }"),
        EmployeeProjectedDetails);
        //endregion

    //region Step_6
    const employeesSinceYear = await employeesQuery.all();
    //endregion
    //endregion

    return employeesSinceYear;
}

module.exports = { run };
