package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;

public class NameAndCountry extends AbstractIndexCreationTask {

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
       // empty
    }
}
