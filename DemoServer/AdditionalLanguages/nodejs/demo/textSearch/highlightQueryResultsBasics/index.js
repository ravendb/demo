const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');
//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion

//region Demo
//region Step_1
class EmployeesDetails extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map('employees', employee => {
            return {
                notes: employee.Notes[0]
            };
        });
        //endregion

        //region Step_3
        this.store('notes', 'Yes');
        this.index('notes', 'Search');
        this.termVector('notes', 'WithPositionsAndOffsets');
        //endregion
    }
}
//endregion

async function run ({ fragmentLength, fragmentCount }) {
    await new EmployeesDetails().execute(documentStore);

    //region Demo
    let notesHighlightings;

    const session = documentStore.openSession();

    //region Step_4
    const employeesResults = await session.query(Employee, EmployeesDetails)
        .highlight({
            fieldName: 'notes',
            fragmentLength,
            fragmentCount
        }, h => { notesHighlightings = h; })
        .search('notes', 'sales')
        .all();
    //endregion

    //region Step_5
    if (employeesResults.length > 0) {
        const employeeId = employeesResults[0].id;

        const notesFragments = notesHighlightings.getFragments(employeeId);
    }
    //endregion
    //endregion

    return employeesResults;
}

module.exports = { run, EmployeesDetails };
