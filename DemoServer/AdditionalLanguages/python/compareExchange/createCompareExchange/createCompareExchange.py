from demo_example import Example

#region Usings
from ravendb import (
    PutCompareExchangeValueOperation,
    GetCompareExchangeValueOperation
)
#endregion

class RunParams:
    def __init__(self, cmp_xchg_key: str = None, cmp_xchg_value: str = None):
        self.cmp_xchg_key = cmp_xchg_key
        self.cmp_xchg_value = cmp_xchg_value

class CreateCompareExchange(Example):
    def run(self, run_params: RunParams) -> str:
        cmpXchgKey = run_params.cmp_xchg_key or "abc@gmail.com"
        cmpXchgValue = run_params.cmp_xchg_value or "employee/1-A"
        result = None

        #region Demo
        #region Step_1
        put_cmp_xchg_operation = PutCompareExchangeValueOperation(cmpXchgKey, cmpXchgValue, 0)
        put_cmp_xchg_result = self.document_store_holder.store().operations.send(put_cmp_xchg_operation)
        #endregion

        #region Step_2
        success = put_cmp_xchg_result.successful
        put_value = put_cmp_xchg_result.value
        put_version = put_cmp_xchg_result.index

        if not success:
            result = "Key already exists"
        #endregion

        #region Step_3
        get_cmp_xchg_operation = GetCompareExchangeValueOperation(cmpXchgKey, str)
        get_cmp_xchg_result = self.document_store_holder.store().operations.send(get_cmp_xchg_operation)
        #endregion

        #region Step_4
        key = get_cmp_xchg_result.key
        current_value = get_cmp_xchg_result.value
        current_value_version = get_cmp_xchg_result.index
        current_metadata = get_cmp_xchg_result.metadata
        #endregion
        #endregion

        results = (
            result
            or f"Created a new Compare-Exchange "
               f"Key: {key}, "
               f"Value: {current_value}, "
               f"Value Version: {current_value_version}"
        )

        return results
