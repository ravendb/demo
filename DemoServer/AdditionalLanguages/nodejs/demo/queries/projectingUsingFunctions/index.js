//region Usings
const { documentStore } = require('../../common/docStoreHolder');
//endregion
async function run () {
    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const projectedQueryWithFunctions  = await session.advanced.rawQuery(
    //endregion
        //region Step_2
        `declare function output(employee) {
            var formatName  = function(employee) { return "Full Name: " + employee.FirstName + " " + employee.LastName; };
            var formatTitle = function(employee) { return "Title: " + employee.Title };
            return { FullName : formatName(employee), Title : formatTitle(employee) };
        }
    //endregion
    //region Step_3
        from Employees as employee select output(employee)`
    //endregion
    );

    //region Step_4
    const projectedResults = await projectedQueryWithFunctions.all();
    //endregion

    //region Step_5
    /*
    [
        {
            "FullName": "Full Name: Anne Dodsworth",
            "Title": "Title: Sales Representative",
        },
        {
            "FullName": "Full Name: Nancy Davolio",
            "Title": "Title: Sales Representative",
        },
        {
            "FullName": "Full Name: Andrew Fuller",
            "Title": "Title: Vice President, Sales",
        },
        ...
    ]
    */
    //endregion
    //endregion

    return projectedResults;
}

module.exports = { run };
