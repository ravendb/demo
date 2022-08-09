const { documentStore } = require('../../common/docStoreHolder');
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
const { Employee } = require('../../common/models');

class EmployeesDetails extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        this.map('employees', employee => {
            return {
                title: employee.Title,
                notes: employee.Notes[0]
            };
        });

        this.store('title', 'Yes');
        this.index('title', 'Search');
        this.termVector('title', 'WithPositionsAndOffsets');

        this.store('notes', 'Yes');
        this.index('notes', 'Search');
        this.termVector('notes', 'WithPositionsAndOffsets');
    }
}

async function run ({ fragmentLength, fragmentCount, tag1, tag2, tag3, tag4 }) {
    fragmentLength = fragmentLength || 100;
    fragmentCount = fragmentCount || 1;

    tag1 = tag1 || '+++';
    tag2 = tag2 || '+++';
    tag3 = tag3 || '<<<';
    tag4 = tag4 || '>>>';

    let titleHighlightings, notesHighlightings;

    const session = documentStore.openSession();

    const tagsToUse1 = {
        preTags: [tag1],
        postTags: [tag2]
    };

    const tagsToUse2 = {
        preTags: [tag3],
        postTags: [tag4]
    };

    const employeesResults = await session.query(Employee, EmployeesDetails)
        .highlight({
            fieldName: 'title',
            fragmentLength,
            fragmentCount,
            ...tagsToUse1
        }, h => { titleHighlightings = h; })
        .highlight({
            fieldName: 'notes',
            fragmentLength,
            fragmentCount,
            ...tagsToUse2
        }, h => { notesHighlightings = h; })
        .search('title', 'manager')
        .search('notes', 'sales')
        .all();

    if (employeesResults.length > 0) {
        const employeeId = employeesResults[0].id;
        const titleFragments = titleHighlightings.getFragments(employeeId);
        const notesFragments = notesHighlightings.getFragments(employeeId);
    }

    const highlightResults = [];

    employeesResults.forEach(employee => {
        const titleFragments = titleHighlightings.getFragments(employee.id);

        titleFragments.forEach(item => {
            highlightResults.push({
                documentId: employee.id,
                indexField: titleHighlightings.fieldName,
                fragment: item
            });
        });

        const notesFragments = notesHighlightings.getFragments(employee.id);

        notesFragments.forEach(item => {
            highlightResults.push({
                documentId: employee.id,
                indexField: notesHighlightings.fieldName,
                fragment: item
            });
        });
    });

    return highlightResults.sort((a, b) => a.indexField > b.indexField ? -1 : (b.indexField > a.indexField ? 1 : 0));
}

module.exports = { run, EmployeesDetails };
