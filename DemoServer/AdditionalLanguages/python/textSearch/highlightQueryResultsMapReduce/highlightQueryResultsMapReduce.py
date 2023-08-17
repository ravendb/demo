from typing import Optional
from demo_example import RunParamsBase, Example

#region Usings
from ravendb import AbstractIndexCreationTask
from ravendb.documents.indexes.definitions import (
    FieldStorage,
    FieldIndexing,
    FieldTermVector,
)
from ravendb.documents.queries.highlighting import Highlightings, HighlightingOptions
#endregion

class RunParams(RunParamsBase):
    def __init__(
        self,
        search_term: str = None,
        pre_tag: str = None,
        post_tag: str = None,
        fragment_length: int = None,
        fragment_count: int = None,
    ):
        self.search_term = search_term
        self.pre_tag = pre_tag
        self.post_tag = post_tag
        self.fragment_length = fragment_length
        self.fragment_count = fragment_count

#region Demo
#region Step_1
class ArtistsAllSongs(AbstractIndexCreationTask):
#endregion
    #region Step_2
    class IndexEntry:
        def __init__(self, Artist: str = None, AllSongTitles: str = None):
            self.Artist = Artist
            self.AllSongTitles = AllSongTitles
    #endregion

    def __init__(self):
        #region Step_3
        super().__init__()
        self.map = (
            "docs.LastFms.Select(song => new { "
            "    Artist = song.Artist, "
            "    AllSongTitles = song.Title "
            "})"
        )        
        #endregion
        
        #region Step_4
        self.reduce = (
            "results.GroupBy(result => result.Artist).Select(g => new {"
            "    Artist = g.Key,"
            "    AllSongTitles = String.Join(' ', g.Select(x => x.AllSongTitles))"
            "})"
        )
        #endregion

        #region Step_5
        self._store("Artist", FieldStorage.YES)
        
        self._store("AllSongTitles", FieldStorage.YES)
        self._index("AllSongTitles", FieldIndexing.SEARCH)
        self._term_vector("AllSongTitles", FieldTermVector.WITH_POSITIONS_AND_OFFSETS)
        #endregion
#endregion

class HighlightQueryResultsMapReduce(Example):
    def run(self, run_params: RunParams):
        searchTerm = run_params.search_term or "smile"
        preTag = run_params.pre_tag or " (: "
        postTag = run_params.post_tag or " :) "
        fragmentLength = run_params.fragment_length or 80
        fragmentCount = run_params.fragment_count or 1
        ArtistsAllSongs().execute(self.document_store_holder.store())
        
        #region Demo
        highlightings_info: Optional[Highlightings] = None

        def __highlightings_callback(highlight: Highlightings):
            nonlocal highlightings
            highlightings = highlight

        with self.document_store_holder.store().open_session() as session:
            #region Step_6
            highlighting_options = HighlightingOptions()
            highlighting_options.group_key = "Artist"
            highlighting_options.pre_tags = [preTag]
            highlighting_options.post_tags = [postTag]
            #endregion

            #region Step_7
            artists_results = list(
                session.query_index_type(ArtistsAllSongs, ArtistsAllSongs.IndexEntry)
                .highlight(
                    "AllSongTitles",
                    fragmentLength,
                    fragmentCount,
                    __highlightings_callback,
                    highlighting_options,
                )
                .search("AllSongTitles", searchTerm)
            )
            #endregion

            #region Step_8
            if len(artists_results) > 0:
                songs_fragments = highlightings_info.get_fragments(artists_results[0].Artist)
            #endregion
            #endregion

            highlight_results = []
            for artist_item in artists_results:
                songs_fragments = highlightings_info.get_fragments(artist_item.Artist)
                for fragment in songs_fragments:
                    item_results = DataToShow(artist_item.Artist, fragment)
                    highlight_results.append(item_results)

        return sorted(highlight_results, key=lambda x: x.Artist)

class DataToShow:
    def __init__(self, artist: str = None, song_fragment: str = None):
        self.artist = artist
        self.song_fragment = song_fragment
