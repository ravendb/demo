from demo_example import Example
from models import Company

#region Usings
from ravendb import (
    AbstractIndexCreationTask
)
#endregion

#region Demo
#region Step_1
class Companies_ByLocation(AbstractIndexCreationTask):
#endregion

    #region Step_2
    class IndexEntry:
        def __init__(self, company_name: str = None, location_coordinates: object = None):
            self.company_name = company_name
            self.location_coordinates = location_coordinates
    #endregion

    def __init__(self):
        #region Step_3
        super().__init__()
        self.map = (
            "docs.Companies.Select(company => new { "
            "    company_name = company.Name, "
            "    location_coordinates = this.CreateSpatialField(((double ? ) company.Address.Location.Latitude), ((double ? ) company.Address.Location.Longitude)) "
            "}) "
        )
        #endregion
        
        #region Step_4
        self._spatial(
            "location_coordinates",
            lambda factory: factory.geography().quad_prefix_tree_index(5),
        )
        #endregion
#endregion

class SpatialIndex(Example):
    def run(self, run_params=None):
        Companies_ByLocation().execute(self.document_store_holder.store())
        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_5
            wkt_polygon = (
                "POLYGON ((-125.06868394091362 41.855902525062724,"
                " -109.99544175341362 41.888625730467275,"
                " -116.76301987841362 50.59949235579767,"
                " -125.26643784716362 50.592518406260766,"
                " -125.06868394091362 41.855902525062724))"
            )
            #endregion

            seattle_latitude = 47.6062
            seattle_longitude = -122.3321

            #region Step_6
            companies_near_seattle = list(
                session.query_index_type(Companies_ByLocation, Company)
                .spatial(
                    "location_coordinates",
                    lambda spatial_criteria: spatial_criteria.within(wkt_polygon),
                )
                .order_by_distance("location_coordinates", seattle_latitude, seattle_longitude)
            )
            #endregion
        #endregion

        return companies_near_seattle
