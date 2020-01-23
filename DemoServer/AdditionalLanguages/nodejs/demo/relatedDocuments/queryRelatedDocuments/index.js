const { documentStore } = require('../../common/docstoreholder');

async function run () {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    const shippedOrders = await session.query({ collection: 'orders' })
        .include('Lines.Product')
        .whereExists('ShippedAt')
        .all();
    //endregion

    //region Step_2
    for (const shippedOrder of shippedOrders) {
        const productIds = shippedOrder.Lines.map(x => x.Product);
        for (let i = 0; i < productIds.length; i++) {
            //region Step_3
            const product = await session.load(productIds[i]);
            product.UnitsOnOrder = product.UnitsOnOrder + shippedOrder.Lines[i].Quantity;
            //endregion
        }
    }
    //endregion

    //region Step_4
    await session.saveChanges();
    //endregion
    //endregion

    return shippedOrders;
}

module.exports = { run };
