package net.ravendb.demo.relatedDocuments.loadRelatedDocuments;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.common.models.Supplier;

public class LoadRelatedDocuments {

    public void run(RunParams runParams) {
        double pricePerUnit = runParams.getPricePerUnit();
        String phone = runParams.getPhone();

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            Product product = session
                .include("Supplier")
                .load(Product.class, "products/34-A");
            //endregion

            //region Step_2
            Supplier supplier = session.load(Supplier.class, product.getSupplier());
            //endregion

            //region Step_3
            product.setPricePerUnit(pricePerUnit);
            supplier.setPhone(phone);
            //endregion

            //region Step_4
            session.saveChanges();
            //endregion
        }
        //endregion
    }

    public static class RunParams {
        private double pricePerUnit;
        private String phone;

        public double getPricePerUnit() {
            return pricePerUnit;
        }

        public void setPricePerUnit(double pricePerUnit) {
            this.pricePerUnit = pricePerUnit;
        }

        public String getPhone() {
            return phone;
        }

        public void setPhone(String phone) {
            this.phone = phone;
        }
    }
}
