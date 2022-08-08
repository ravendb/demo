const { documentStore } = require('../../common/docStoreHolder');
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
const { Product } = require('../../common/models');

class DiscountUtils {
    static CalcSalePrice (num) {
        // this is empty impl
    }

    static CalcProfitPrice (num) {
        // this is empty impl
    }
}

const ADDITIONAL_SOURCE = 'public static class DiscountUtils' +
    '{' +
    '    public static decimal CalcSalePrice(decimal price)' +
    '    {' +
    '        return price - price / 100M * 25M;' +
    '    }' +
    '     ' +
    '    public static decimal CalcProfitPrice(decimal price)' +
    '    {' +
    '        return price + price / 100M * 25M;' +
    '    }' +
    '}';

class Products_ByPrice extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        function calcSalePrice (price) {
            return price * 0.75;
        }

        function calcProfitPrice (price) {
            return price * 0.25;
        }

        this.map('products', product => {
            return {
                productName: product.Name,
                originalPrice: product.PricePerUnit,
                salePrice: calcSalePrice(product.PricePerUnit),
                profitPrice: calcProfitPrice(product.PricePerUnit)
            };
        });

        this.addSource('calcSalePrice', calcSalePrice);
        this.addSource('calcProfitPrice', calcProfitPrice);
    }
}

async function run ({ price }) {
    price = price || 5;

    const session = documentStore.openSession();

    const lowCostProducts = await session.query(Product, Products_ByPrice)
        .whereLessThan('salePrice', price)
        .orderBy('salePrice')
        .all();

    // Manipulate results to show because index fields are Not stored..
    const productsData = lowCostProducts.map(item => {
        return {
            productName: item.Name,
            originalPrice: item.PricePerUnit,
            salesPrice: item.PricePerUnit * 0.75,
            profitPrice: item.PricePerUnit * 0.25
        };
    });

    return productsData;
}

module.exports = { run, Products_ByPrice };
