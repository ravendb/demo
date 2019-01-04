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

namespace DemoServer.Controllers.Demos.Queries.Query2
{
    public class Query2Controller : DemoCodeController
    {
        public Query2Controller(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        public class RunParams
        {
            public string FirstName { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var firstName = runParams.FirstName;

            #region Demo
            Employee result;

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                QueryStatistics stats;

                var query =
                    from employee in session.Query<Employee>().Statistics(out stats)
                    where employee.FirstName == firstName
                    select employee;

                result = query.FirstOrDefault();
            }
            #endregion 
            
            return Ok(result);
        }
    }
}
