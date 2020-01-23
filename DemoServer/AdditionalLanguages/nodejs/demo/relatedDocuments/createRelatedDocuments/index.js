const { Supplier, Category, Product } = require('../../common/models');
const { documentStore } = require('../../common/docStoreHolder');

async function run ({ supplierName, supplierPhone, productName }) {
    //region Demo
    //region Step_1
    const supplier = new Supplier();
    supplier.name = supplierName;
    supplier.phone = supplierPhone;

    const category = new Category();
    category.name = 'NoSQL Databases';
    category.description = 'Non-relational databases';
    //endregion

    //region Step_2
    const product = new Product();
    product.name = productName;
    //endregion

    const session = documentStore.openSession();
    //region Step_3
    await session.store(supplier);
    await session.store(category);
    //endregion

    //region Step_4
    product.supplier = supplier.id;
    product.category = category.id;
    //endregion

    //region Step_5
    await session.store(product);
    //endregion

    //region Step_6
    await session.saveChanges();
    //endregion
    //endregion

    return product;
}

module.exports = { run };
