package net.ravendb.demo.basic;

import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Order;
import net.ravendb.demo.entities.QOrder;
import net.ravendb.demo.indexes.OrderByCompanyAndCountry;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class BoostingDisabled {

    @RequestMapping("/Basic/BoostingDisabled")
    public List<Order> boostingDisabled(
            @RequestParam(value = "city", defaultValue = "London") String city,
            @RequestParam(value = "country", defaultValue = "Denmark") String country) {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            QOrder order = QOrder.order;

            List<Order> orders = session.query(Order.class, OrderByCompanyAndCountry.class)
                    .where(order.shipTo().city.eq(city).or(order.shipTo().country.eq(country)))
                    .toList();

            return orders;
        }
    }
}
