<?php

namespace RavenDB\Demo\textSearch\fTSWithStaticIndexMultipleFields;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
use RavenDB\Documents\Indexes\FieldIndexing;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\LastFm;

//region Demo
//region Step_1
class Song_TextData extends AbstractIndexCreationTask
{
//endregion
    public function __construct()
    {
        parent::__construct();

        //region Step_2
        $this->map = "docs.LastFms.Select(song => new { " .
            "    SongData = new object[] { " .
            "        song.Artist, " .
            "        song.Title, " .
            "        song.Tags, " .
            "        song.TrackId " .
            "    } " .
            "})";
        //endregion

        //region Step_3
        $this->index("SongData", FieldIndexing::search());
        //endregion
    }
}
//endregion

class FTSWithStaticIndexMultipleFields
{
    public function __invoke(RunParams $runParams): array
    {
        $searchTerm = $runParams->getSearchTerm();

        //region Demo
        $results = [];

        $session = DocumentStoreHolder::getMediaStore()->openSession();
        try {
            //region Step_4
            $results = $session->query(LastFm::class, Song_TextData::class)
                ->search("SongData", $searchTerm)
                ->take(20)
                ->toList();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return $results;
    }
}
