package net.ravendb.demo.compareExchange.createCompareExchange;
//region Using
import net.ravendb.client.documents.operations.compareExchange.CompareExchangeResult;
import net.ravendb.client.documents.operations.compareExchange.CompareExchangeValue;
import net.ravendb.client.documents.operations.compareExchange.GetCompareExchangeValueOperation;
import net.ravendb.client.documents.operations.compareExchange.PutCompareExchangeValueOperation;
import net.ravendb.client.documents.session.IMetadataDictionary;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;

public class CreateCompareExchange {
    public String run(RunParams runParams) {
        String result = null;
        String cmpXchgKey = runParams.getCmpXchgKey();
        String cmpXchgValue = runParams.getCmpXchgValue();
        //region Demo
        //region Step_1
        PutCompareExchangeValueOperation<String> putCmpXchgOperation = new PutCompareExchangeValueOperation<>(cmpXchgKey, cmpXchgValue, 0);

        CompareExchangeResult<String> putCmpXchgResult = DocumentStoreHolder.store.operations().send(putCmpXchgOperation);
        //endregion
        
        //region Step_2
        boolean success = putCmpXchgResult.isSuccessful();
        String putValue = putCmpXchgResult.getValue();
        long putVersion = putCmpXchgResult.getIndex();
        
        if (success == false)
            result = "Key already exists";
        //endregion

        //region Step_3
        GetCompareExchangeValueOperation<String> getCmpXchgOperation = new GetCompareExchangeValueOperation<String>(String.class, cmpXchgKey);

        CompareExchangeValue<String> getCmpXchgResult = DocumentStoreHolder.store.operations().send(getCmpXchgOperation);
        //endregion

        //region Step_4
        String key = getCmpXchgResult.getKey();
        String currentValue = getCmpXchgResult.getValue();
        long currentValueVersion = getCmpXchgResult.getIndex();
        IMetadataDictionary currentMetadata = getCmpXchgResult.getMetadata();
        //endregion
        //endregion
        return result = (result == "Key already exists") ? result : new StringBuilder().append("Created a new Compare-Exchange Key: ").append(key).append(", Value: ").append(currentValue).append(", Value Version: ").append(currentValueVersion).toString();
    }
    //add params
    public static class RunParams {
        private String cmpXchgKey;
        private String cmpXchgValue;

        public String getCmpXchgKey() {
            return cmpXchgKey;
        }

        public void setCmpXchgKey(String cmpXchgKey) {
            this.cmpXchgKey = cmpXchgKey;
        }

        public String getCmpXchgValue() {
            return cmpXchgValue;
        }

        public void setCmpXchgValue(String cmpXchgValue) {
            this.cmpXchgValue = cmpXchgValue;
        }
    }
}
