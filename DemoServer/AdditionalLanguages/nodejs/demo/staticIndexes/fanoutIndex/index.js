const {
    AbstractJavaScriptIndexCreationTask
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');
const { Order } = require('../../common/models');

class Orders_ByProductDetails extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        this.map('orders', order => {
            return order.Lines.map(orderLine => {
                return {
                    productId: orderLine.Product,
                    productName: orderLine.ProductName
                };
            });
        });
    }
}

async function run ({ namePrefix }) {
    namePrefix = namePrefix || 'Chocolade';

    const session = documentStore.openSession();

    const orders = await session.query(Order, Orders_ByProductDetails)
        .whereStartsWith('productName', namePrefix)
        .all();

    return orders;
}

module.exports = { run, Orders_ByProductDetails };

