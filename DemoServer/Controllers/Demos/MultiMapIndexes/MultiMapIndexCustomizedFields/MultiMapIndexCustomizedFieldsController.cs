using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.MultiMapIndexes.MultiMapIndexCustomizedFields
{
    public class MultiMapIndexCustomizedFieldsController : DemoCodeController
    {
        public MultiMapIndexCustomizedFieldsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Contacts_ByNameAndTitle : AbstractMultiMapIndexCreationTask<Contacts_ByNameAndTitle.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string ContactName { get; set; }
                public string ContactTitle { get; set; }
                public object Collection { get; set; }
            }
            #endregion
            
            #region Step_3
            public class ProjectedEntry : IndexEntry
            {
                public string Phone { get; set; }
            }
            #endregion
            
           
            public Contacts_ByNameAndTitle()
            {
                #region Step_4
                AddMap<Employee>(employees => from employee in employees
                    select new IndexEntry
                    {
                        ContactName = employee.FirstName + " " + employee.LastName,
                        ContactTitle = employee.Title,
                        Collection = MetadataFor(employee)["@collection"]
                        
                    });
                
                AddMap<Company>(companies => from company in companies
                    select new IndexEntry
                    {
                        ContactName = company.Contact.Name,
                        ContactTitle = company.Contact.Title,
                        Collection = MetadataFor(company)["@collection"]
                    });
                
                AddMap<Supplier>(suppliers => from supplier in suppliers
                    select new IndexEntry
                    {
                        ContactName = supplier.Contact.Name,
                        ContactTitle = supplier.Contact.Title,
                        Collection = MetadataFor(supplier)["@collection"]
                    });
                #endregion
                
                #region Step_5
                Store(x => x.ContactName, FieldStorage.Yes);
                Store(x => x.ContactTitle, FieldStorage.Yes);
                Store(x => x.Collection, FieldStorage.Yes);
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Contacts_ByNameAndTitle().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string namePrefix = runParams.NamePrefix?? "Michael";
            string titlePrefix = runParams.TitlePrefix?? "Sales";

            #region Demo
            List<Contacts_ByNameAndTitle.ProjectedEntry> contacts;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_6
                contacts = session.Query<Contacts_ByNameAndTitle.IndexEntry, Contacts_ByNameAndTitle>()
                    .Where(contact => contact.ContactName.StartsWith(namePrefix) &&
                                  contact.ContactTitle.StartsWith(titlePrefix))
                    .ProjectInto<Contacts_ByNameAndTitle.ProjectedEntry>()
                    .ToList();

                #endregion
            }
            #endregion
            
           return Ok(contacts);
        }
        
        public class RunParams
        {
            public string NamePrefix { get; set; }
            public string TitlePrefix { get; set; }
        }
    }
}
