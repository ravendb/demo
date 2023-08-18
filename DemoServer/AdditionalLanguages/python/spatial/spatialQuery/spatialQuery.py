from demo_example import Example, RunParamsBase
from models import Employee

#region Usings
from ravendb import PointField
from ravendb.documents.indexes.spatial.configuration import (
    SpatialRelation,
    SpatialUnits
)
#endregion

class RunParams(RunParamsBase):
    def __init__(self, radius: int = None):
        self.radius = radius

class EmployeeDetails:
    def __init__(self, employee_name: str = None, longitude: float = None, latitude: float = None):
        self.employee_name = employee_name
        self.longitude = longitude
        self.latitude = latitude

class SpatialQuery(Example):
    def run(self, run_params: RunParams):
        radius = run_params.radius or 2
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            center_point_lng = -122.31550148
            center_point_lat = 47.63016419999999

            #region Step_1
            wkt_circle = f"CIRCLE({center_point_lng} {center_point_lat} d={radius})"
            #endregion

            #region Step_2
            spatial_query = (
                session.query(object_type=Employee)
                .spatial(
                    #endregion
                    #region Step_3
                    PointField("Address.Location.Latitude", "Address.Location.Longitude"),
                    #endregion
                    #region Step_4
                    lambda spatial_cirteria: spatial_cirteria.relates_to_shape(
                        wkt_circle, SpatialRelation.WITHIN, SpatialUnits.MILES
                    )
                    #endregion
                    #region Step_5
                )
                .order_by_distance(
                    PointField("Address.Location.Latitude", "Address.Location.Longitude"),
                    center_point_lat,
                    center_point_lng,
                )
            )
            #endregion
            
            #region Step_6
            employees_within_circle = list(spatial_query)
            #endregion            
        #endregion
            
            query_results = [
                EmployeeDetails(
                    f"{item.FirstName} {item.LastName}",
                    item.Address.Location.Longitude,
                    item.Address.Location.Latitude,
                )
                for item in employees_within_circle
            ]
            
            return query_results
