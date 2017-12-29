<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundingPayInfo.aspx.cs"
    Inherits="join.pms.web.BizInfo.FundingPayInfo" %>

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
                                            <b>��Ŀ����</b></p>
                                        <div class="form_table">
                                            <table width="800px" border="0" cellspacing="0" cellpadding="0">
                                                <tr style="height:30px">
                                                    <th style="width: 100px">
                                                        <span class="xing">*</span>�ļ����ƣ�
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_Name" runat="server"></asp:Literal>
                                                        <asp:HiddenField ID="hd_P_ID" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr style="height:30px">
                                                    <th style="width: 100px">
                                                        <span class="xing">*</span>֧��ʱ�䣺
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_F_PayDateTime" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height:30px">
                                                    <th>
                                                        <span class="xing">*</span>֧����
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_F_PayAmount" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table width="0" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <th style="width: 98px">
                                                                    ʹ�õ�λ��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_1" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th style="width: 98px">
                                                                    �����ϸ��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_1" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="width: 30px; padding-left: 10px">
                                                                    <a href="javascript:show_tr(1)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_1" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ1��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_2" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ1��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_2" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(2)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_2" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ2��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_3" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ2��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_3" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(3)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_3" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ3��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_4" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ3��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_4" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(4)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_4" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ4��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_5" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ4��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_5" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(5)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_5" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ5��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_6" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ5��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_6" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(7)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_6" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ6��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_7" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ6��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_7" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(7)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_7" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ7��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_8" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ7��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_8" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(8)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_8" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ8��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_9" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ8��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_9" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(9)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_9" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ9��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_10" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ9��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_10" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(10)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_10" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ10��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_11" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ10��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_11" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(11)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_11" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ11��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_12" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ11��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_12" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(12)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_12" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ12��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_13" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ12��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_13" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(13)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_13" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ13��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_14" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ13��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_14" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(14)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_14" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ14��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_15" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ14��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_15" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(15)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_15" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ15��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_16" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ15��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_16" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(16)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_16" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ16��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_17" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ16��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_17" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(17)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_17" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ17��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_18" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ17��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_18" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(18)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_18" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ18��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_19" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ18��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_19" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                    <a href="javascript:show_tr(19)">
                                                                        <img src="../images/add.gif" alt="" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr_19" style="display: none">
                                                                <th>
                                                                    ʹ�õ�λ19��
                                                                </th>
                                                                <td class="select">
                                                                    <p>
                                                                        <span>
                                                                            <asp:DropDownList ID="dr_UseUnit_20" runat="server">
                                                                            </asp:DropDownList>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    �����ϸ19��
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_MoneyInfo_20" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <td style="padding-left: 10px">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <!---------------->
                                    <div class="form_a">
                                        <p class="form_title">
                                            <b>֧���ƻ�</b></p>
                                        <div class="form_table">
                                            <table width="900px" border="0" cellspacing="0" cellpadding="0">
                                                <tr style="height:30px">
                                                    <th style="width: 100px">
                                                        <span class="xing">*</span>֧���ƻ���
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_PaymentPlan" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        �������ѣ�
                                                    </th>
                                                    <td class="select" colspan="4">
                                                        <p>
                                                            <span>
                                                                <asp:CheckBox ID="ck_SetReminder" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="width: 80px">
                                                        ʱ�䣺
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        ʹ�õ�λ��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 70px">
                                                        ��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(1)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_1" style="display: none">
                                                    <th>
                                                        ʱ��1��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_1" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_1'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ1��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_1" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���1��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_1" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(2)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_2" style="display: none">
                                                    <th>
                                                        ʱ��2��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_2" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_2'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ2��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_2" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���2��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_2" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(3)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_3" style="display: none">
                                                    <th>
                                                        ʱ��3��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_3" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_3'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ3��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_3" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���3��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_3" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(4)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_4" style="display: none">
                                                    <th>
                                                        ʱ��4��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_4" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_4'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ4��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_4" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���4��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_4" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(5)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_5" style="display: none">
                                                    <th>
                                                        ʱ��5��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_5" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_5'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ5��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_5" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���5��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_5" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(6)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_6" style="display: none">
                                                    <th>
                                                        ʱ��6��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_6" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_6'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ6��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_6" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���6��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_6" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(7)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_7" style="display: none">
                                                    <th>
                                                        ʱ��7��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_7" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_7'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ7��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_7" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���7��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_7" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(8)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_8" style="display: none">
                                                    <th>
                                                        ʱ��8��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_8" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_8'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ8��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_8" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���8��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_8" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(9)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_9" style="display: none">
                                                    <th>
                                                        ʱ��9��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_9" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_9'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ9��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_9" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���9��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_9" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(10)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_10" style="display: none">
                                                    <th>
                                                        ʱ��10��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_10" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_10'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ10��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_10" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���10��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_10" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(11)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_11" style="display: none">
                                                    <th>
                                                        ʱ��11��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_11" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_11'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ11��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_11" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���11��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_11" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(12)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_12" style="display: none">
                                                    <th>
                                                        ʱ��12��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_12" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_12'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ12��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_12" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���12��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_12" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(13)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_13" style="display: none">
                                                    <th>
                                                        ʱ��13��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_13" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_13'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ13��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_13" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���13��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_13" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(14)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_14" style="display: none">
                                                    <th>
                                                        ʱ��14��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_14" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_14'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ14��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_14" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���14��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_14" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(15)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_15" style="display: none">
                                                    <th>
                                                        ʱ��15��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_15" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_15'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ15��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_15" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���15��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_15" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(16)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_16" style="display: none">
                                                    <th>
                                                        ʱ��16��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_16" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_16'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ16��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_16" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���16��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_16" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(17)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_17" style="display: none">
                                                    <th>
                                                        ʱ��17��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_17" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_17'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ17��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_17" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���17��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_17" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(18)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_18" style="display: none">
                                                    <th>
                                                        ʱ��18��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_18" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_18'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ18��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_18" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���18��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_18" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                        <a href="javascript:show_tr_2(1)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2_19" style="display: none">
                                                    <th>
                                                        ʱ��19��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_19" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_19'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ʹ�õ�λ19��
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_UnitOfUse_19" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ���19��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_Money_19" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 5px">
                                                    </td>
                                                </tr>
                                                <tr style="height: 60px">
                                                    <th>
                                                        �����ļ�
                                                    </th>
                                                    <td colspan="6" style="text-align: left; padding-left: 20px">
                                                        <!-- ������ָ��� Start -->
                                                        <input id="txtDocsID" name="txtDocsID" type="hidden" runat="server" />
                                                        <input id="txtDocsName" name="txtDocsName" class="cuserinput" check="1" show="��������"
                                                            size="50" style="width: 360px; background-color: #fafafa" />
                                                        <input type="button" class="submit6" value="ѡ��.." onclick="SelectCmsDocs('txtDocsID','txtDocsName')"
                                                            class="cusersubmit" />
                                                        <!-- ������ָ��� End -->
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <%--<div style="padding-left:25px">
                                            <table width="900px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                   <td><a onclick="Ceng()"><img width="300px" height="180px" style="margin:6px 7px" src="../images/bg-banner.jpg" /></a></td>
                                                   <td><a onclick="Ceng()"><img width="300px" height="180px" style="margin:6px 7px" src="../images/bg-banner.jpg" /></a></td>
                                                   <td><a onclick="Ceng()"><img width="300px" height="180px" style="margin:6px 7px" src="../images/bg-banner.jpg" /></a></td>
                                                </tr>
                                            </table>
                                        </div>--%>
                                    </div>
                                </div>
                                <%--������--%>
                                <%--<div id="ceng">
                                </div>
                                <div id="close">
                                    <a style="position: relative;left:-3px;COLOR:RED;text-decoration: none;" onclick="closeCeng()"><img src="../images/close.png"/ alt=""></a>
                                    <div style="text-align:center;">
                                        <img width="90%" height="450px" style="margin:6px 7px" src="../images/bg-banner.jpg" />
                                    </div>
                                </div>--%>
                                <!-- ������ť m_TargetUrl -->
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="zhengwen">
                                    <tr>
                                        <td align="right" style="width: 100px; height: 30px;">
                                            &nbsp;
                                        </td>
                                        <td width="*" align="left" style="height: 25px">
                                            <asp:Button ID="btnAdd" runat="server" Text="�� �ύ ��" CssClass="submit6" OnClick="btnAdd_Click">
                                            </asp:Button>
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
</body>
</html>
<script type="text/javascript" language="javascript">
    function show_tr(num) {
        var var_tr_1 = document.getElementById("tr_1");
        var var_tr_2 = document.getElementById("tr_2");
        var var_tr_3 = document.getElementById("tr_3");
        var var_tr_4 = document.getElementById("tr_4");
        var var_tr_5 = document.getElementById("tr_5");
        var var_tr_6 = document.getElementById("tr_6");
        var var_tr_7 = document.getElementById("tr_7");
        var var_tr_8 = document.getElementById("tr_8");
        var var_tr_9 = document.getElementById("tr_9");
        var var_tr_10 = document.getElementById("tr_10");
        var var_tr_11 = document.getElementById("tr_11");
        var var_tr_12 = document.getElementById("tr_12");
        var var_tr_13 = document.getElementById("tr_13");
        var var_tr_14 = document.getElementById("tr_14");
        var var_tr_15 = document.getElementById("tr_15");
        var var_tr_16 = document.getElementById("tr_16");
        var var_tr_17 = document.getElementById("tr_17");
        var var_tr_18 = document.getElementById("tr_18");
        var var_tr_19 = document.getElementById("tr_19");

        if (num == 1) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "none";
            var_tr_3.style.display = "none";
            var_tr_4.style.display = "none";
            var_tr_5.style.display = "none";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 2) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "none";
            var_tr_4.style.display = "none";
            var_tr_5.style.display = "none";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 3) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "none";
            var_tr_5.style.display = "none";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 4) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "none";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 5) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 6) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 7) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 8) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 9) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "none";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 10) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "none";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 11) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "none";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 12) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "none";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 13) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "";
            var_tr_14.style.display = "none";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 14) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "";
            var_tr_14.style.display = "";
            var_tr_15.style.display = "none";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 15) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "";
            var_tr_14.style.display = "";
            var_tr_15.style.display = "";
            var_tr_16.style.display = "none";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 16) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "";
            var_tr_14.style.display = "";
            var_tr_15.style.display = "";
            var_tr_16.style.display = "";
            var_tr_17.style.display = "none";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 17) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "";
            var_tr_14.style.display = "";
            var_tr_15.style.display = "";
            var_tr_16.style.display = "";
            var_tr_17.style.display = "";
            var_tr_18.style.display = "none";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 18) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "";
            var_tr_14.style.display = "";
            var_tr_15.style.display = "";
            var_tr_16.style.display = "";
            var_tr_17.style.display = "";
            var_tr_18.style.display = "";
            var_tr_19.style.display = "none";
            return;
        }
        else if (num == 19) {
            var_tr_1.style.display = "";
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            var_tr_11.style.display = "";
            var_tr_12.style.display = "";
            var_tr_13.style.display = "";
            var_tr_14.style.display = "";
            var_tr_15.style.display = "";
            var_tr_16.style.display = "";
            var_tr_17.style.display = "";
            var_tr_18.style.display = "";
            var_tr_19.style.display = "";
            return;
        }
    }

    function show_tr_2(num) {
        var var_tr_2_1 = document.getElementById("tr_2_1");
        var var_tr_2_2 = document.getElementById("tr_2_2");
        var var_tr_2_3 = document.getElementById("tr_2_3");
        var var_tr_2_4 = document.getElementById("tr_2_4");
        var var_tr_2_5 = document.getElementById("tr_2_5");
        var var_tr_2_6 = document.getElementById("tr_2_6");
        var var_tr_2_7 = document.getElementById("tr_2_7");
        var var_tr_2_8 = document.getElementById("tr_2_8");
        var var_tr_2_9 = document.getElementById("tr_2_9");
        var var_tr_2_10 = document.getElementById("tr_2_10");
        var var_tr_2_11 = document.getElementById("tr_2_11");
        var var_tr_2_12 = document.getElementById("tr_2_12");
        var var_tr_2_13 = document.getElementById("tr_2_13");
        var var_tr_2_14 = document.getElementById("tr_2_14");
        var var_tr_2_15 = document.getElementById("tr_2_15");
        var var_tr_2_16 = document.getElementById("tr_2_16");
        var var_tr_2_17 = document.getElementById("tr_2_17");
        var var_tr_2_18 = document.getElementById("tr_2_18");
        var var_tr_2_19 = document.getElementById("tr_2_19");

        if (num == 1) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "none";
            var_tr_2_3.style.display = "none";
            var_tr_2_4.style.display = "none";
            var_tr_2_5.style.display = "none";
            var_tr_2_6.style.display = "none";
            var_tr_2_7.style.display = "none";
            var_tr_2_8.style.display = "none";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 2) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "none";
            var_tr_2_4.style.display = "none";
            var_tr_2_5.style.display = "none";
            var_tr_2_6.style.display = "none";
            var_tr_2_7.style.display = "none";
            var_tr_2_8.style.display = "none";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 3) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "none";
            var_tr_2_5.style.display = "none";
            var_tr_2_6.style.display = "none";
            var_tr_2_7.style.display = "none";
            var_tr_2_8.style.display = "none";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 4) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "none";
            var_tr_2_6.style.display = "none";
            var_tr_2_7.style.display = "none";
            var_tr_2_8.style.display = "none";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 5) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "none";
            var_tr_2_7.style.display = "none";
            var_tr_2_8.style.display = "none";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 6) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "none";
            var_tr_2_8.style.display = "none";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 7) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "none";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 8) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "none";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 9) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "none";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 10) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "none";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 11) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "none";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 12) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "none";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 13) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "";
            var_tr_2_14.style.display = "none";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 14) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "";
            var_tr_2_14.style.display = "";
            var_tr_2_15.style.display = "none";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 15) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "";
            var_tr_2_14.style.display = "";
            var_tr_2_15.style.display = "";
            var_tr_2_16.style.display = "none";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 16) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "";
            var_tr_2_14.style.display = "";
            var_tr_2_15.style.display = "";
            var_tr_2_16.style.display = "";
            var_tr_2_17.style.display = "none";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 17) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "";
            var_tr_2_14.style.display = "";
            var_tr_2_15.style.display = "";
            var_tr_2_16.style.display = "";
            var_tr_2_17.style.display = "";
            var_tr_2_18.style.display = "none";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 18) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "";
            var_tr_2_14.style.display = "";
            var_tr_2_15.style.display = "";
            var_tr_2_16.style.display = "";
            var_tr_2_17.style.display = "";
            var_tr_2_18.style.display = "";
            var_tr_2_19.style.display = "none";
            return;
        }
        else if (num == 19) {
            var_tr_2_1.style.display = "";
            var_tr_2_2.style.display = "";
            var_tr_2_3.style.display = "";
            var_tr_2_4.style.display = "";
            var_tr_2_5.style.display = "";
            var_tr_2_6.style.display = "";
            var_tr_2_7.style.display = "";
            var_tr_2_8.style.display = "";
            var_tr_2_9.style.display = "";
            var_tr_2_10.style.display = "";
            var_tr_2_11.style.display = "";
            var_tr_2_12.style.display = "";
            var_tr_2_13.style.display = "";
            var_tr_2_14.style.display = "";
            var_tr_2_15.style.display = "";
            var_tr_2_16.style.display = "";
            var_tr_2_17.style.display = "";
            var_tr_2_18.style.display = "";
            var_tr_2_19.style.display = "";
            return;
        }
    }
</script>
<script type="text/javascript" language="javascript">

    $(function () {
        ShowBirth0101();
        ShowFMName();
    });
    // window.onload=function(){
    // ShowBirth0101(); ShowFMName();
    //}
</script>
