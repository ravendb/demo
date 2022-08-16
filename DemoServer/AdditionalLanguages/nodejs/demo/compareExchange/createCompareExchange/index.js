const { documentStore } = require('../../common/docStoreHolder');
//region Usings
const { PutCompareExchangeValueOperation, GetCompareExchangeValueOperation } = require('ravendb');
//endregion

const DEFAULT_COMPARE_EXCHANGE_KEY = 'abc@gmail.com';
const DEFAULT_COMPARE_EXCHANGE_VALUE = 'employee/1-A';

async function run ({ cmpXchgKey, cmpXchgValue }) {
    cmpXchgKey = cmpXchgKey || DEFAULT_COMPARE_EXCHANGE_KEY;
    cmpXchgValue = cmpXchgValue || DEFAULT_COMPARE_EXCHANGE_VALUE;

    //region Demo
    let result;

    //region Step_1
    const putCmpXchgOperation = new PutCompareExchangeValueOperation(cmpXchgKey, cmpXchgValue, 0);

    const putCmpXchgResult = await documentStore.operations.send(putCmpXchgOperation);
    //endregion

    //region Step_2
    const success = putCmpXchgResult.successful;
    const putValue = putCmpXchgResult.value;
    const putVersion = putCmpXchgResult.index;

    if (!success) {
        result = 'Key already exists';
    }
    //endregion

    //region Step_3
    const getCmpXchgOperation = new GetCompareExchangeValueOperation(cmpXchgKey, String);

    const getCmpXchgResult = await documentStore.operations.send(getCmpXchgOperation);
    //endregion

    //region Step_4
    const key = getCmpXchgResult.key;
    const currentValue = getCmpXchgResult.value;
    const currentValueVersion = getCmpXchgResult.index;
    const currentMetadata = getCmpXchgResult.metadata;
    //endregion
    //endregion

    result = result || 'Created a new Compare-Exchange Key: ' + key + ', Value: ' + currentValue + ', Value Version: ' + currentValueVersion;

    return result;
}

module.exports = { run };
