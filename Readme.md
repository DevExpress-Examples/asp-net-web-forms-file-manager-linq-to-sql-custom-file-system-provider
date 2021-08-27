<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128554513/15.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2900)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [LinqFileSystemProvider.cs](./CS/WebSite/App_Code/LinqFileSystemProvider.cs) (VB: [LinqFileSystemProvider.vb](./VB/WebSite/App_Code/LinqFileSystemProvider.vb))
* **[Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))**
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# ASPxFileManager - How to implement a LINQ to SQL based file system provider


<p>This example shows how to create a LINQ to SQL based file system provider for the <strong>ASPxFileManager</strong>. The provider retrieves data from DataContext connected to a database containing file/folder structure and contents. To improve performance, we do the following:</p><p>- Cache a folder list in memory to decrease the number of recursive LINQ to SQL queries made to a database (see the <strong>FolderCache</strong> property and the <strong>RefreshFolderCache</strong> method). </p><p>- Use delayed loading for the <strong>Data</strong> property mapped to a database field that stores file contents (the <strong>Delay Loaded</strong> property is set to True for this property in the <strong>DbFileSystemItem</strong> entity class).</p><p><strong>See </strong><strong>also:</strong><strong><br />
</strong><a href="https://www.devexpress.com/Support/Center/p/E5024">E5024: ASPxFileManager - How to implement a List data bound custom file system provider</a></p>

<br/>


