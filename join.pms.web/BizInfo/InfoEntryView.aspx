<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoEntryView.aspx.cs"
    Inherits="join.pms.web.BizInfo.InfoEntryView" %>

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

    <link rel="stylesheet" href="../css/index1.css"/>
    <link rel="stylesheet"  href="../css/zoom.css" media="all" />


    <script type="text/javascript">
        var close = document.getElementById("close_1");
        var height = parseInt((window.screen.height - 500) / 2);
        close.style.top = "" + height + "px";
    </script>
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
                                                    </td>
                                                </tr>
                                                <tr style="height:30px">
                                                    <th style="width: 100px">
                                                        <span class="xing">*</span>资金来源：
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_SourceFunds" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height:30px">
                                                    <th>
                                                        <span class="xing">*</span>资金总额：
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_AllMoney" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height:30px">
                                                    <td colspan="2">
                                                        <asp:Literal ID="ltr_ProjectInfo" runat="server"></asp:Literal>
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
                                            <asp:Literal ID="ltr_PaymentPlan" runat="server"></asp:Literal>
                                        </div>
                                        <div style="padding-left: 25px">
                                            <asp:Literal ID="ltr_P_PaymentPlan" runat="server"></asp:Literal>
                                            <table width="900px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <div class="container" style="width:1000px;height:180px;">	 
	                                                        <ul class="gallery" style="padding-left:25px; width:1000px">
                                                                <asp:Literal ID="ltr_Img" runat="server"></asp:Literal>
	                                                        </ul>
                                                        </div>
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
