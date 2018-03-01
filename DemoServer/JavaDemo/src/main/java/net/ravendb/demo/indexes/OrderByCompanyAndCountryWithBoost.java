package net.ravendb.demo.indexes;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;

public class OrderByCompanyAndCountryWithBoost extends AbstractIndexCreationTask {

    public OrderByCompanyAndCountryWithBoost() {
        map = "from order in docs.Orders " +
                "select new { " +
                "  ShipTo_City = order.ShipTo.City.Boost(10)," +
                " ShipTo_Country = order.ShipTo.Country " +
                "}";
    }
}
