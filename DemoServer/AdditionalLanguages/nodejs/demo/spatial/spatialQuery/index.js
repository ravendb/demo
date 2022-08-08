const { documentStore } = require('../../common/docStoreHolder');
const { PointField } = require('ravendb');
const { Employee } = require('../../common/models');

async function run ({ radius }) {
    radius = radius != null ? radius : 2;

    const session = documentStore.openSession();

    const centerPointLng = -122.3150148;
    const centerPointLat = 47.63016419999999;

    const wktCircle = 'CIRCLE(' + centerPointLng + ' ' + centerPointLat + ' d=' + radius + ')';

    const employeesWithinCircle = await session.query(Employee)
        .spatial(
            new PointField('Address.Location.Latitude', 'Address.Location.Longitude'),
            spatialCriteria => spatialCriteria.relatesToShape(wktCircle, 'Within', 'Miles', 0)
        )
        .orderByDistance(new PointField('Address.Location.Latitude', 'Address.Location.Longitude'))
        .all();

    const queryResults = employeesWithinCircle.map(item => {
        const detailedItem = new EmployeeDetails();
        detailedItem.employeeName = item.FirstName + ' ' + item.LastName;
        detailedItem.longitude = item.Address.Location.Longitude;
        detailedItem.latitude = item.Address.Location.Latitude;

        return detailedItem;
    });

    return queryResults;
}

class EmployeeDetails {
    constructor () {
        this.employeeName = null;
        this.longitude = null;
        this.latitude = null;
    }
}

module.exports = { run };
