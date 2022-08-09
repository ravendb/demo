const {
    documentStore,
    mediaStore
} = require('../../common/docStoreHolder');
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');

class ArtistsAllSongs extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        this.map('lastFms', song => {
            return {
                artist: song.Artist,
                allSongTitles: song.Title
            };
        });

        this.reduce(results => results.groupBy(result => result.artist).aggregate(g => {
            return {
                artist: g.key,
                allSongTitles: g.values.map(x => x.allSongTitles).join(' ')
            };
        }));

        this.store('artist', 'Yes');

        this.store('allSongTitles', 'Yes');
        this.index('allSongTitles', 'Search');
        this.termVector('allSongTitles', 'WithPositionsAndOffsets');
    }
}

class IndexEntry {
    constructor () {
        this.artist = null;
        this.allSongTitles = null;
    }
}

async function run ({ searchTerm, preTag, postTag, fragmentLength, fragmentCount }) {
    searchTerm = searchTerm || 'smile';
    preTag = preTag || ' (: ';
    postTag = postTag || ' :) ';
    fragmentLength = fragmentLength || 80;
    fragmentCount = fragmentCount || 1;

    const session = mediaStore.openSession();

    let highlightingsInfo;

    const highlightingOptions = {
        groupKey: 'artist',
        preTags: [preTag],
        postTags: [postTag]
    };

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

    if (artistsResults.length > 0) {
        const songsFragments = highlightingsInfo.getFragments(artistsResults[0].artist);
    }

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
