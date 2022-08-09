
const {
    mediaStore
} = require('../../common/docStoreHolder');
const { LastFm } = require('../../common/models');

async function run ({ start, end, middle, numberOrResults }) {
    const session = mediaStore.openSession();

    const songsWithMatchingTerms = await session.query(LastFm)
        .search('Artist', start + '* *' + end, 'AND')
        .search('Title', '*' + middle + '*')
        .take(numberOrResults)
        .orderBy('Artist')
        .all();

    return songsWithMatchingTerms;
}

module.exports = { run };
