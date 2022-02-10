## Setup
In the `DemoServer` folder:
```
npm install
npm run webpack
```

Node version needed: v14+  
ASP.NET core: 3.1.x

## Running the application
Run the `DemoServer` project in Visual Studio, Rider or `dotnet run`.  
The application should run on http://localhost:9090/  
In order for the demos to execute, a RavenDB instance must be running on http://localhost:8080

## General description
The demo application contains source code examples that will be displayed as demo pages. The demo pages are divided into categories.  
The majority of the demos can be executed on the server side, which will result in modifying user database.  
User can go to their database using the "Open in studio" button, in order to examine the changes introduced by running a demo.

Each demo contains description, assets (links to an external or internal resource) and walkthrough steps.  
Each walkthrough step guides the user through the RavenDB knowledge by highlighting and describing relevant code, as well as providing a set of resources.

## User database
Each user has their own database. On production, it resides on the live-test server.  
The default database contains Northwind data. Some of the demos require special Media database, which will be created automatically when needed.

## Source code
C# is the main demo application language.  
The code that is displayed on each C# demo page will be also executed when the "Run" button is clicked.

The additional language demos contain their own descriptions, assets and walkthroughs.  
However, they should be designed to present the semantically identical code as their C# counterpart.  
When the "Run" button is clicked on a multi-language demo, the application will execute the C# source code.

## demos.json
The `demos.json` file contains a list of categories and the corresponding demos.  
Each demo language folder should contain its own `demos.json` file.

`demos.json` locations:
- C# `DemoServer/Controllers/Demos/demos.json`
- GO `DemoServer/AdditionalLanguages/go/demos.json`
- Java `DemoServer/AdditionalLanguages/java/src/main/java/net/ravendb/demo/demos.json`
- Node.js `DemoServer/AdditionalLanguages/nodejs/demo/demos.json`

The JSON structure of the `demos.json` file is an array of such entries:
```
    {
        "category": {
            "slug": "basics",
            "directory": "Basics"
        },
        "demos": [
            "theDocumentStore",
            "theSession",
            "createDocument",
            "editDocument",
            "deleteDocument"
        ]
    }
```

### category item
Each category should contain `slug` and `directory` fields.

