//region Usings
const { documentStore } = require('../../common/docStoreHolder');
const {AbstractCsharpIndexCreationTask} = require("ravendb");
//endregion
async function run({ startYear }) {
    //region Demo
    //region Step_1
    class Employees_ByWorkPeriod extends AbstractCsharpIndexCreationTask {
    //endregion
        //region Step_2
        constructor() {
            super();
            this.map = "docs.Employees.Select(employee => new {\n" +
                "    WorkingInCompanySince = employee.HiredAt.Year\n" +
                "})"
        }
        //endregion
    }

    //region Step_3
    await new Employees_ByWorkPeriod().execute(documentStore);
    //endregion

    const session = documentStore.openSession();

    //region Step_4
    const rawQuery = `from index 'Employees/ByWorkPeriod' as employee\n`+
        `where employee.WorkingInCompanySince >` + startYear + '\n' +
        `select
        { 
            FirstName : employee.FirstName,
            Phone : employee.HomePhone,
            Location : employee.Address.City +" "+ employee.Address.Country 
        }`;
    //endregion
    //region Step_5
    const employeesQuery = await session.advanced.rawQuery(rawQuery);
    //endregion
    //region Step_6
    const employeesSinceYear  = await employeesQuery.all();
    //endregion
    //endregion
    return employeesSinceYear;
}

module.exports = { run };
