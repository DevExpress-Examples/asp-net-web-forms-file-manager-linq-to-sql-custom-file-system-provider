using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void ASPxFileManager1_ItemDeleting(object source, DevExpress.Web.FileManagerItemDeleteEventArgs e) {
        ValidateSiteMode(e);
    }
    protected void ASPxFileManager1_ItemMoving(object source, DevExpress.Web.FileManagerItemMoveEventArgs e) {
        ValidateSiteMode(e);
    }
    protected void ASPxFileManager1_ItemRenaming(object source, DevExpress.Web.FileManagerItemRenameEventArgs e) {
        ValidateSiteMode(e);
    }
    protected void ASPxFileManager1_FolderCreating(object source, DevExpress.Web.FileManagerFolderCreateEventArgs e) {
        ValidateSiteMode(e);
    }
    protected void ASPxFileManager1_FileUploading(object source, DevExpress.Web.FileManagerFileUploadEventArgs e) {
        ValidateSiteMode(e);
        
    }
    protected void ASPxFileManager1_ItemCopying(object source, FileManagerItemCopyEventArgs e) {
        ValidateSiteMode(e);
    }
    protected void ValidateSiteMode(FileManagerActionEventArgsBase e) {
        e.Cancel = true;
        e.ErrorText = "Data modifications are not allowed in the example.";
    }
}