const { documentStore } = require('../../common/docStoreHolder');
//region Usings
const { AbstractJavaScriptMultiMapIndexCreationTask } = require('ravendb');
//endregion

//region Demo
//region Step_1
class CityCommerceDetails extends AbstractJavaScriptMultiMapIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map('companies', company => {
            return {
                cityName: company.Address.City,
                numberOfCompaniesInCity: 1,
                numberOfSuppliersInCity: 0,
                numberOfItemsShippedToCity: 0
            };
        });

        this.map('suppliers', supplier => {
            return {
                cityName: supplier.Address.City,
                numberOfCompaniesInCity: 0,
                numberOfSuppliersInCity: 1,
                numberOfItemsShippedToCity: 0
            };
        });

        this.map('orders', order => {
            return {
                cityName: order.ShipTo.City,
                numberOfCompaniesInCity: 0,
                numberOfSuppliersInCity: order.Lines.reduce((p, c) => p + c.Quantity, 0),
                numberOfItemsShippedToCity: 0
            };
        });
        //endregion

        //region Step_3
        this.reduce(results => results.groupBy(result => result.cityName).aggregate(g => {
            return {
                cityName: g.key,
                numberOfCompaniesInCity: g.values.reduce((p, c) => c.numberOfCompaniesInCity + p, 0),
                numberOfSuppliersInCity: g.values.reduce((p, c) => c.numberOfSuppliersInCity + p, 0),
                numberOfItemsShippedToCity: g.values.reduce((p, c) => c.numberOfItemsShippedToCity + p, 0)
            };
        }));
        //endregion
    }
}

//region Step_4
class IndexEntry {
    constructor () {
        this.cityName = null;
        this.numberOfCompaniesInCity = null;
        this.numberOfSuppliersInCity = null;
        this.numberOfItemsShippedToCity = null;
    }
//endregion
}
//endregion

async function run ({ minCompaniesCount, minItemsCount }) {
    minCompaniesCount = minCompaniesCount != null ? minCompaniesCount : 5;
    minItemsCount = minItemsCount != null ? minItemsCount : 2000;

    //region Demo
    const session = documentStore.openSession();

    //region Step_5
    const commerceDetails = await session.query(IndexEntry, CityCommerceDetails)
        .whereGreaterThan('numberOfCompaniesInCity', minCompaniesCount)
        .orElse()
        .whereGreaterThan('numberOfItemsShippedToCity', minItemsCount)
        .orderBy('cityName')
        .all();
    //endregion
    //endregion

    return commerceDetails;
}

module.exports = { run, CityCommerceDetails };
