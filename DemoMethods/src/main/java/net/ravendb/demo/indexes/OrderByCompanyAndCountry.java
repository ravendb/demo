package net.ravendb.demo.indexes;

import net.ravendb.client.indexes.AbstractIndexCreationTask;

public class OrderByCompanyAndCountry extends AbstractIndexCreationTask {

    public OrderByCompanyAndCountry() {
        map = "from order in docs.Orders " +
                "select new { " +
                "   ShipTo_City = order.ShipTo.City, " +
                "   ShipTo_Country = order.ShipTo.Country" +
                "}";
    }
}
