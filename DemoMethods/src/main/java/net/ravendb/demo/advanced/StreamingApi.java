package net.ravendb.demo.advanced;

import net.ravendb.abstractions.basic.CloseableIterator;
import net.ravendb.abstractions.data.StreamResult;
import net.ravendb.client.IDocumentSession;
import net.ravendb.client.linq.IRavenQueryable;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.Order;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
public class StreamingApi {

    @RequestMapping("/Advanced/StreamingApi")
    public int streamingApi() {

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            IRavenQueryable<Order> query = session.query(Order.class, "OrderByCompanyAndCountry");

            int count = 0;

            try (CloseableIterator<StreamResult<Order>> e = session.advanced().stream(query)) {
                while (e.hasNext()) {
                    StreamResult<Order> next = e.next();
                    count++;
                }
            }

            return count;
        }
    }
}
