//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Company } = require('../../common/models');

//region Demo
//region Step_1
class Companies_ByLocation extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        const { createSpatialField } = this.mapUtils();
        this.map('companies', company => {
            return {
                companyName: company.Name,
                locationCoordinates: createSpatialField(
                    company.Address.Location.Latitude,
                    company.Address.Location.Longitude
                )
            };
        });
        //endregion

        //region Step_3
        this.spatial('locationCoordinates', factory => factory.geography().quadPrefixTreeIndex(5));
        //endregion
    }
}
//endregion

async function run () {
    //region Demo
    const session = documentStore.openSession();

    //region Step_4
    const wktPolygon = 'POLYGON ((-125.06868394091362 41.855902525062724,' +
        ' -109.99544175341362 41.888625730467275,' +
        ' -116.76301987841362 50.59949235579767,' +
        ' -125.26643784716362 50.592518406260766,' +
        ' -125.06868394091362 41.855902525062724))';
    //endregion

    const seattleLatitude = 47.6062;
    const seattleLongitude = -122.3321;

    //region Step_5
    const companiesNearSeattle = await session.query(Company, Companies_ByLocation)
        .spatial('locationCoordinates', spatialCriteria => spatialCriteria.within(wktPolygon))
        .orderByDistance('locationCoordinates', seattleLatitude, seattleLongitude)
        .all();
    //endregion
    //endregion
    
    return companiesNearSeattle;
}

module.exports = { run, Companies_ByLocation };
