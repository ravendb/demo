const {
    AbstractJavaScriptIndexCreationTask,
    QueryData
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');
const { Order } = require('../../common/models');

class OrdersQuantity_ByCompany extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        this.map('orders', order => {
            return {
                company: order.Company,
                totalItemsOrdered: order.Lines.reduce((p, c) => p + c.Quantity, 0)
            };
        });

        this.store('totalItemsOrdered', 'Yes');
    }
}

class OrderProjectedDetails {
    constructor () {
        this.orderedAt = null;
        this.totalItemsOrdered = null;
    }
}

async function run ({ companyId }) {
    await new OrdersQuantity_ByCompany().execute(documentStore);

    const session = documentStore.openSession();

    const ordersQuery = session
        .query(Order, OrdersQuantity_ByCompany)
        .whereEquals('company', companyId)
        .selectFields(new QueryData(['OrderedAt', 'totalItemsOrdered'],
            ['orderedAt', 'totalItemsOrdered']), OrderProjectedDetails);

    const ordersDetails = await ordersQuery.all();

    return ordersDetails;
}

module.exports = { run };
