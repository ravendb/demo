const { documentStore } = require('../../common/docStoreHolder');
const {
    AbstractJavaScriptIndexCreationTask,
    QueryData
} = require('ravendb');
const {
    Employee
} = require('../../common/models');

class Employees_ByWorkPeriod extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        this.map('employees', employee => {
            return {
                workingInCompanySince: new Date(employee.HiredAt).getFullYear()
            };
        });
    }
}

class EmployeeProjectedDetails {
    constructor () {
        this.firstName = null;
        this.phone = null;
        this.location = null;
    }
}

async function run ({ startYear }) {
    await new Employees_ByWorkPeriod().execute(documentStore);

    const session = documentStore.openSession();

    const employeesQuery = session
        .query(Employee, Employees_ByWorkPeriod)
        .whereGreaterThan('workingInCompanySince', startYear)
        .selectFields(QueryData.customFunction('employee',
            '{ firstName: employee.FirstName,\n' +
            '  phone: employee.HomePhone,\n' +
            "  location: employee.Address.City + ' ' + employee.Address.Country }"),
        EmployeeProjectedDetails);

    const employeesSinceYear = await employeesQuery.all();

    return employeesSinceYear;
}


module.exports = { run };
