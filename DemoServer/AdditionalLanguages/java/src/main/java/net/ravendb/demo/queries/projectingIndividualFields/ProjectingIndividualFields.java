package net.ravendb.demo.queries.projectingIndividualFields;

//region Usings
import net.ravendb.client.documents.queries.QueryData;
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;

public class ProjectingIndividualFields {
    //region Demo
    public static class CompanyDetails {
        private String companyName;
        private String city;
        private String country;
    //endregion
        public String getCompanyName() {
            return companyName;
        }

        public void setCompanyName(String companyName) {
            this.companyName = companyName;
        }

        public String getCity() {
            return city;
        }

        public void setCity(String city) {
            this.city = city;
        }

        public String getCountry() {
            return country;
        }

        public void setCountry(String country) {
            this.country = country;
        }
    //region Demo
    }
    //endregion

    public List<CompanyDetails> run() {
        List<CompanyDetails> projectedResults;

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<CompanyDetails> projectedQuery = session.query(Company.class)
            //endregion
            //region Step_2
                .selectFields(CompanyDetails.class,
                    new QueryData(
                        new String[] { "Name", "Address.City", "Address.Country" },
                        new String[] { "CompanyName", "City", "Country" }));
            //endregion

            //region Step_3
            projectedResults = projectedQuery.toList();
            //endregion
        }
        //endregion
        return projectedResults;
    }
}
