package net.ravendb.demo.basic;

import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Order;
import net.ravendb.demo.indexes.CostlyOrders;
import net.ravendb.demo.indexes.QCostlyOrders_Result;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.time.Duration;
import java.util.List;

@Controller
public class StaticQuery {
    @RequestMapping("/Basic/StaticQuery")
    public List<Order> staticQuery(
            @RequestParam(value = "highPrice", defaultValue = "500") int highPrice,
            @RequestParam(value = "delayDays", defaultValue = "35") int delayDays) {

        //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            QCostlyOrders_Result c = QCostlyOrders_Result.result;

            long delayInNanos = Duration.ofDays(delayDays).toNanos() / 100;

            List<Order> problematicOrder = session.query(CostlyOrders.Result.class, CostlyOrders.class)
                    .where(c.price.gt(highPrice).and(c.delay.gt(delayInNanos)))
                    .as(Order.class)
                    .toList();
            return problematicOrder;
        }
    }
}
