package net.ravendb.demo.relatedDocuments.createRelatedDocuments;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Category;
import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.common.models.Supplier;

public class CreateRelatedDocuments {

    public void run(RunParams runParams) {
        String supplierName = runParams.getSupplierName();
        String supplierPhone = runParams.getSupplierPhone();
        String productName = runParams.getProductName();

        //region Demo
        //region Step_1
        Supplier supplier = new Supplier();
        supplier.setName(supplierName);
        supplier.setPhone(supplierPhone);

        Category category = new Category();
        category.setName("NoSQL Databases");
        category.setDescription("Non-relational databases");
        //endregion

        //region Step_2
        Product product = new Product();
        product.setName(productName);
        //endregion

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_3
            session.store(supplier);
            session.store(category);
            //endregion

            //region Step_4
            product.setSupplier(supplier.getId());
            product.setCategory(category.getId());
            //endregion

            //region Step_5
            session.store(product);
            //endregion

            //region Step_6
            session.saveChanges();
            //endregion
        }
        //endregion
    }

    public static class RunParams {
        private String supplierName;
        private String supplierPhone;
        private String productName;

        public String getSupplierName() {
            return supplierName;
        }

        public void setSupplierName(String supplierName) {
            this.supplierName = supplierName;
        }

        public String getSupplierPhone() {
            return supplierPhone;
        }

        public void setSupplierPhone(String supplierPhone) {
            this.supplierPhone = supplierPhone;
        }

        public String getProductName() {
            return productName;
        }

        public void setProductName(String productName) {
            this.productName = productName;
        }
    }
}
