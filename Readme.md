# ASPxFileManager - How to implement a LINQ to SQL based file system provider


<p>This example shows how to create a LINQ to SQL based file system provider for the <strong>ASPxFileManager</strong>. The provider retrieves data from DataContext connected to a database containing file/folder structure and contents. To improve performance, we do the following:</p><p>- Cache a folder list in memory to decrease the number of recursive LINQ to SQL queries made to a database (see the <strong>FolderCache</strong> property and the <strong>RefreshFolderCache</strong> method). </p><p>- Use delayed loading for the <strong>Data</strong> property mapped to a database field that stores file contents (the <strong>Delay Loaded</strong> property is set to True for this property in the <strong>DbFileSystemItem</strong> entity class).</p><p><strong>See </strong><strong>also:</strong><strong><br />
</strong><a href="https://www.devexpress.com/Support/Center/p/E5024">E5024: ASPxFileManager - How to implement a List data bound custom file system provider</a></p>

<br/>


