package net.ravendb.demo.spatial.spatialIndex;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;

import java.util.List;

public class SpatialIndex {
    //region Demo
    //region Step_1
    public static class Companies_ByLocation extends AbstractIndexCreationTask {
    //endregion
    
        //region Step_2
        public static class IndexEntry {
            private String companyName;
            private Object locationCoordinates ;
        
            public String getCompanyName() {
                return companyName;
            }
        
            public void setCompanyName(String companyName) {
                this.companyName = companyName;
            }
        
            public Object getLocationCoordinates () {
                return locationCoordinates;
            }
        
            public void setLocationCoordinates (Object locationCoordinates) {
                this.locationCoordinates = locationCoordinates;
            }
        }
        //endregion
        
        public Companies_ByLocation() {
        
            //region Step_3
            map = "docs.Companies.Select(company => new {" +
                  "    CompanyName = company.Name," +
                  "    LocationCoordinates = this.CreateSpatialField(((double ? ) company.Address.Location.Latitude), ((double ? ) company.Address.Location.Longitude))" +
                  "})";
            //endregion
            
            //region Step_4
            spatial("LocationCoordinates", factory -> factory.geography().quadPrefixTreeIndex(5));
            //endregion
        }
    }
    //endregion

    public List<Company> run() {
        //region Demo
        List<Company> companiesNearSeattle;
   
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_5
            String wktPolygon = "POLYGON ((-125.06868394091362 41.855902525062724," +
                " -109.99544175341362 41.888625730467275," +
                " -116.76301987841362 50.59949235579767,"  +
                " -125.26643784716362 50.592518406260766," +
                " -125.06868394091362 41.855902525062724))";
            //endregion
            
            double seattleLatitude = 47.6062;
            double seattleLongitude = -122.3321;

            //region Step_6
            companiesNearSeattle = session.query(Company.class, Companies_ByLocation.class)
                .spatial("LocationCoordinates", spatialCriteria -> spatialCriteria.within(wktPolygon))
                .orderByDistance("LocationCoordinates", seattleLatitude, seattleLongitude)
                .toList();
            //endregion    
        }
        //endregion

        return companiesNearSeattle;
    }
}
