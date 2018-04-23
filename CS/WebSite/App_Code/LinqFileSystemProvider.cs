using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web;
using System.IO;

public class LinqFileSystemProvider : FileSystemProviderBase {
    const int DbRootItemId = 1;
    DbFileSystemDataContext dataContext;
    Dictionary<int, DbFileSystemItem> folderCache;
    string rootFolderDisplayName;

    public LinqFileSystemProvider(string rootFolder)
        : base(rootFolder) {
        this.dataContext = new DbFileSystemDataContext();
        RefreshFolderCache();
    }

    public DbFileSystemDataContext DataContext { get { return dataContext; } }

    // Used to decrease the number of recursive LINQ to SQL queries made to a database
    public Dictionary<int, DbFileSystemItem> FolderCache { get { return folderCache; } }


    public override string RootFolderDisplayName { get { return rootFolderDisplayName; } }

    public override IEnumerable<FileManagerFile> GetFiles(FileManagerFolder folder) {
        DbFileSystemItem dbFolderItem = FindDbFolderItem(folder);
        return
            from dbItem in DataContext.DbFileSystemItems
            where !dbItem.IsFolder && dbItem.ParentId == dbFolderItem.Id
            select new FileManagerFile(this, folder, dbItem.Name, dbItem.Id.ToString());
    }
    public override IEnumerable<FileManagerFolder> GetFolders(FileManagerFolder parentFolder) {
        DbFileSystemItem dbFolderItem = FindDbFolderItem(parentFolder);
        return
            from dbItem in FolderCache.Values
            where dbItem.IsFolder && dbItem.ParentId == dbFolderItem.Id
            select new FileManagerFolder(this, parentFolder, dbItem.Name, dbItem.Id.ToString());
    }

    public override bool Exists(FileManagerFile file) {
        return FindDbFileItem(file) != null;
    }
    public override bool Exists(FileManagerFolder folder) {
        return FindDbFolderItem(folder) != null;
    }
    public override System.IO.Stream ReadFile(FileManagerFile file) {
        return new MemoryStream(FindDbFileItem(file).Data.ToArray());
    }
    public override DateTime GetLastWriteTime(FileManagerFile file) {
        var dbFileItem = FindDbFileItem(file);
        return dbFileItem.LastWriteTime.GetValueOrDefault(DateTime.Now);
    }

