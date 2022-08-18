using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.StaticIndexes.AdditionalSourcesIndex
{
    public class AdditionalSourcesIndexController : DemoCodeController
    {
        public AdditionalSourcesIndexController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Products_ByPrice : AbstractIndexCreationTask<Product, Products_ByPrice.IndexEntry>
            #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string ProductName { get; set; }
                public decimal OriginalPrice { get; set; }
                public decimal SalePrice { get; set; }
                public decimal ProfitPrice { get; set; }
            }
            #endregion

            public Products_ByPrice()
            {
                #region Step_3
                Map = products => from product in products
                    select new IndexEntry
                    {
                        ProductName = product.Name,
                        OriginalPrice = product.PricePerUnit,
                        
                        SalePrice = DiscountUtils.CalcSalePrice(product.PricePerUnit),
                        // Code behind:
                        // public static decimal CalcSalePrice(decimal price)
                        // {
                        //    return price - price / 100M * 25M;
                        //  }
                        ProfitPrice = DiscountUtils.CalcProfitPrice(product.PricePerUnit)
                        // Code behind:
                        // public static decimal CalcProfitPrice(decimal price)
                        // {
                        //    return price + price / 100M * 25M;
                        //  }
                    };
                #endregion
                
                #region Step_4
                AdditionalSources = new Dictionary<string, string>
                {
                    {
                        "DiscountLogic", System.IO.File.ReadAllText(
                            Path.Combine(AppContext.BaseDirectory, "MyAdditionalSourceFile.cs"))
                    }
                };
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Products_ByPrice().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int price = runParams.Price?? 5;

            #region Demo
            List<Product> lowCostProducts;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                lowCostProducts = session.Query<Products_ByPrice.IndexEntry, Products_ByPrice>()
                       .Where(product => product.SalePrice < price)
                       .OrderBy(x => x.SalePrice)
                       .OfType<Product>()
                       .ToList();
                #endregion
            }
            #endregion
            
            // Manipulate results to show because index fields are Not stored..
            List<DataToShow> productsData = new List<DataToShow>();
            foreach (var item in lowCostProducts)
            {
                productsData.Add(new DataToShow()
                {
                    ProductName = item.Name,
                    OriginalPrice = item.PricePerUnit,
                    SalesPrice = item.PricePerUnit - item.PricePerUnit / 100 * 25,
                    ProfitPrice = item.PricePerUnit + item.PricePerUnit / 100 * 25
                });
            }

            return Ok(productsData);
        }
        
        public class RunParams
        {
            public int? Price { get; set; }
        }

        private class DataToShow
        {
            public string ProductName { get; set; }
            public decimal OriginalPrice { get; set; }
            public decimal SalesPrice { get; set; }
            public decimal ProfitPrice { get; set; }
        }
    }
}
