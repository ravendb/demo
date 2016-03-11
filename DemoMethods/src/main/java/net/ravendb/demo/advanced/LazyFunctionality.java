package net.ravendb.demo.advanced;

import net.ravendb.abstractions.basic.Lazy;
import net.ravendb.client.IDocumentSession;
import net.ravendb.demo.DocumentStoreHolder;
import net.ravendb.demo.entities.*;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class LazyFunctionality {

    @RequestMapping("/Advanced/LazyFunctionality")
    public Result lazyFunctionality(
            @RequestParam(value = "companyId", defaultValue = "companies/20") String companyId) {

        QOrder o = QOrder.order;

        try (IDocumentSession session = DocumentStoreHolder.getStore().openSession()) {
            Lazy<Company> company = session
                    .advanced()
                    .lazily()
                    .load(Company.class, companyId);

            Lazy<Integer> countOfOrders = session
                    .query(Order.class)
                    .where(o.company.eq(companyId))
                    .countLazily();

            session.advanced().eagerly().executeAllPendingLazyOperations();

            return new Result(company.getValue().getName(),
                    countOfOrders.getValue(),
                    session.advanced().getNumberOfRequests());
        }
    }

    public static class Result {
        private String companyName;
        private int numberOfOrders;
        private int numberOfRequests;

        public Result(String companyName, int numberOfOrders, int numberOfRequests) {
            this.companyName = companyName;
            this.numberOfOrders = numberOfOrders;
            this.numberOfRequests = numberOfRequests;
        }

        public String getCompanyName() {
            return companyName;
        }

        public void setCompanyName(String companyName) {
            this.companyName = companyName;
        }

        public int getNumberOfOrders() {
            return numberOfOrders;
        }

        public void setNumberOfOrders(int numberOfOrders) {
            this.numberOfOrders = numberOfOrders;
        }

        public int getNumberOfRequests() {
            return numberOfRequests;
        }

        public void setNumberOfRequests(int numberOfRequests) {
            this.numberOfRequests = numberOfRequests;
        }
    }
}
