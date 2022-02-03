using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Queries.Highlighting;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.HighlightQueryResultsBasics
{
    public class HighlightQueryResultsBasicsController : DemoCodeController
    {
        public HighlightQueryResultsBasicsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class EmployeesDetails : AbstractIndexCreationTask<Employee, EmployeesDetails.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string Notes { get; set; }
            }
            #endregion
           
            public EmployeesDetails()
            {
                #region Step_3
                Map = employees => from employee in employees
                    select new IndexEntry
                    {
                         Notes = employee.Notes[0]
                         // employee.Notes is a string array,
                         // indexing only the first element for this example
                    };
                #endregion
                
                #region Step_4
                Store(x => x.Notes, FieldStorage.Yes); 
                Index(x => x.Notes, FieldIndexing.Search);
                TermVector(x => x.Notes, FieldTermVector.WithPositionsAndOffsets);
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new EmployeesDetails().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int fragmentLength = runParams.FragmentLength?? 50;
            int fragmentCount = runParams.FragmentCount?? 2;

            Highlightings notesHighlightings;
            
            #region Demo
            List<Employee> employeesResults;
         
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                employeesResults = session.Query<EmployeesDetails.IndexEntry, EmployeesDetails>()
                    .Highlight("Notes", fragmentLength, fragmentCount, out notesHighlightings)
                    .Search(x => x.Notes, "sales")
                    .OfType<Employee>()
                    .ToList();
                #endregion
                
                #region Step_6
                string employeeId = employeesResults[0].Id;
                string[] notesFragments = notesHighlightings.GetFragments(employeeId);
                #endregion 
            }
            #endregion

            List<DataToShow> highlightResults = new List<DataToShow>();

            foreach (var employee in employeesResults)
            {
                string[] notesFragments = notesHighlightings.GetFragments(employee.Id);
                foreach (var item in notesFragments)
                {
                    DataToShow itemResults = new DataToShow
                    {
                        documentId = employee.Id,
                        indexField = notesHighlightings.FieldName,
                        fragment = item
                    };
                
                    highlightResults.Add(itemResults);
                }
            }

            return Ok(highlightResults.OrderByDescending(x => x.indexField)); 
        }
        
        public class RunParams
        {
            public int? FragmentLength { get; set; }
            public int? FragmentCount { get; set; }
        }

        private class DataToShow
        {
            public string documentId { get; set; }
            public string indexField { get; set; }
            public string fragment { get; set; }
        }
    }
}
