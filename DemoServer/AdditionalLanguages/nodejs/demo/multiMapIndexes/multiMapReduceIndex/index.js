const { documentStore } = require('../../common/docStoreHolder');
const { AbstractJavaScriptMultiMapIndexCreationTask } = require('ravendb');

class CityCommerceDetails extends AbstractJavaScriptMultiMapIndexCreationTask {
    constructor () {
        super();

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

        this.reduce(results => results.groupBy(result => result.cityName).aggregate(g => {
            return {
                cityName: g.key,
                numberOfCompaniesInCity: g.values.reduce((p, c) => c.numberOfCompaniesInCity + p, 0),
                numberOfSuppliersInCity: g.values.reduce((p, c) => c.numberOfSuppliersInCity + p, 0),
                numberOfItemsShippedToCity: g.values.reduce((p, c) => c.numberOfItemsShippedToCity + p, 0)
            };
        }));
    }
}

class IndexEntry {
    constructor () {
        this.cityName = null;
        this.numberOfCompaniesInCity = null;
        this.numberOfSuppliersInCity = null;
        this.numberOfItemsShippedToCity = null;
    }
}

async function run ({ minCompaniesCount, minItemsCount }) {
    minCompaniesCount = minCompaniesCount != null ? minCompaniesCount : 5;
    minItemsCount = minItemsCount != null ? minItemsCount : 2000;

    const session = documentStore.openSession();

    const commerceDetails = await session.query(IndexEntry, CityCommerceDetails)
        .whereGreaterThan('numberOfCompaniesInCity', minCompaniesCount)
        .orElse()
        .whereGreaterThan('numberOfItemsShippedToCity', minItemsCount)
        .orderBy('cityName')
        .all();

    return commerceDetails;
}

module.exports = { run, CityCommerceDetails };
