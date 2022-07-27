package net.ravendb.demo.compareExchange.createCompareExchange;
//region Using
import net.ravendb.client.documents.operations.compareExchange.CompareExchangeResult;
import net.ravendb.client.documents.operations.compareExchange.CompareExchangeValue;
import net.ravendb.client.documents.operations.compareExchange.GetCompareExchangeValueOperation;
import net.ravendb.client.documents.operations.compareExchange.PutCompareExchangeValueOperation;
import net.ravendb.client.documents.session.IMetadataDictionary;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

public class CreateCompareExchange {

    public String run(RunParams runParams) {
        String cmpXchgKey = ObjectUtils.firstNonNull(runParams.getCmpXchgKey(), "abc@gmail.com");
        String cmpXchgValue = ObjectUtils.firstNonNull(runParams.getCmpXchgValue(), "employee/1-A");
        String result = null;
        
        //region Demo
        //region Step_1
        PutCompareExchangeValueOperation<String> putCmpXchgOperation
            = new PutCompareExchangeValueOperation<>(cmpXchgKey, cmpXchgValue, 0);

        CompareExchangeResult<String> putCmpXchgResult
            = DocumentStoreHolder.store.operations().send(putCmpXchgOperation);
        //endregion
        
        //region Step_2 
        boolean success = putCmpXchgResult.isSuccessful();
        String putValue = putCmpXchgResult.getValue();
        long putVersion = putCmpXchgResult.getIndex();

        if (!success) {
            result = "Key already exists";
        }
        //endregion

        //region Step_3
        GetCompareExchangeValueOperation<String> getCmpXchgOperation
            = new GetCompareExchangeValueOperation<>(String.class, cmpXchgKey);

        CompareExchangeValue<String> getCmpXchgResult
            = DocumentStoreHolder.store.operations().send(getCmpXchgOperation);
        //endregion

        //region Step_4
        String key = getCmpXchgResult.getKey();
        String currentValue = getCmpXchgResult.getValue();
        long currentValueVersion = getCmpXchgResult.getIndex();
        IMetadataDictionary currentMetadata = getCmpXchgResult.getMetadata();
        //endregion
        //endregion

        result = ObjectUtils.firstNonNull(result,
            "Created a new Compare-Exchange Key: " + key + ", Value: " + currentValue + ", Value Version: " + currentValueVersion);

        return result;
    }

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
