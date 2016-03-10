package net.ravendb.demo.indexes;

import com.mysema.query.annotations.QueryEntity;
import net.ravendb.abstractions.indexing.FieldIndexing;
import net.ravendb.abstractions.indexing.FieldStorage;
import net.ravendb.client.indexes.AbstractMultiMapIndexCreationTask;

public class NameAndCountry extends AbstractMultiMapIndexCreationTask {

    @QueryEntity
    public static class Result {
        private String name;
        private String country;
        private String id;

        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        public String getCountry() {
            return country;
        }

        public void setCountry(String country) {
            this.country = country;
        }

        public String getId() {
            return id;
        }

        public void setId(String id) {
            this.id = id;
        }
    }

    public NameAndCountry() {
        QNameAndCountry_Result r = QNameAndCountry_Result.result;

        addMap("from e in docs.Employees " +
                "select new { " +
                "  Name = (e.FirstName + \" \") + e.LastName, " +
                "  Country = e.Address.Country " +
                "}");

        addMap("from c in docs.Companies " +
                "select new { " +
                "  Name = c.Name, " +
                "  Country = c.Address.Country " +
                "}");

        addMap("from s in docs.Suppliers " +
                "select new { " +
                "  Name = s.Name, " +
                "  Country = s.Address.Country " +
                "}");

        index(r.country, FieldIndexing.ANALYZED);
        store(r.name, FieldStorage.YES);
        store(r.name, FieldStorage.YES);
    }
}
