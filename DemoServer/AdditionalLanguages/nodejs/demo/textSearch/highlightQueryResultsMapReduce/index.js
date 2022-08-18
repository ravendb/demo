const { mediaStore } = require('../../common/docStoreHolder');
//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion

//region Demo
//region Step_1
class ArtistsAllSongs extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map('lastFms', song => {
            return {
                artist: song.Artist,
                allSongTitles: song.Title
            };
        });
        //endregion

        //region Step_3
        this.reduce(results => results.groupBy(result => result.artist).aggregate(g => {
            return {
                artist: g.key,
                allSongTitles: g.values.map(x => x.allSongTitles).join(' ')
            };
        }));
        //endregion

        //region Step_4
        this.store('artist', 'Yes');

        this.store('allSongTitles', 'Yes');
        this.index('allSongTitles', 'Search');
        this.termVector('allSongTitles', 'WithPositionsAndOffsets');
        //endregion
    }
}

//region Step_5
class IndexEntry {
    constructor () {
        this.artist = null;
        this.allSongTitles = null;
    }
}
//endregion
//endregion

async function run ({ searchTerm, preTag, postTag, fragmentLength, fragmentCount }) {
    searchTerm = searchTerm || 'smile';
    preTag = preTag || ' (: ';
    postTag = postTag || ' :) ';
    fragmentLength = fragmentLength || 80;
    fragmentCount = fragmentCount || 1;

    //region Demo
    const session = mediaStore.openSession();

    //region Step_6
    const highlightingOptions = {
        groupKey: 'artist',
        preTags: [preTag],
        postTags: [postTag]
    };
    //endregion

    //region Step_7
    let highlightingsInfo;

    const artistsResults = await session.query(IndexEntry, ArtistsAllSongs)
        .highlight({
            fieldName: 'allSongTitles',
            fragmentLength,
            fragmentCount,
            ...highlightingOptions
        }, h => {
            highlightingsInfo = h;
        })
        .search('allSongTitles', searchTerm)
        .all();
    //endregion

    //region Step_8
    if (artistsResults.length > 0) {
        const songsFragments = highlightingsInfo.getFragments(artistsResults[0].artist);
    }
    //endregion
    //endregion

    const highlightResults = [];

    artistsResults.forEach(artistItem => {
        const songFragments = highlightingsInfo.getFragments(artistItem.artist);
        songFragments.forEach(fragment => {
            highlightResults.push({
                artist: artistItem.artist,
                songFragment: fragment
            });
        });
    });

    return highlightResults.sort((a, b) => a.artist > b.artist ? -1 : (b.artist > a.artist ? 1 : 0));
}

module.exports = { run, ArtistsAllSongs };
