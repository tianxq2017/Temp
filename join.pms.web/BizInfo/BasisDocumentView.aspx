<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasisDocumentView.aspx.cs"
    Inherits="join.pms.web.BizInfo.BasisDocumentView" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�ʽ����-�ʽ���Ϣ�ɼ�</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet" />
    <link href="../css/tanchu.css" type="text/css" rel="Stylesheet" />
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
    <script src="/includes/dataGrid.js" type="text/javascript"></script>
    <script src="../Scripts/tanchu.js" type="text/javascript"></script>
    <script src="../Scripts/CommSys.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../css/index1.css"/>
    <link rel="stylesheet"  href="../css/zoom.css" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!--������Ϣ-->
    <div class="mbx">
        ��ǰλ�ã�<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
    <!--��ʾ��Ϣ-->
    <div class="tsxx">
        ��ʾ��Ϣ������һ��д������Ϣ�󣻵�����ύ����ť����</div>
    <!-- ҳ����� -->
    <input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display: none;" />
    <!--Start-->
    <div id="ec_bodyZone">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C">
            <tr>
                <td bgcolor="#F2F9EC">
                    <table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4">
                        <tr>
                            <td bgcolor="#F3FCFF" align="left">
                                <!-- �༭��  -->
                                <div class="form_bg">
                                    <div class="form_a">
                                        <p class="form_title">
                                            <b>�����ļ�</b></p>
                                        <div class="form_table">
                                            <table width="800px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr style="height: 30px;">
                                                                <th style="text-align: center">
                                                                    ��Ŀ���ݣ��ļ����ƣ���
                                                                </th>
                                                                <td colspan="3">
                                                                    <asp:Literal ID="ltr_FileName" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <th style="width: 150px">
                                                                    ʡ�ƻ��ĺţ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_Province_Symbol" runat="server"></asp:Literal>
                                                                </td>
                                                                <th style="width: 120px">
                                                                    ʡ�ʽ��ĺţ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_Province_Money_Symbol" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <th>
                                                                    �мƻ��ĺţ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_City_Symbol" runat="server"></asp:Literal>
                                                                </td>
                                                                <th>
                                                                    ���ʽ��ĺţ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_City_Money_Symbol" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <th>
                                                                    �������ڣ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_IssueDate_1" runat="server"></asp:Literal>
                                                                </td>
                                                                <th>
                                                                    �´��
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_IssueDamount_1" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <th>
                                                                    �ؼƻ��ĺţ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_County_Symbol" runat="server"></asp:Literal>
                                                                </td>
                                                                <th>
                                                                    ���ʽ��ĺţ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_County_Money_Symbol" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <th>
                                                                    �������ڣ�
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_IssueDate_2" runat="server"></asp:Literal>
                                                                </td>
                                                                <th>
                                                                    �´��
                                                                </th>
                                                                <td class="text">
                                                                    <asp:Literal ID="ltr_B_IssueDamount_2" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <th>
                                                                    ��ע��
                                                                </th>
                                                                <td class="text" colspan="3">
                                                                    <asp:Literal ID="ltr_B_Remarks" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <td colspan="4">
                                                                    ��Ȳ�����ƶ��Ŀ�ʽ��������ܱ�
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <div class="container">	 
	                                                                    <ul class="gallery" style="padding-left:20px;">
		                                                                    <asp:Literal ID="ltr_Img" runat="server"></asp:Literal>
	                                                                    </ul>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <!---------------->
                                </div>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="zhengwen">
                                    <tr>
                                        <td align="right" style="width: 100px; height: 30px;">
                                            &nbsp;
                                        </td>
                                        <td width="*" align="left" style="height: 25px">
                                            <input type="button" name="ButBackPage" value="�� ���� ��" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';"
                                                class="submit6" />
                                        </td>
                                    </tr>
                                </table>
                                <!--End-->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <div class="tsxx" id="qykInfo">
    </div>
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/zoom.min.js"></script>
</body>
</html>
