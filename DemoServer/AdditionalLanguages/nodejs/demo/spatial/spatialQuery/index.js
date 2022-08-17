const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');
//region Usings
const { PointField } = require('ravendb');
//endregion

async function run ({ radius }) {
    radius = radius != null ? radius : 2;

    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const centerPointLng = -122.3150148;
    const centerPointLat = 47.63016419999999;

    const wktCircle = 'CIRCLE(' + centerPointLng + ' ' + centerPointLat + ' d=' + radius + ')';
    //endregion

    //region Step_2
    const employeesWithinCircle = await session.query(Employee)
        .spatial(
    //endregion
            //region Step_3
            new PointField('Address.Location.Latitude', 'Address.Location.Longitude'),
            //endregion
            //region Step_4
            spatialCriteria => spatialCriteria.relatesToShape(wktCircle, 'Within', 'Miles', 0)
            //endregion
        )
        //region Step_5
        .orderByDistance(new PointField('Address.Location.Latitude', 'Address.Location.Longitude'), centerPointLat, centerPointLng)
        //endregion
        //region Step_6
        .all();
        //endregion
    //endregion

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
