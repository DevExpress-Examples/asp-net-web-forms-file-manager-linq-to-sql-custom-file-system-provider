Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub ASPxFileManager1_ItemDeleting(ByVal source As Object, ByVal e As DevExpress.Web.FileManagerItemDeleteEventArgs)
		ValidateSiteMode(e)
	End Sub
	Protected Sub ASPxFileManager1_ItemMoving(ByVal source As Object, ByVal e As DevExpress.Web.FileManagerItemMoveEventArgs)
		ValidateSiteMode(e)
	End Sub
	Protected Sub ASPxFileManager1_ItemRenaming(ByVal source As Object, ByVal e As DevExpress.Web.FileManagerItemRenameEventArgs)
		ValidateSiteMode(e)
	End Sub
	Protected Sub ASPxFileManager1_FolderCreating(ByVal source As Object, ByVal e As DevExpress.Web.FileManagerFolderCreateEventArgs)
		ValidateSiteMode(e)
	End Sub
	Protected Sub ASPxFileManager1_FileUploading(ByVal source As Object, ByVal e As DevExpress.Web.FileManagerFileUploadEventArgs)
		ValidateSiteMode(e)
    End Sub
    Protected Sub ASPxFileManager1_ItemCopying(ByVal source As Object, ByVal e As FileManagerItemCopyEventArgs)
        ValidateSiteMode(e)
    End Sub
	Protected Sub ValidateSiteMode(ByVal e As FileManagerActionEventArgsBase)
		e.Cancel = True
		e.ErrorText = "Data modifications are not allowed in the example."
	End Sub
End Class
