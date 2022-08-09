const { documentStore } = require('../../common/docStoreHolder');
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
const { Employee } = require('../../common/models');

class EmployeesDetails extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        this.map('employees', employee => {
            return {
                Notes: employee.Notes[0]
            };
        });

        this.store('Notes', 'Yes');
        this.index('Notes', 'Search');
        this.termVector('Notes', 'WithPositionsAndOffsets');
    }
}

async function run ({ fragmentLength, fragmentCount }) {
    await new EmployeesDetails().execute(documentStore);

    let notesHighlightings;

    const session = documentStore.openSession();
    const employeesResults = await session.query(Employee, EmployeesDetails)
        .highlight({
            fieldName: 'Notes',
            fragmentLength,
            fragmentCount
        }, h => { notesHighlightings = h; })
        .search('Notes', 'sales')
        .all();

    if (employeesResults.length > 0) {
        const employeeId = employeesResults[0].id;

        const notesFragments = notesHighlightings.getFragments(employeeId);
    }

    return employeesResults;
}

module.exports = { run, EmployeesDetails };
