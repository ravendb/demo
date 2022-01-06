using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Operations.CompareExchange;
#endregion

namespace DemoServer.Controllers.Demos.CompareExchange.CreateCompareExchange
{
    public class CreateCompareExchangeController : DemoCodeController
    {
        public CreateCompareExchangeController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string cmpXchgKey = runParams.CmpXchgKey ?? "abc@gmail.com";
            string cmpXchgValue = runParams.CmpXchgValue?? "employee/1-A";
            
            string result = null;
            
            #region Demo
            #region Step_1
            var putCmpXchgOperation =
                new PutCompareExchangeValueOperation<string>(cmpXchgKey, cmpXchgValue, 0);
            
            CompareExchangeResult<string> putCmpXchgResult =
                DocumentStoreHolder.Store.Operations.Send(putCmpXchgOperation);
            #endregion
            
            #region Step_2
            var success = putCmpXchgResult.Successful;
            var putValue = putCmpXchgResult.Value;
            var putVersion = putCmpXchgResult.Index;
            
            if (success == false)
                result = "Key already exists";
            #endregion
            
            #region Step_3
            var getCmpXchgOperation =
                new GetCompareExchangeValueOperation<string>(cmpXchgKey);
            
            CompareExchangeValue<string> getCmpXchgResult =
                DocumentStoreHolder.Store.Operations.Send(getCmpXchgOperation);
            #endregion
            
            #region Step_4
            var key = getCmpXchgResult.Key;
            var currentValue = getCmpXchgResult.Value;
            var currentValueVersion = getCmpXchgResult.Index;
            var currentMetadata = getCmpXchgResult.Metadata;
            #endregion
            #endregion

            result = result??  $"Created a new Compare-Exchange Key: {key}, Value: {currentValue}, Value Version: {currentValueVersion}";
            return Ok(result);
        }
        
        public class RunParams
        {
            public string CmpXchgKey { get; set; }
            public string CmpXchgValue { get; set; }
        }
    }
}
