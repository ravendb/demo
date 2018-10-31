// import { ParametersProps } from "./demoDisplay/Parameters";
import { CodeProps } from "./demoDisplay/Code";
import React = require("react");
import { SidebarDisplay } from "./sidebar";
import { AssetsItem } from "./sidebar/AssetLinks";

// export function MockupDemoBody() {
//     const paramProps: ParametersProps = {
//         items: [
//             { type: "text", name: "searchQuery", placeholder: "John" },
//             { type: "date", name: "searchQuery", placeholder: "2011-09-29" },
//             { type: "number", datatype: "integer", name: "searchQuery", placeholder: "10" },
//             { type: "number", datatype: "float", name: "searchQuery", placeholder: "10.42792" }
//         ]
//     };

//     const codeProps: CodeProps = {
//         language: "csharp",
//         sourceCode,
//         usingsLastLine: 4
//     };

//     const resultProps: ResultDisplayProps = {
//         clientExecTime: "0.06 seconds",
//         serverExecTime: "< 0.01 seconds"
//     };

//     return <DemoBodyDisplay code={codeProps} parameters={paramProps} results={resultProps} />;
// }

export function MockSidebar() {
    const description = <>
        <p>
            Lorem ipsum dolor sit amet, <strong>consectetuer adipiscing</strong> elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.
            </p>
        <p>
            Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.
            </p>
    </>;

    const walkthroughLinks = [
        { href: "#", title: "Lorem ipsum dolor sit amet" },
        { href: "#", title: "Cons ectetuer adipiscing elit" },
        { href: "#", title: "Sed diam nonummy nibh euismod tincidunt" },
        { href: "#", title: "Ut laoreet dolore magna aliquam erat volutpat" },
        { href: "#", title: "Ut wisi enim ad minim veniam, quis nostrud exerci tation" },
        { href: "#", title: "Ullamcorper suscipit lobortis nisl ut aliquip ex ea" }
    ];

    const assetLinks: AssetsItem[] = [
        { type: "Link", href: "#", title: "Lorem ipsum dolor sit amet" },
        { type: "Downloadable", href: "#", title: "consectetuer adipiscing elit" },
        { type: "Document", href: "#", title: "sed diam nonummy nibh euismod tincidunt" },
        { type: "Link", href: "#", title: "ut laoreet dolore magna" },
        { type: "Link", href: "#", title: "aliquam erat volutpat" },
        { type: "Link", href: "#", title: "augue duis dolore" }
    ];

    return <SidebarDisplay
        selectedLanguage="csharp"
        title="Lorem ipsum dolor sit amet"
        description={description}
        walkthroughLinks={walkthroughLinks}
        assetLinks={assetLinks}
    />;
}

export const sourceCode = `using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Queries.Facets;
namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        public static List<FacetBase> FixedFacet { get; set; }

        [HttpGet]
        [Route("/menu/CreateIndexes")]
        [Demo("Deploy Indexes", DemoOutputType.String, demoOrder: 310)]
        public object CreateIndexes()
        {
            // Side By Side Index Creation:
            // IndexCreation.SideBySideCreateIndexes(GetType().GetTypeInfo().Assembly, DocumentStoreHolder.Store);

            try
            {
                IndexCreation.CreateIndexes(GetType().GetTypeInfo().Assembly, DocumentStoreHolder.Store);
                CreateFixedFacet();

                DocumentStoreHolder.MediaStore.ExecuteIndex(new LastFmAnalyzed());
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "Indexes and Facets were created successfully";
        }

        private static void CreateFixedFacet()
        {
            const decimal fromVal = 10;
            const decimal toVal = 20;

            FixedFacet = FacetRangeCreation.CreateFacets(fromVal, toVal);

            var facets = FacetRangeCreation.CreateFacets();
            var rangeFacets = FacetRangeCreation.CreateRangeFacets(fromVal, toVal);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new FacetSetup
                {
                    Id = "facets/ProductFacet",
                    Facets = facets,
                    RangeFacets = rangeFacets
                });

                session.SaveChanges();
            }
        }
    }
}`;