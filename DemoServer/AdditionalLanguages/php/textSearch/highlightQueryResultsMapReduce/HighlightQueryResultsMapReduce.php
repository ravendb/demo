<?php

namespace RavenDB\Demo\textSearch\highlightQueryResultsMapReduce;

//region Usings
use RavenDB\Documents\Queries\Highlighting\HighlightingOptions;
use RavenDB\Documents\Queries\Highlighting\Highlightings;
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Indexes\FieldIndexing;
use RavenDB\Documents\Indexes\FieldStorage;
use RavenDB\Documents\Indexes\FieldTermVector;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region Demo
//region Step_1
class ArtistsAllSongs extends AbstractIndexCreationTask
//endregion
{
    public function __construct()
    {
        parent::__construct();

        //region Step_2
        $this->map = "docs.LastFms.Select(song => new {" .
            "    Artist = song.Artist," .
            "    AllSongTitles = song.Title" .
            "})";
        //endregion

        //region Step_3
        $this->reduce = "results.GroupBy(result => result.Artist).Select(g => new {" .
            "    Artist = g.Key," .
            "    AllSongTitles = String.Join(\" \", g.Select(x => x.AllSongTitles))" .
            "})";
        //endregion

        //region Step_4
        $this->store("Artist", FieldStorage::yes());

        $this->store("AllSongTitles", FieldStorage::yes());
        $this->index("AllSongTitles", FieldIndexing::search());
        $this->termVector("AllSongTitles", FieldTermVector::withPositionsAndOffsets());
        //endregion
    }
}
//endregion

//region Demo
//region Step_5
class IndexEntry
{
    private ?string $artist = null;
    private ?string $allSongTitles = null;

    public function getArtist(): ?string
    {
        return $this->artist;
    }

    public function setArtist(?string $artist): void
    {
        $this->artist = $artist;
    }

    public function getAllSongTitles(): ?string
    {
        return $this->allSongTitles;
    }

    public function setAllSongTitles(?string $allSongTitles): void
    {
        $this->allSongTitles = $allSongTitles;
    }
}
//endregion
//endregion

class HighlightQueryResultsMapReduce
{
    public function __invoke(RunParams $runParams): array
    {
        $searchTerm = $runParams->getSearchTerm() ?? "smile";
        $preTag = $runParams->getPreTag() ?? " (: ";
        $postTag = $runParams->getPostTag() ?? " :) ";
        $fragmentLength = $runParams->getFragmentLength() ?? 80;
        $fragmentCount = $runParams->getFragmentCount() ?? 1;

        $highlightingsInfo = new Highlightings();

        //region Demo
        $artistsResults = [];

        $session = DocumentStoreHolder::getMediaStore()->openSession();
        try {
            //region Step_6
            $highlightingOptions = new HighlightingOptions();
            $highlightingOptions->setGroupKey("Artist");
            $highlightingOptions->setPreTags([ $preTag ]);
            $highlightingOptions->setPostTags([ $postTag ]);
            //endregion

            //region Step_7
            $artistsResults = $session->query(IndexEntry::class, ArtistsAllSongs::class)
                ->highlight("AllSongTitles", $fragmentLength, $fragmentCount, $highlightingOptions, $highlightingsInfo)
                ->search("AllSongTitles", $searchTerm)
                ->toList();
            //endregion

            //region Step_8
            if (count($artistsResults) > 0) {
                $songsFragments = $highlightingsInfo->getFragments($artistsResults[0]->getArtist());
            }
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        $highlightResults = [];

        /** @var IndexEntry $artistItem */
        foreach ($artistsResults as $artistItem) {
            $songsFragments = $highlightingsInfo->getFragments($artistItem->getArtist());
            foreach ($songsFragments as $fragment) {
                $itemResults = new DataToShow();
                $itemResults->setArtist($artistItem->getArtist());
                $itemResults->setSongFragment($fragment);

                $highlightResults[] = $itemResults;
            }
        }

        usort($highlightResults, function(DataToShow $a, DataToShow $b) {
            return $a->getArtist() > $b->getArtist();
        });

        return $highlightResults;
    }
}

class DataToShow
{
    private ?string $artist = null;
    private ?string $songFragment = null;

    public function getArtist(): ?string
    {
        return $this->artist;
    }

    public function setArtist(?string $artist): void
    {
        $this->artist = $artist;
    }

    public function getSongFragment(): ?string
    {
        return $this->songFragment;
    }

    public function setSongFragment(?string $songFragment): void
    {
        $this->songFragment = $songFragment;
    }
}
