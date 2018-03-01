package net.ravendb.demo.basic;

import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Order;
import net.ravendb.demo.indexes.CostlyOrders;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.time.Duration;
import java.util.List;

@Controller
public class StaticQuery {
    @GetMapping("/basic/staticQuery")
    public List<Order> staticQuery(
            @RequestParam(value = "highPrice", defaultValue = "500") String highPriceStr,
            @RequestParam(value = "delayDays", defaultValue = "35") String delayDaysStr) {

        int highPrice = Integer.parseInt(highPriceStr);
        int delayDeys = Integer.parseInt(delayDaysStr);

        //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {

            IDocumentQuery<Order> query = session.query(CostlyOrders.Result.class, CostlyOrders.class)
                    .whereGreaterThan("Price", highPrice)
                    .whereGreaterThan("Delay", Duration.ofDays(delayDeys).toNanos() / 100)
                    .ofType(Order.class);

            List<Order> problematicOrders = query.toList();

            return problematicOrders;
        }
    }
}
