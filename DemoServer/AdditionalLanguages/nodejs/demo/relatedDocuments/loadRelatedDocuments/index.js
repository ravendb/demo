const { documentStore } = require('../../common/docStoreHolder');

async function run ({ pricePerUnit, phone }) {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    const product = await session
        .include('Supplier')
        .load('products/34-A');
    //endregion

    //region Step_2
    const supplier = await session.load(product.Supplier);
    //endregion

    //region Step_3
    product.PricePerUnit = pricePerUnit;
    supplier.Phone = phone;
    //endregion

    //region Step_4
    await session.saveChanges();
    //endregion
    //endregion

    return { product, supplier };
}

module.exports = { run };
