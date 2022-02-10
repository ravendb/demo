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
    public class HighlightQueryResultsCustomizedController : DemoCodeController
    {
        public HighlightQueryResultsCustomizedController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
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
                public string Title { get; set; }
                public string Notes { get; set; }
            }
            #endregion
           
            public EmployeesDetails()
            {
                #region Step_3
                Map = employees => from employee in employees
                    select new IndexEntry
                    {
                         Title = employee.Title,
                         Notes = employee.Notes[0]
                         // employee.Notes is a string array,
                         // indexing only the first element for this example
                    };
                #endregion
                
                #region Step_4
                Store(x => x.Title, FieldStorage.Yes);
                Index(x => x.Title, FieldIndexing.Search);
                TermVector(x => x.Title, FieldTermVector.WithPositionsAndOffsets);
                
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
            int fragmentLength = runParams.FragmentLength?? 100;
            int fragmentCount = runParams.FragmentCount?? 1;
            string tag1 = runParams.Tag1 ?? "+++";
            string tag2 = runParams.Tag2 ?? "+++";
            string tag3 = runParams.Tag3 ?? "<<<";
            string tag4 = runParams.Tag4 ?? ">>>";

            Highlightings titleHighlightings;
            Highlightings notesHighlightings;
            
            #region Demo
            List<Employee> employeesResults;
         
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                HighlightingOptions tagsToUse1 = new HighlightingOptions
                {
                    PreTags = new[] { tag1 },
                    PostTags = new[] { tag2 }
                };
                
                HighlightingOptions tagsToUse2 = new HighlightingOptions
                {
                    PreTags = new[] { tag3 },
                    PostTags = new[] { tag4 }
                };
                #endregion
                
                #region Step_6
                employeesResults = session.Query<EmployeesDetails.IndexEntry, EmployeesDetails>()
                    .Highlight("Title", fragmentLength, fragmentCount, tagsToUse1, out titleHighlightings)
                    .Highlight("Notes", fragmentLength, fragmentCount, tagsToUse2, out notesHighlightings)
                    .Search(x => x.Title, "manager")
                    .Search(x => x.Notes, "sales")
                    .OfType<Employee>()
                    .ToList();
                #endregion
                
                #region Step_7
                if (employeesResults.Count > 0)
                {
                    string employeeId = employeesResults[0].Id;
                    string[] titleFragments = titleHighlightings.GetFragments(employeeId);
                    string[] notesFragments = notesHighlightings.GetFragments(employeeId);
                }
                #endregion 
            }
            #endregion

            List<DataToShow> highlightResults = new List<DataToShow>();

            foreach (var employee in employeesResults)
            {
                string[] titleFragments = titleHighlightings.GetFragments(employee.Id);
                foreach (var item in titleFragments)
                {
                    DataToShow itemResults = new DataToShow
                    {
                        documentId = employee.Id,
                        indexField = titleHighlightings.FieldName,
                        fragment = item
                    };
                
                    highlightResults.Add(itemResults);
                }
                
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
            public string Tag1 { get; set; }
            public string Tag2 { get; set; }
            public string Tag3 { get; set; }
            public string Tag4 { get; set; }
        }

        private class DataToShow
        {
            public string documentId { get; set; }
            public string indexField { get; set; }
            public string fragment { get; set; }
        }
    }
}
