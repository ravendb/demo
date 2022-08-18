//region Usings
const { AbstractJavaScriptIndexCreationTask, QueryData } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Order } = require('../../common/models');

//region Demo
//region Step_1
class OrdersQuantity_ByCompany extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map('orders', order => {
            return {
                company: order.Company,
                totalItemsOrdered: order.Lines.reduce((p, c) => p + c.Quantity, 0)
            };
        });
        //endregion

        //region Step_3
        this.store('totalItemsOrdered', 'Yes');
        //endregion
    }
}

//region Step_4
class OrderProjectedDetails {
    constructor () {
        this.orderedAt = null;
        this.totalItemsOrdered = null;
    }
}
//endregion
//endregion

async function run ({ companyId }) {
    await new OrdersQuantity_ByCompany().execute(documentStore);

    //region Demo
    const session = documentStore.openSession();

    const ordersQuery = session
        //region Step_5
        .query(Order, OrdersQuantity_ByCompany)
        .whereEquals('company', companyId)
        //endregion
        //region Step_6
        .selectFields(new QueryData(['OrderedAt', 'totalItemsOrdered'], ['orderedAt', 'totalItemsOrdered']),
            OrderProjectedDetails);
        //endregion

    //region Step_7
    const ordersDetails = await ordersQuery.all();
    //endregion
    //endregion

    return ordersDetails;
}

module.exports = { run };
