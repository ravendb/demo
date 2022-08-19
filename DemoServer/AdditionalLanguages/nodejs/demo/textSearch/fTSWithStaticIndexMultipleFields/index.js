//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { mediaStore } = require('../../common/docStoreHolder');

module.exports = { run };

//region Demo
//region Step_1
class Song_TextData extends AbstractJavaScriptIndexCreationTask {
//endregion

    constructor () {
        super();

        //region Step_2
        this.map("LastFms", song => {
            return {
                SongData: [song.Arist, song.Title, song.Tags, song.TrackId]
            }
        })
        //endregion

        //region Step_3
        this.index('SongData', 'Search');
        //endregion
    }
}
//endregion

async function run ({ searchTerm }) {
    await mediaStore.executeIndex(new Song_TextData());

    //region Demo
    const session = mediaStore.openSession();
    //region Step_4
    const results = await session.query({ indexName: 'Song/TextData' })
        .search('SongData', searchTerm)
        .take(20)
        .all();
    //endregion
    //endregion

    return results;
}
