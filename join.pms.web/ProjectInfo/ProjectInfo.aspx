<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectInfo.aspx.cs" Inherits="join.pms.web.BizInfo.ProjectInfo" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目管理-项目信息采集</title>
    <meta http-equiv="Content-type" content="text/html; charset=gb2312" />
    <link href="/css/right.css" type="text/css" rel="stylesheet" />
    <link href="../css/tanchu.css" type="text/css" rel="Stylesheet" />
    <script src="/Scripts/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jsdate.js" type="text/javascript"></script>
    <script src="/includes/dataGrid.js" type="text/javascript"></script>
    <script src="../Scripts/tanchu.js" type="text/javascript"></script>
    <script src="../Scripts/CommSys.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../css/index1.css" />
    <link rel="stylesheet" href="../css/zoom.css" media="all" />
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
                                            <b>单位用款申报情况（单位填报）</b></p>
                                        <div class="form_table">
                                            <table width="800px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <th style="width: 120px">
                                                        <span class="xing">*</span>资金申报单位：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_UnitInfo" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 130px">
                                                        <span class="xing">*</span>申报日期：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_P_CreationTime" runat="server" onclick="SelectDate(document.getElementById('txt_P_CreationTime'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        <span class="xing">*</span>项目分类：
                                                    </th>
                                                    <td class="select">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="dr_P_ProjectType" runat="server">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        <span class="xing">*</span>项目名称：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_Name" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        <span class="xing">*</span>专款文号：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_SpecialNumber" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        <span class="xing">*</span>下达指标数：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_IndicatorsNumber_1" runat="server" EnableViewState="False"
                                                                    MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        <span class="xing">*</span>累计执行数：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_CumulativeNumber_1" runat="server" EnableViewState="False"
                                                                    MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        <span class="xing">*</span>余额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_Balance_1" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        <span class="xing">*</span>本次申请数：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_ApplicationsNumber_1" runat="server" EnableViewState="False"
                                                                    MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        <span class="xing">*</span>资金用途：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_UseFunds" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        <span class="xing">*</span>支付方式：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_PaymentMethod" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        <span class="xing">*</span>收款人账户名称：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_PayeeName" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        <span class="xing">*</span>账号：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_AccountNumber" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        <span class="xing">*</span>开户银行：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_BankAddress" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <%if(Is_Show == "0")
                                                {%>
                                                <tr style="height: 60px">
                                                    <th>
                                                        依据文件
                                                    </th>
                                                    <td colspan="6" style="text-align: left; padding-left: 20px" class="select">
                                                        <!-- 插入各种附件 Start -->
                                                        <input id="txtDocsID" name="txtDocsID" type="hidden" runat="server" />
                                                        <input id="txtDocsName" name="txtDocsName" class="cuserinput" check="1" show="附件名称"
                                                            size="50" style="width: 360px; background-color: #fafafa" />
                                                        <input type="button" class="submit6" value="选择.." onclick="SelectCmsDocs('txtDocsID','txtDocsName')"
                                                            class="cusersubmit" />
                                                        <!-- 插入各种附件 End -->
                                                    </td>
                                                </tr>
                                                <%}%>
                                            </table>
                                        </div>
                                    </div>
                                    <%if(Is_Show == "1"){%>
                                    <%--依据--%>
                                    <asp:Literal ID="ltr_Img" runat="server"></asp:Literal>
                                    <%}%>
                                    <div class="form_a">
                                        <p class="form_title">
                                            <b>支付计划</b></p>
                                        <div class="form_table">
                                            <table width="1000px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <th style="width: 140px">
                                                        项目总金额：
                                                    </th>
                                                    <td class="text" colspan="6">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_AllMoney" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                        万元
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="width: 120px">
                                                        第一次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_1" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_1" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_1" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_1'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_1" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(1)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_2" style="display: none">
                                                    <th style="width: 120px">
                                                        第二次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_2" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_2" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_2" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_2'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_2" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(2)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_3" style="display: none">
                                                    <th style="width: 120px">
                                                        第三次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_3" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_3" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_3" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_3'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_3" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(3)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_4" style="display: none">
                                                    <th style="width: 120px">
                                                        第四次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_4" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_4" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_4" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_4'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_4" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(4)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_5" style="display: none">
                                                    <th style="width: 120px">
                                                        第五次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_5" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_5" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                               <input id="txt_DateTime_5" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_5'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_5" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(5)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_6" style="display: none">
                                                    <th style="width: 120px">
                                                        第六次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_6" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_6" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_6" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_6'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_6" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(6)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_7" style="display: none">
                                                    <th style="width: 120px">
                                                        第七次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_7" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_7" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_7" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_7'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_7" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(7)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_8" style="display: none">
                                                    <th style="width: 120px">
                                                        第八次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_8" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_8" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_8" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_8'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_8" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(8)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_9" style="display: none">
                                                    <th style="width: 120px">
                                                        第九次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_9" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_9" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_9" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_9'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_9" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <a href="javascript:show_tr(9)">
                                                            <img src="../images/add.gif" alt="" /></a>
                                                    </td>
                                                </tr>
                                                <tr id="tr_10" style="display: none">
                                                    <th style="width: 120px">
                                                        第十次支付金额：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_AmountOfMoney_10" runat="server" EnableViewState="False" MaxLength="25" />
                                                                <asp:HiddenField ID="hd_ID_10" runat="server" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        资金到位时间：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <input id="txt_DateTime_10" runat="server" onclick="SelectDate(document.getElementById('txt_DateTime_10'),'yyyy-MM-dd')" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th style="width: 120px">
                                                        预计完工量：
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_UnitOfUse_10" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
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
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/zoom.min.js"></script>
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

        if (num == 1) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "none";
            var_tr_4.style.display = "none";
            var_tr_5.style.display = "none";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 2) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "none";
            var_tr_5.style.display = "none";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 3) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "none";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 4) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "none";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 5) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "none";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 6) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "none";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 7) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "none";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 8) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "none";
            return;
        }
        else if (num == 9) {
            var_tr_2.style.display = "";
            var_tr_3.style.display = "";
            var_tr_4.style.display = "";
            var_tr_5.style.display = "";
            var_tr_6.style.display = "";
            var_tr_7.style.display = "";
            var_tr_8.style.display = "";
            var_tr_9.style.display = "";
            var_tr_10.style.display = "";
            return;
        }
    }
</script>
