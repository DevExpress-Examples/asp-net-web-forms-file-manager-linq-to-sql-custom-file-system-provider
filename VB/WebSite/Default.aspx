<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<dx:ASPxFileManager ID="ASPxFileManager1" runat="server"
			OnFileUploading="ASPxFileManager1_FileUploading"
			OnFolderCreating="ASPxFileManager1_FolderCreating"
			OnItemDeleting="ASPxFileManager1_ItemDeleting"
			OnItemMoving="ASPxFileManager1_ItemMoving"
			OnItemRenaming="ASPxFileManager1_ItemRenaming"
			OnItemCopying="ASPxFileManager1_ItemCopying"
			CustomFileSystemProviderTypeName="LinqFileSystemProvider">
			<Settings RootFolder="a1" ThumbnailFolder="~\Thumb\" />
			<SettingsDataSource />
			<SettingsEditing AllowCreate="True" AllowDelete="True" AllowMove="True" AllowRename="True" AllowCopy="true" />
			<SettingsFileList View="Thumbnails"></SettingsFileList>
		</dx:ASPxFileManager>
	</form>
</body>
</html>