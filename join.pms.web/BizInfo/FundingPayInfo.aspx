<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundingPayInfo.aspx.cs"
    Inherits="join.pms.web.BizInfo.FundingPayInfo" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>资金管理-资金信息采集</title>
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
    <!--导航信息-->
    <div class="mbx">
        当前位置：<asp:Literal ID="LiteralNav" runat="server"></asp:Literal></div>
    <!--提示信息-->
    <div class="tsxx">
        提示信息：请逐一填写下列信息后；点击“提交”按钮……</div>
    <!-- 页面参数 -->
    <input type="hidden" name="txtObjID" id="txtObjID" value="" runat="server" style="display: none;" />
    <!--Start-->
    <div id="ec_bodyZone">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#99A89C">
            <tr>
                <td bgcolor="#F2F9EC">
                    <table width="100%" border="0" cellpadding="8" cellspacing="5" bgcolor="#E2EBE4">
                        <tr>
                            <td bgcolor="#F3FCFF" align="left">
                                <!-- 编辑区  -->
                                <div class="form_bg">
                                    <div class="form_a">
                                        <p class="form_title">
                                            <b>项目内容</b></p>
                                        <div class="form_table">
                                            <table width="800px" border="0" cellspacing="0" cellpadding="0">
                                                <tr style="height:30px">
                                                    <th style="width: 100px">
                                                        <span class="xing">*</span>文件名称：
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_Name" runat="server"></asp:Literal>
                                                        <asp:HiddenField ID="hd_P_ID" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr style="height:30px">
                                                    <th style="width: 100px">
                                                        <span class="xing">*</span>支付时间：
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_F_PayDateTime" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height:30px">
                                                    <th>
                                                        <span class="xing">*</span>支付金额：
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
                                                                    使用单位：
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
                                                                    金额明细：
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
                                                                    使用单位1：
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
                                                                    金额明细1：
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
                                                                    使用单位2：
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
                                                                    金额明细2：
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
                                                                    使用单位3：
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
                                                                    金额明细3：
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
                                                                    使用单位4：
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
                                                                    金额明细4：
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
                                                                    使用单位5：
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
                                                                    金额明细5：
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
                                                                    使用单位6：
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
                                                                    金额明细6：
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
                                                                    使用单位7：
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
                                                                    金额明细7：
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
                                                                    使用单位8：
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
                                                                    金额明细8：
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
                                                                    使用单位9：
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
                                                                    金额明细9：
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
                                                                    使用单位10：
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
                                                                    金额明细10：
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
                                                                    使用单位11：
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
                                                                    金额明细11：
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
                                                                    使用单位12：
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
                                                                    金额明细12：
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
                                                                    使用单位13：
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
                                                                    金额明细13：
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
                                                                    使用单位14：
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
                                                                    金额明细14：
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
                                                                    使用单位15：
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
                                                                    金额明细15：
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
                                                                    使用单位16：
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
                                                                    金额明细16：
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
                                                                    使用单位17：
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
                                                                    金额明细17：
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
                                                                    使用单位18：
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
                                                                    金额明细18：
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
                                                                    使用单位19：
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
                                                                    金额明细19：
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
                                            <b>支付计划</b></p>
                                        <div class="form_table">
                                            <table width="900px" border="0" cellspacing="0" cellpadding="0">
                                                <tr style="height:30px">
                                                    <th style="width: 100px">
                                                        <span class="xing">*</span>支付计划：
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_PaymentPlan" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        设置提醒：
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
                                                        时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        使用单位：
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
                                                        金额：
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
                                                        时间1：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_1" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_1'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位1：
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
                                                        金额1：
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
                                                        时间2：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_2" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_2'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位2：
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
                                                        金额2：
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
                                                        时间3：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_3" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_3'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位3：
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
                                                        金额3：
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
                                                        时间4：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_4" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_4'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位4：
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
                                                        金额4：
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
                                                        时间5：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_5" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_5'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位5：
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
                                                        金额5：
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
                                                        时间6：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_6" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_6'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位6：
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
                                                        金额6：
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
                                                        时间7：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_7" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_7'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位7：
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
                                                        金额7：
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
                                                        时间8：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_8" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_8'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位8：
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
                                                        金额8：
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
                                                        时间9：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_9" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_9'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位9：
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
                                                        金额9：
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
                                                        时间10：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_10" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_10'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位10：
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
                                                        金额10：
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
                                                        时间11：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_11" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_11'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位11：
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
                                                        金额11：
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
                                                        时间12：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_12" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_12'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位12：
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
                                                        金额12：
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
                                                        时间13：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_13" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_13'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位13：
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
                                                        金额13：
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
                                                        时间14：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_14" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_14'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位14：
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
                                                        金额14：
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
                                                        时间15：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_15" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_15'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位15：
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
                                                        金额15：
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
                                                        时间16：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_16" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_16'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位16：
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
                                                        金额16：
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
                                                        时间17：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_17" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_17'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位17：
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
                                                        金额17：
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
                                                        时间18：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_18" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_18'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位18：
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
                                                        金额18：
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
                                                        时间19：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_19" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_19'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        使用单位19：
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
                                                        金额19：
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
                                                        依据文件
                                                    </th>
                                                    <td colspan="6" style="text-align: left; padding-left: 20px">
                                                        <!-- 插入各种附件 Start -->
                                                        <input id="txtDocsID" name="txtDocsID" type="hidden" runat="server" />
                                                        <input id="txtDocsName" name="txtDocsName" class="cuserinput" check="1" show="附件名称"
                                                            size="50" style="width: 360px; background-color: #fafafa" />
                                                        <input type="button" class="submit6" value="选择.." onclick="SelectCmsDocs('txtDocsID','txtDocsName')"
                                                            class="cusersubmit" />
                                                        <!-- 插入各种附件 End -->
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
                                <%--弹出层--%>
                                <%--<div id="ceng">
                                </div>
                                <div id="close">
                                    <a style="position: relative;left:-3px;COLOR:RED;text-decoration: none;" onclick="closeCeng()"><img src="../images/close.png"/ alt=""></a>
                                    <div style="text-align:center;">
                                        <img width="90%" height="450px" style="margin:6px 7px" src="../images/bg-banner.jpg" />
                                    </div>
                                </div>--%>
                                <!-- 操作按钮 m_TargetUrl -->
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="zhengwen">
                                    <tr>
                                        <td align="right" style="width: 100px; height: 30px;">
                                            &nbsp;
                                        </td>
                                        <td width="*" align="left" style="height: 25px">
                                            <asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" CssClass="submit6" OnClick="btnAdd_Click">
                                            </asp:Button>
                                            <input type="button" name="ButBackPage" value="· 返回 ·" id="ButBackPage" onclick="javascript:window.location.href='<%=m_TargetUrl %>';"
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
