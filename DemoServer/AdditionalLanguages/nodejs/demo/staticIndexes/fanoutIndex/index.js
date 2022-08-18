//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Order } = require('../../common/models');

//region Demo
//region Step_1
class Orders_ByProductDetails extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map('orders', order => {
            return order.Lines.map(orderLine => {
                return {
                    productId: orderLine.Product,
                    productName: orderLine.ProductName
                };
            });
        });
        //endregion
    }
}
//endregion

async function run ({ namePrefix }) {
    namePrefix = namePrefix || 'Chocolade';

    //region Demo
    const session = documentStore.openSession();

    //region Step_3
    const orders = await session.query(Order, Orders_ByProductDetails)
        .whereStartsWith('productName', namePrefix)
        .all();
    //endregion

    //endregion
    return orders;
}

module.exports = { run, Orders_ByProductDetails };

