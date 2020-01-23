const { documentStore } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    const session = documentStore.openSession();
    let stats;

    //region Step_1
    const query = session.query({ collection: 'employees' })
    //endregion
        //region Step_2
        .whereEquals('FirstName', 'Steven')
        .orElse()
        .whereEquals('Title', 'Sales Representative')
        //endregion
        //region Step_3
        .include('ReportsTo')
        //endregion
        //region Step_4
        .statistics(s => { stats = s; })
        //endregion
        //region Step_5
        .orderByDescending('HiredAt')
        //endregion
        //region Step_6
        .take(5);
        //endregion

    //region Step_7
    const queryResults = await query.all();
    //endregion
    //endregion
}

module.exports = { run };
