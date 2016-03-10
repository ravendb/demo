package net.ravendb.demo.entities;

import com.mysema.query.annotations.QueryEntity;

import java.util.List;

@QueryEntity
public class Region {
    private String id;
    private String name;
    private List<Territory> territories;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Territory> getTerritories() {
        return territories;
    }

    public void setTerritories(List<Territory> territories) {
        this.territories = territories;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }
}
