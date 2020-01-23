const assert = require('assert');
const { documentStore } = require('../demo/common/docStoreHolder');

describe('Related documents', function () {
    it('create', async function () {
        const result = await require('../demo/relatedDocuments/createRelatedDocuments').run({
            productName: 'Test' + parseInt(Math.random() * 1000),
            supplierName: 'test-supplier',
            supplierPhone: '1515151'
        });
        assert.ok(
            result);
        const session = documentStore.openSession();
        const p = await session.load(result.id);
        assert.ok(p.supplier);
        assert.ok(p.category);
    });
    it('index', async function () {
        const result = await require('../demo/relatedDocuments/indexRelatedDocuments').run({
            categoryName: 'Seafood'
        });
        assert.ok(result && result.length);
        assert.ok(result[0]);
    });
    it('load', async function () {
        const result = await require('../demo/relatedDocuments/loadRelatedDocuments').run({
            pricePerUnit: 0.01,
            phone: '123556'
        });

        const session = documentStore.openSession();
        const product = await session
            .include('Supplier')
            .load(result.product.id);

        const supplier = await session.load(product.Supplier);
        assert.equal(product.PricePerUnit, 0.01);
        assert.equal(supplier.Phone, '123556');
    });
});
