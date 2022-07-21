package net.ravendb.demo.spatial.spatialQuery;
//region Usings
import net.ravendb.client.documents.indexes.spatial.SpatialRelation;
import net.ravendb.client.documents.indexes.spatial.SpatialUnits;
import net.ravendb.client.documents.queries.spatial.PointField;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import org.apache.commons.lang3.ObjectUtils;
import java.util.ArrayList;
import java.util.List;

public class SpatialQuery {

    public static class EmployeeDetails {
        private String employeeName;
        private double longitude;
        private double latitude;

        public String getEmployeeName() {
            return employeeName;
        }

        public void setEmployeeName(String employeeName) {
            this.employeeName = employeeName;
        }

        public double getLongitude() {
            return longitude;
        }

        public void setLongitude(double longitude) {
            this.longitude = longitude;
        }

        public double getLatitude() {
            return latitude;
        }

        public void setLatitude(double latitude) {
            this.latitude = latitude;
        }
    }

    public List<EmployeeDetails> run(RunParams runParams) {
        int radius = ObjectUtils.firstNonNull(runParams.getRadius(), 2);

        List<EmployeeDetails> queryResults = new ArrayList<>();
        //region Demo
        List<Employee> employeesWithinCircle = new ArrayList<>();

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            double centerPointLng = -122.3150148;
            double centerPointLat = 47.63016419999999;
            
            //region Step_1
            String wktCircle = "CIRCLE(" + centerPointLng + " " + centerPointLat + " d=" + radius + ")";
            //endregion

            //region Step_2 
            employeesWithinCircle = session.query(Employee.class)
                .spatial(
            //endregion
                    //region Step_3
                    new PointField("Address.Location.Latitude", "Address.Location.Longitude"),
                    //endregion
                    
                    //region Step_4
                    spatialCriteria -> spatialCriteria.relatesToShape(wktCircle, SpatialRelation.WITHIN, SpatialUnits.MILES, 0))
                    //endregion
                    
                //region Step_5
                .orderByDistance(new PointField("Address.Location.Latitude", "Address.Location.Longitude"), centerPointLat, centerPointLng)
                //endregion
                
                //region Step_6
                .toList();
                //endregion
        }
        //endregion

        for (Employee item : employeesWithinCircle) {
            EmployeeDetails detailedItem = new EmployeeDetails();
            detailedItem.setEmployeeName(item.getFirstName() + " " + item.getLastName());
            detailedItem.setLongitude(item.getAddress().getLocation().getLongitude());
            detailedItem.setLatitude(item.getAddress().getLocation().getLatitude());

            queryResults.add(detailedItem);
        }

        return queryResults;
    }

    public static class RunParams {
        private Integer radius;

        public Integer getRadius() {
            return radius;
        }

        public void setRadius(Integer radius) {
            this.radius = radius;
        }
    }
}
