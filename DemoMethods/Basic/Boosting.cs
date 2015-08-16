using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object Boosting()
        {
            using (var session = Store.OpenSession())
            {
                new Index_OrderByCompanyAndShipper().Execute(Store);

                var orders = session.Query<Order,Index_OrderByCompanyAndShipper>()
                .ToList();

                return DemoUtilities.Instance.ObjectToJson(orders);
            }
        }
    }
}
