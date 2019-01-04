using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Controllers.Demos.Attachments.StoreAttachment;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

#region Usings
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.Query1
{
    public class Query1Controller : DemoCodeController
    {
        public Query1Controller(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        public class RunParams
        {
            public string Country { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var country = runParams.Country;

            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                QueryStatistics stats;

                var query =
                    from employee in session.Query<Employee>().Statistics(out stats)
                    where employee.Address.Country == country
                    select employee;

                // TODO @tomtom
                // - add link to indexes list for conference to show indexes merging

                var result = query.FirstOrDefault();
            }
            
            #endregion 
            
            //TODO: How to show results ?
            return Ok("The documents in the Company collection are: ...  TODO: Show Query Results ..."); 
        }
    }
}
