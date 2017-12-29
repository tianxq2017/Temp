<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadDisk.aspx.cs" Inherits="join.pms.web.Disk.uploadDisk" %>

<%@ Register Src="../UC/Uc_UpLoadFile.ascx" TagName="Uc_UpLoadFile" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文件上传</title>
    <base target="_self">
<link href="/css/index.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <table style="width: 581px; height: 43px">
            <tr>
                <td align="right" style="height: 12px">
                    上传位置：</td>
                <td style="height: 12px; width: 504px;">
                    <asp:Literal ID="dirList" runat="server" ></asp:Literal>
                    <font color="red">*根目录下不能上传文件</font></td>
            </tr>
            <tr>
                 <td align="right" style="height: 17px">
                    浏览文件：</td>
                <td style="height: 17px; width: 504px;">
                       <input type="file" size="40" name="UploadFiles" id="UploadFiles" contenteditable="false" />
                    <br />
	<asp:Literal ID="msg" runat="server"  Visible="False"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="height: 11px">
                </td>
                <td align="left" style="height: 11px; width: 504px;">
                    <asp:Button id="ButUploadFile" runat="server" Text=" 上传 " OnClick="ButUploadFile_Click" OnLoad="ButUploadFile_Load" Width="109px"></asp:Button></td>
            </tr>
        </table>
    
    </form>
</body>
</html>
