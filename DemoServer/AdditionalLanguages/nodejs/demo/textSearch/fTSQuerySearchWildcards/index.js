const { mediaStore } = require('../../common/docStoreHolder');
const { LastFm } = require('../../common/models');

async function run ({ start, end, middle, numberOrResults }) {
    //region Demo
    const session = mediaStore.openSession();

    //region Step_1
    const songsWithMatchingTerms = await session.query(LastFm)
    //endregion
        //region Step_2
        .search('Artist', start + '* *' + end, 'AND')
        //endregion
        //region Step_3
        .search('Title', '*' + middle + '*')
        //endregion
        //region Step_4
        .take(numberOrResults)
        .orderBy('Artist')
        //endregion
        //region Step_5
        .all();
        //endregion
    //endregion

    return songsWithMatchingTerms;
}

module.exports = { run };
