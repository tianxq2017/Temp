<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasisDocument.aspx.cs"
    Inherits="join.pms.web.BizInfo.BasisDocument" %>

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
                                            <b>依据文件</b></p>
                                        <div class="form_table">
                                            <table width="800px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr style="height:30px;">
                                                                <th>
                                                                    项目内容（文件名称）：
                                                                </th>
                                                                <td colspan="3">
                                                                    <asp:Literal ID="ltr_FileName" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <th style=" width:150px">
                                                                    省计划文号：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_Province_Symbol" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th style=" width:120px">
                                                                    省资金文号：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_Province_Money_Symbol" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <th>
                                                                    市计划文号：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_City_Symbol" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    市资金文号：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_City_Money_Symbol" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <th>
                                                                    发文日期：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <input id="txt_B_IssueDate_1" runat="server" onclick="SelectDate(document.getElementById('txt_B_IssueDate_1'),'yyyy-MM-dd')" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    下达金额：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_IssueDamount_1" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <th>
                                                                    县计划文号：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_County_Symbol" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    县资金文号：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_County_Money_Symbol" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <th>
                                                                    发文日期：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <input id="txt_B_IssueDate_2" runat="server" onclick="SelectDate(document.getElementById('txt_B_IssueDate_2'),'yyyy-MM-dd')" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                                <th>
                                                                    下达金额：
                                                                </th>
                                                                <td class="text">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_IssueDamount_2" runat="server" EnableViewState="False" MaxLength="25" />
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <th>
                                                                    备注：
                                                                </th>
                                                                <td class="text" colspan="3">
                                                                    <p>
                                                                        <span>
                                                                            <asp:TextBox ID="txt_B_Remarks" runat="server" Width="500px" EnableViewState="False"/>
                                                                        </span>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <td colspan="4">
                                                                    年度财政扶贫项目资金往来汇总表
                                                                </td>
                                                            </tr>
                                                            <tr style="height:30px;">
                                                                <td class="text" colspan="4">
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
