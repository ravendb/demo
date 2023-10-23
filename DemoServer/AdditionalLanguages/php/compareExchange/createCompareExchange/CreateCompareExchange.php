<?php

namespace RavenDB\Demo\compareExchange\createCompareExchange;

//region Using
use RavenDB\Documents\Operations\CompareExchange\CompareExchangeResult;
use RavenDB\Documents\Operations\CompareExchange\CompareExchangeValue;
use RavenDB\Documents\Operations\CompareExchange\GetCompareExchangeValueOperation;
use RavenDB\Documents\Operations\CompareExchange\PutCompareExchangeValueOperation;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

class CreateCompareExchange
{
    public function __invoke(RunParams $runParams): string
    {
        $cmpXchgKey = $runParams->getCmpXchgKey() ?? "abc@gmail.com";
        $cmpXchgValue = $runParams->getCmpXchgValue() ?? "employee/1-A";
        $result = null;

        //region Demo
        //region Step_1
        $putCmpXchgOperation = new PutCompareExchangeValueOperation($cmpXchgKey, $cmpXchgValue, 0);

        /** @var CompareExchangeResult $putCmpXchgResult */
        $putCmpXchgResult = DocumentStoreHolder::getStore()->operations()->send($putCmpXchgOperation);
        //endregion

        //region Step_2
        $success = $putCmpXchgResult->isSuccessful();
        $putValue = $putCmpXchgResult->getValue();
        $putVersion = $putCmpXchgResult->getIndex();

        if (!$success) {
            $result = "Key already exists";
        }
        //endregion

        //region Step_3
        $getCmpXchgOperation = new GetCompareExchangeValueOperation(null, $cmpXchgKey);

        /** @var CompareExchangeValue $getCmpXchgResult */
        $getCmpXchgResult = DocumentStoreHolder::getStore()->operations()->send($getCmpXchgOperation);
        //endregion

        //region Step_4
        $key = $getCmpXchgResult->getKey();
        $currentValue = $getCmpXchgResult->getValue();
        $currentValueVersion = $getCmpXchgResult->getIndex();
        $currentMetadata = $getCmpXchgResult->getMetadata();
        //endregion
        //endregion

        $result = $result ?? "Created a new Compare-Exchange Key: " . $key . ", Value: " . $currentValue . ", Value Version: " . $currentValueVersion;

        return $result;
    }
}
