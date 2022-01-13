//region Usings
const { documentStore } = require('../../common/docStoreHolder');
const {AbstractCsharpIndexCreationTask} = require("ravendb");
//endregion
async function run (companyId) {
    //region Demo
    //region Step_1
    class OrdersQuantity_ByCompany extends AbstractCsharpIndexCreationTask {
    //endregion
        
        constructor() {
            super();
            //region Step_2
            this.map =
                "docs.Orders.Select(order => new {\n" +
                "   Company = order.Company,\n" +
                "   TotalItemsOrdered = order.Lines.Sum(orderLine => orderLine.Quantity)\n" +
                "})"
            ////endregion
            //region Step_3
            this.store("TotalItemsOrdered", "Yes")
            ////endregion
        }
    }
    //region Step_4
    await new OrdersQuantity_ByCompany().execute(documentStore);
    //endregion
    const session = documentStore.openSession();

    const ordersQuery = await session
        //region Step_5
        .query({indexName: "OrdersQuantity/ByCompany"})
        .whereEquals("Company", companyId)
        //endregion
        //region Step_6
        .selectFields(["OrderedAt", "TotalItemsOrdered"])
        //endregion

    //region Step_7
    const ordersDetails = ordersQuery.all()
    //endregion
    //endregion
    return ordersDetails;
}

module.exports = { run };