    // File/folder management operations
    public override void CreateFolder(FileManagerFolder parent, string name) {
        UpdateAndSubmitChanges(parent,
            dbItem => DataContext.DbFileSystemItems.InsertOnSubmit(new DbFileSystemItem() {
                IsFolder = true,
                LastWriteTime = DateTime.Now,
                Name = name,
                ParentId = dbItem.Id
            }));
    }
    public override void DeleteFile(FileManagerFile file) {
        UpdateAndSubmitChanges(file, dbItem => DataContext.DbFileSystemItems.DeleteOnSubmit(dbItem));
    }
    public override void DeleteFolder(FileManagerFolder folder) {
        UpdateAndSubmitChanges(folder, dbItem => DataContext.DbFileSystemItems.DeleteOnSubmit(dbItem));
    }
    public override void MoveFile(FileManagerFile file, FileManagerFolder newParentFolder) {
        UpdateAndSubmitChanges(file, dbItem => dbItem.ParentId = FindDbFolderItem(newParentFolder).Id);
    }
    public override void MoveFolder(FileManagerFolder folder, FileManagerFolder newParentFolder) {
        UpdateAndSubmitChanges(folder, dbItem => dbItem.ParentId = FindDbFolderItem(newParentFolder).Id);
    }
    public override void RenameFile(FileManagerFile file, string name) {
        UpdateAndSubmitChanges(file, dbItem => dbItem.Name = name);
    }
    public override void RenameFolder(FileManagerFolder folder, string name) {
        UpdateAndSubmitChanges(folder, dbItem => dbItem.Name = name);
    }
    public override void UploadFile(FileManagerFolder folder, string fileName, Stream content) {
        UpdateAndSubmitChanges(folder,
            dbItem => DataContext.DbFileSystemItems.InsertOnSubmit(new DbFileSystemItem() {
                Name = fileName,
                Data = ReadAllBytes(content),
                ParentId = dbItem.Id,
                LastWriteTime = DateTime.Now,
                IsFolder = false
            }));
    }
    public override void CopyFile(FileManagerFile file, FileManagerFolder newParentFolder) {
        var dbFileItem = FindDbFileItem(file);
        CopyCore(dbFileItem, newParentFolder.RelativeName, false);
    }
    public override void CopyFolder(FileManagerFolder folder, FileManagerFolder newParentFolder) {
        List<FileManagerFolder> folders = new List<FileManagerFolder>();
        FillSubFoldersList(folder, folders);
        int folderNameOffset = !string.IsNullOrEmpty(folder.Parent.RelativeName) ? folder.Parent.RelativeName.Length + 1 : 0;

        foreach (FileManagerFolder copyingFolder in folders) {
            DbFileSystemItem dbFolderItem = FindDbFolderItem(copyingFolder);
            string folderPath = newParentFolder.RelativeName;
            if (copyingFolder != folder)
                folderPath = Path.Combine(folderPath, copyingFolder.Parent.RelativeName.Substring(folderNameOffset));
            CopyCore(dbFolderItem, folderPath, true);
            foreach (FileManagerFile currentFile in copyingFolder.GetFiles()) {
                DbFileSystemItem dbFileItem = FindDbFileItem(currentFile);
                CopyCore(dbFileItem, Path.Combine(folderPath, copyingFolder.Name), false);
            }
        }
    }
    void FillSubFoldersList(FileManagerFolder folder, List<FileManagerFolder> list) {
        list.Add(folder);
        foreach (FileManagerFolder subFolder in folder.GetFolders())
            FillSubFoldersList(subFolder, list);
    }
    void CopyCore(DbFileSystemItem item, string path, bool isFolder) {
        FileManagerFolder newParentFolder = new FileManagerFolder(this, path);
        UpdateAndSubmitChanges(newParentFolder,
            dbItem => DataContext.DbFileSystemItems.InsertOnSubmit(new DbFileSystemItem() {
                Name = item.Name,
                Data = item.Data,
                ParentId = dbItem.Id,
                LastWriteTime = DateTime.Now,
                IsFolder = isFolder
            }));
    }
    protected void UpdateAndSubmitChanges(FileManagerFile file, Action<DbFileSystemItem> update) {
        UpdateAndSubmitChangesCore(FindDbFileItem(file), update);
    }
    protected void UpdateAndSubmitChanges(FileManagerFolder folder, Action<DbFileSystemItem> update) {
        UpdateAndSubmitChangesCore(FindDbFolderItem(folder), update);
    }
    protected void UpdateAndSubmitChangesCore(DbFileSystemItem dbitem, Action<DbFileSystemItem> update) {
        update(dbitem);
        DataContext.SubmitChanges();
        RefreshFolderCache();
    }
    protected DbFileSystemItem FindDbFileItem(FileManagerFile file) {
        DbFileSystemItem dbFolderItem = FindDbFolderItem(file.Folder);
        if (dbFolderItem == null)
            return null;
        return
            (from dbItem in DataContext.DbFileSystemItems
             where dbItem.ParentId == dbFolderItem.Id && !dbItem.IsFolder && dbItem.Name == file.Name
             select dbItem).FirstOrDefault();
    }
    protected DbFileSystemItem FindDbFolderItem(FileManagerFolder folder) {
        return
            (from dbItem in FolderCache.Values
             where dbItem.IsFolder && GetRelativeName(dbItem) == folder.RelativeName
             select dbItem).FirstOrDefault();
    }

    // Returns the path to a specified folder relative to a root folder
    protected string GetRelativeName(DbFileSystemItem dbFolder) {
        if (dbFolder.Id == DbRootItemId) return string.Empty;
        if (dbFolder.ParentId == DbRootItemId) return dbFolder.Name;
        if (!FolderCache.ContainsKey(dbFolder.ParentId)) return null;
        string name = GetRelativeName(FolderCache[dbFolder.ParentId]);
        return name == null ? null : Path.Combine(name, dbFolder.Name);
    }

    // Caches folder names in memory and obtains a root folder's name
    protected void RefreshFolderCache() {
        this.folderCache = (
            from dbItem in DataContext.DbFileSystemItems
            where dbItem.IsFolder
            select dbItem
        ).ToDictionary(i => i.Id);

        this.rootFolderDisplayName = (
            from dbItem in FolderCache.Values
            where dbItem.Id == DbRootItemId
            select dbItem.Name).First();
    }
    protected static byte[] ReadAllBytes(Stream stream) {
        byte[] buffer = new byte[16 * 1024];
        int readCount;
        using (MemoryStream ms = new MemoryStream()) {
            while ((readCount = stream.Read(buffer, 0, buffer.Length)) > 0) {
                ms.Write(buffer, 0, readCount);
            }
            return ms.ToArray();
        }
    }

    // Get file size (length).
    public override long GetLength(FileManagerFile file) {
        return FindDbFileItem(file).Data.Length;
    }
}