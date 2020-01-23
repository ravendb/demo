const { documentStore } = require('../../common/docStoreHolder');

async function run ({ resultsToSkip, resultsToTake }) {
    //region Demo
    const session = documentStore.openSession();
    let stats;

    //region Step_1
    const pagedResults = await session.query({ collection: 'companies' })
    //endregion
        //region Step_2
        .statistics(_stats => { stats = _stats; })
        //endregion
        //region Step_3
        .skip(resultsToSkip)
        //endregion
        //region Step_4
        .take(resultsToTake)
        //endregion
        //region Step_5
        .all();
        //endregion

    //region Step_6
    const totalResults = stats.totalResults;
    //endregion
    //endregion
}

module.exports = { run };
