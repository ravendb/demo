//region Usings
const { documentStore } = require('../../common/docStoreHolder');
//endregion
async function run () {
    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const rawQueryString = `declare function output(employee) {
            var formatTitle = function(employee) { return "Title: " + employee.Title };
            var formatName  = function(employee) { return "Name: " + employee.FirstName + " " + employee.LastName; };
            return { Title : formatTitle(employee), Name : formatName(employee) };
        }
    //endregion
    //region Step_2
        from Employees as employee select output(employee)`;
    //endregion

    //region Step_3
    const projectedQueryWithFunctions  = await session.advanced.rawQuery(rawQueryString);
    //endregion

    //region Step_4
    const projectedResults = await projectedQueryWithFunctions.all();
    //endregion

    //endregion

    return projectedResults;
}

module.exports = { run };