`slug` should identify the category and should be identical for all languages (i.e. the `basics` slug is the same for C#, GO, Java & Node.js).

The `directory` field indicates the directory name of the category. Please remember that it's case-sensitive on Linux.

### demos array
The `demos` array contains a list of the demo **folders** located inside the category folder. The values are also case-sensitive on Linux.

**Important:** in order for a demo to be visible in the application, it's **directory name** needs to be added to the proper category in the `demos.json` file.

## Demo folder
Each demo resides in its own folder which name should correspond with the `demos.json` entry.  
Each demo directory should contain `metadata.json` file and a source code file.

### `metadata.json`
`metadata.json` contains the demo settings, wordings, etc.

**Important:** `metadata.json` should be added to the project as Content file.

Example content:

```
    "Slug": "create-document",
    "SourceFileName": "CreateDocument.java",
    "Title": "Create Document",
    "DescriptionLines": [
        "**Create a new JAVA entity and save it as a document** in the Database.",
        ...
    ],
	"Assets": [ ... ],
	"Walkthroughs: [ ... ]
```

- `Slug` may be treated as a human-readable identifier of the demo. It is a part of the demo URL and should be the same for all language versions.
- `SourceFileName` points to the file that contains the demo source code. It is case-sensitive.
- `Title` contains the displayed demo title.
- `DescriptionLines` contains the demo description.
- `Assets` contains the general demo assets listed in the left-hand-side menu.
- `Walkthroughs` contains the walkthrough step definitions.

### Optional `metadata.json` fields
- `NonInteractive` - the demo is not interactive. The "Run" and "Open in studio" buttons won't be visible.
- `StudioLinkToMediaDatabase` - the "Open in studio" button will lead to the media database instead of the regular DemoUser database.
- `StudioLinkToIndexList` - the "Open in studio" button will lead to the index list page.
- `ConferenceOnly` - the demo will be available only in the conference mode.

### Assets
Assets are displayed as a webpage link. They can be placed in the general demo section (under demo description etc.) or in the walkthrough step panel.

Example definition:

```
{
	"Title": "Document Modeling",
	"Type" : "link",
	"Url"  : "https://github.com/ravendb/book/blob/v4.0/Ch03/Ch03.md#document-modeling"
}
```
Available asset types: `link`, `downloadable`, `document`, `demo`

### Walkthroughs
The `Walkthroughs` element is an array of walkthrough steps. Example walkthrough step:

```
{
	"Title": "Create a new JAVA object to be saved",
	"Slug" : "step-1",
	"DescriptionLines": [
		"* Define the JAVA object to be saved.",
		"",
		"TODO"
	],
	"Assets": [ ...	]
}
```

- `Slug` is the identifier of the walkthrough step in the context of this demo. It will be part of the walkthrough step URL.
- `Assets` contains the same type of data as the demo general assets.

## Source code
The demo source code file should contain additional region sections in order to make the demo parser know which code parts should be displayed on the demo page.  
The region begin\end syntax is language-dependent. For example, in C# it should be as below:

```
#region Usings
using Raven.Client.Documents.Session;
#endregion
```

```
#region Demo 
#region Step_1
Supplier supplier = new Supplier
{
    Name = supplierName,
    Phone = supplierPhone
};

Category category = new Category
{
    Name = "NoSQL Databases",
    Description = "Non-relational databases"
};
#endregion
#endregion
```

Supported region names:
- `Usings` - there should be only one such a region.
- `Demo` - there can be multiple such regions.
- `Step_N` (for example: `Step_1`, `Step_2`, etc.). The `Step` region should be nested inside a `Demo` region.

### C# Source code
The C# demo source code is displayed on the demo pages, but it's also executed when the "Run" button is clicked.  
A demo can be marked as `NonInteractive` in `metadata.json` to ensure that the code won't be executed and will be only treated as a read-only code example.

### Non-C# source code
The source code that is not written in C# **won't be compiled**. Only the C# demos are compiled and executed in the runtime of the application.

For example, a Java source code can be found here:
`DemoServer/AdditionalLanguages/java/src/main/java/net/ravendb/demo/basics/createDocument/CreateDocument.java`

The Java source code is divided into regions that start with `//region REGION_NAME` line and end with `//endregion` line, for example:

```
//region Usings
import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
//endregion
```

## Troubleshooting
All directory and file names are case sensitive on Linux, but may be case-insensitive on the developer machine (if the application is being developed on windows).  
If a demo is missing on production but was working on the developer's machine, please make sure that all paths and file names (including the values provided in `demos.json`) match the correct casing.

A log file is created in folder: `DemoServer\bin\Debug\netcoreapp2.1`

Checklist:

1. `demos.json`:
    - `category` item: ensure that `slug` is correct and the `directory` value matches the existing folder name and the casing is correct
    - `demos` array: ensure that the demo name matches the directory name inside a given category
2. `metadata.json`
    - ensure it is added as a content file to the `DemoServer` project
    - for non-C# demos: make sure that the `Slug` value matches the corresponding C# demo slug
    - ensure that `SourceFileName` matches the source code file name located in the same folder as `metadata.json`. Please remember to include the file extension.

## When adding a new C# demo:

1. Add demo to `demos.json`
2. Add slug to `slugs.ts`
3. Add the demo `Controller.cs` file
4. Add the demo `metadata.json` file
5. Add the demo `*.svg` file under `wwwroot\img\demo-icons\`
6. Add the demo `*.tsx` file under `client\src\components\demos\`
7. Add the demo to file: `DemoFactory.tsx`
