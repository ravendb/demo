using System.Collections.Generic;
using System.Threading.Tasks;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Subscriptions;
using Raven.Client.Exceptions.Documents.Subscriptions;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/subscriptions")]
        [Demo("Subscriptions", demoOrder: 230)]
        public async Task<object> Subscriptions(string subscriptionName = null)
        {
            var companies = new List<object>();
            if (subscriptionName == null)
            {
                var subscriptionCreationOptions = new SubscriptionCreationOptions
                {
                    Query = @"From Companies"
                };
                subscriptionName = DocumentStoreHolder.Store.Subscriptions.Create(subscriptionCreationOptions);
            }
            var workerOptions = new SubscriptionWorkerOptions(subscriptionName)
            {
                Strategy = SubscriptionOpeningStrategy.TakeOver,
                CloseWhenNoDocsLeft = true
            };
            using (var subscriptionWorker = DocumentStoreHolder.Store.Subscriptions.GetSubscriptionWorker(workerOptions))
            {
                try
                {
                    await subscriptionWorker.Run(batch =>
                    {
                        foreach (var item in batch.Items)
                        {
                            companies.Add(item.Result);
                        }
                    });
                }
                catch (SubscriptionClosedException)
                {
                    // That's expected
                }

                return companies;
            }
        }
    }
}
