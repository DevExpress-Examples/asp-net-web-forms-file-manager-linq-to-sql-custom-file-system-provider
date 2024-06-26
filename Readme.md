<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128554513/15.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2900)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# File Manager for ASP.NET Web Forms - How to implement a custom file system provider for LINQ to SQL data source

This example demonstrates how to implement a custom file system provider that bounds [ASPxFileManager](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxFileManager) to a LINQ to SQL data source. The provider retrieves data from data context ( a `DbFileSystemDataContext`  object) connected to a database that contains file/folder structure and contents.

To decrease the number of recursive LINQ to SQL queries, a folder list is cached in memory (see the `FolderCache` property and the `RefreshFolderCache` method). 

## Files to Review

* [LinqFileSystemProvider.cs](./CS/WebSite/App_Code/LinqFileSystemProvider.cs) (VB: [LinqFileSystemProvider.vb](./VB/WebSite/App_Code/LinqFileSystemProvider.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))

## Documentation

* [Custom File System Provider](https://docs.devexpress.com/AspNet/9907/components/file-management/file-manager/concepts/file-system-providers/custom-file-system-provider)

## More Examples

* [File Manager for ASP.NET Web Forms - How to implement a custom file system provider for List data source](https://github.com/DevExpress-Examples/asp-net-web-forms-file-manager-list-custom-file-system-provider)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-web-forms-file-manager-linq-to-sql-custom-file-system-provider&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-web-forms-file-manager-linq-to-sql-custom-file-system-provider&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
