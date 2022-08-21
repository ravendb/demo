const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');
//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion

//region Demo
//region Step_1
class Products_ByPrice extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map('products', product => {
            return {
                productName: product.Name,
                originalPrice: product.PricePerUnit,
                salePrice: calcSalePrice(product.PricePerUnit),
                profitPrice: calcProfitPrice(product.PricePerUnit)
            };
        });
        //endregion

        //region Step_3
        this.addSource('CalcSalePrice', calcSalePrice);
        this.addSource('CalcProfitPrice', calcProfitPrice);
        //endregion
    }
}

//region Step_4
function calcSalePrice(price) {
    return price - price * 0.25;
}

function calcProfitPrice(price) {
    return price + price * 0.25;
}
//endregion
//endregion

async function run ({ price }) {
    price = price || 5;

    //region Demo
    const session = documentStore.openSession();

    //region Step_5
    const lowCostProducts = await session.query(Product, Products_ByPrice)
        .whereLessThan('salePrice', price)
        .orderBy('salePrice')
        .all();
    //endregion
    //endregion    

    // Manipulate results to show because index fields are Not stored..
    const productsData = lowCostProducts.map(item => {
        return {
            productName: item.Name,
            originalPrice: item.PricePerUnit,
            salesPrice: item.PricePerUnit - item.PricePerUnit * 0.25,
            profitPrice: item.PricePerUnit + item.PricePerUnit * 0.25
        };
    });

    return productsData;
}

module.exports = { run, Products_ByPrice };
