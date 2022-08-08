const {
    AbstractJavaScriptIndexCreationTask
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');
const { Company } = require('../../common/models');

class Companies_ByLocation extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

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

        this.spatial('locationCoordinates', factory => factory.geography().quadPrefixTreeIndex(5));
    }
}

async function run () {
    const session = documentStore.openSession();

    const wktPolygon = 'POLYGON ((-125.06868394091362 41.855902525062724,' +
        ' -109.99544175341362 41.888625730467275,' +
        ' -116.76301987841362 50.59949235579767,' +
        ' -125.26643784716362 50.592518406260766,' +
        ' -125.06868394091362 41.855902525062724))';

    const seattleLatitude = 47.6062;
    const seattleLongitude = -122.3321;

    const companiesNearSeattle = await session.query(Company, Companies_ByLocation)
        .spatial('locationCoordinates', spatialCriteria => spatialCriteria.within(wktPolygon))
        .orderByDistance('locationCoordinates', seattleLatitude, seattleLongitude)
        .all();

    return companiesNearSeattle;
}

module.exports = { run, Companies_ByLocation };
