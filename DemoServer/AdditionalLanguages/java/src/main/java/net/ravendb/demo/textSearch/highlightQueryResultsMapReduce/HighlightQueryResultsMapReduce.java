package net.ravendb.demo.textSearch.highlightQueryResultsMapReduce;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.indexes.FieldTermVector;
import net.ravendb.client.documents.queries.highlighting.HighlightingOptions;
import net.ravendb.client.documents.queries.highlighting.Highlightings;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.primitives.Reference;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class HighlightQueryResultsMapReduce {
    //region Demo
    //region Step_1
    public static class ArtistsAllSongs extends AbstractIndexCreationTask {    
    //endregion
    
        //region Step_2
        public static class IndexEntry {
            private String artist;
            private String allSongTitles;

            public String getArtist() {
                return artist;
            }

            public void setArtist(String artist) {
                this.artist = artist;
            }

            public String getAllSongTitles() {
                return allSongTitles;
            }

            public void setAllSongTitles(String allSongTitles) {
                this.allSongTitles = allSongTitles;
            }
        }
        //endregion
    
        public ArtistsAllSongs() {
            //region Step_3
            map = "docs.LastFms.Select(song => new {" +
                  "    artist = song.Artist," +
                  "    allSongTitles = song.Title" +
                  "})";
            //endregion
            
            //region Step_4
            reduce = "results.GroupBy(result => result.artist).Select(g => new {" +
                     "    artist = g.Key," +
                     "    allSongTitles = String.Join(\" \", g.Select(x => x.allSongTitles))" +
                     "})";
            //endregion

            //region Step_5
            store("artist", FieldStorage.YES);

            store("allSongTitles", FieldStorage.YES);
            index("allSongTitles", FieldIndexing.SEARCH);
            termVector("allSongTitles", FieldTermVector.WITH_POSITIONS_AND_OFFSETS);
            //endregion
        }
    }
    //endregion

    public List<DataToShow> run(RunParams runParams) {
        String searchTerm = ObjectUtils.firstNonNull(runParams.getSearchTerm(), "smile");
        String preTag = ObjectUtils.firstNonNull(runParams.getPreTag(), " (: ");
        String postTag = ObjectUtils.firstNonNull(runParams.getPostTag(), " :) ");
        int fragmentLength = ObjectUtils.firstNonNull(runParams.getFragmentLength(), 80);
        int fragmentCount = ObjectUtils.firstNonNull(runParams.getFragmentCount(), 1);

        Reference<Highlightings> highlightingsInfo = new Reference<>();
        
        //region Demo
        List<ArtistsAllSongs.IndexEntry> artistsResults;

        try (IDocumentSession session = DocumentStoreHolder.mediaStore.openSession()) {
            //region Step_6
            HighlightingOptions highlightingOptions = new HighlightingOptions();
            highlightingOptions.setGroupKey("artist");
            highlightingOptions.setPreTags(new String[] { preTag });
            highlightingOptions.setPostTags(new String[] { postTag });
            //endregion
            
            //region Step_7
            artistsResults = session.query(ArtistsAllSongs.IndexEntry.class, ArtistsAllSongs.class)
                .highlight("allSongTitles", fragmentLength, fragmentCount, highlightingOptions, highlightingsInfo)
                .search("allSongTitles", searchTerm)
                .toList();
            //endregion
            
            //region Step_8  
            if (artistsResults.size() > 0) {
                String[] songsFragments = highlightingsInfo.value.getFragments(artistsResults.get(0).getArtist());
            }
            //endregion
        }
        //endregion

        List<DataToShow> highlightResults = new ArrayList<>();

        for (ArtistsAllSongs.IndexEntry artistItem : artistsResults) {
            String[] songsFragments = highlightingsInfo.value.getFragments(artistItem.getArtist());
            for (String fragment : songsFragments) {
                DataToShow itemResults = new DataToShow();
                itemResults.setArtist(artistItem.getArtist());
                itemResults.setSongFragment(fragment);

                highlightResults.add(itemResults);
            }
        }

        return highlightResults.stream().sorted(Comparator.comparing(DataToShow::getArtist)).toList();
    }

    public static class RunParams {
        private String searchTerm;
        private String preTag;
        private String postTag;
        private Integer fragmentLength;
        private Integer fragmentCount;

        public String getSearchTerm() {
            return searchTerm;
        }

        public void setSearchTerm(String searchTerm) {
            this.searchTerm = searchTerm;
        }

        public String getPreTag() {
            return preTag;
        }

        public void setPreTag(String preTag) {
            this.preTag = preTag;
        }

        public String getPostTag() {
            return postTag;
        }

        public void setPostTag(String postTag) {
            this.postTag = postTag;
        }

        public Integer getFragmentLength() {
            return fragmentLength;
        }

        public void setFragmentLength(Integer fragmentLength) {
            this.fragmentLength = fragmentLength;
        }

        public Integer getFragmentCount() {
            return fragmentCount;
        }

        public void setFragmentCount(Integer fragmentCount) {
            this.fragmentCount = fragmentCount;
        }
    }

    public static class DataToShow {
        private String artist;
        private String songFragment;

        public String getArtist() {
            return artist;
        }

        public void setArtist(String artist) {
            this.artist = artist;
        }

        public String getSongFragment() {
            return songFragment;
        }

        public void setSongFragment(String songFragment) {
            this.songFragment = songFragment;
        }
    }
}
