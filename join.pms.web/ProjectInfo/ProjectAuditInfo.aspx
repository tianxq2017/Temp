<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectAuditInfo.aspx.cs"
    Inherits="join.pms.web.BizInfo.ProjectAuditInfo" %>

<%@ Register Src="../userctrl/ucAreaSel.ascx" TagName="ucAreaSel" TagPrefix="uc3" %>
<%@ Register Src="/userctrl/ucAreaSelXian.ascx" TagName="ucAreaSelXian" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��Ŀ����-��Ŀ��Ϣ�ɼ�</title>
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
                                            <b>��λ�ÿ��걨�������λ���</b></p>
                                        <div class="form_table">
                                            <table width="800px" border="0" cellspacing="0" cellpadding="0">
                                                <tr style="height: 30px">
                                                    <th style="width: 120px">
                                                        �ʽ��걨��λ��
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_UnitInfo" runat="server"></asp:Literal>
                                                    </td>
                                                    <th style="width: 130px">
                                                        �걨���ڣ�
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_CreationTime" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        ��Ŀ���ࣺ
                                                    </th>
                                                    <td class="select">
                                                        <asp:Literal ID="ltr_P_ProjectType" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        ��Ŀ���ƣ�
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_Name" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        ר���ĺţ�
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_SpecialNumber" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        �´�ָ������
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_IndicatorsNumber_1" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        �ۼ�ִ������
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_CumulativeNumber_1" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        ��
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_Balance_1" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        ������������
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_ApplicationsNumber_1" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        �ʽ���;��
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_UseFunds" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        ֧����ʽ��
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_PaymentMethod" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        �տ����˻����ƣ�
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_PayeeName" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        �˺ţ�
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_AccountNumber" runat="server"></asp:Literal>
                                                    </td>
                                                    <th>
                                                        �������У�
                                                    </th>
                                                    <td class="text">
                                                        <asp:Literal ID="ltr_P_BankAddress" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        ���״̬��
                                                    </th>
                                                    <td class="select" colspan="3">
                                                        <p>
                                                            <span>
                                                                <asp:DropDownList ID="ddl_P_State" runat="server">
                                                                    <asp:ListItem Value="1">���ͨ��</asp:ListItem>
                                                                    <asp:ListItem Value="2">δͨ�����</asp:ListItem>
                                                                    <asp:ListItem Value="3">���ݴ����޸�</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <th>
                                                        ��������
                                                    </th>
                                                    <td class="text" colspan="3">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_AuditInfo" runat="server" EnableViewState="False" Width="600px" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <%--����--%>
                                    <asp:Literal ID="ltr_Img" runat="server"></asp:Literal>
                                    <div class="form_a">
                                        <p class="form_title">
                                            <b>��ˡ��������</b></p>
                                        <div class="form_table">
                                            <table width="900px" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <th style="width: 120px">
                                                        �ϼ�ר���ĺţ�
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_SuperfineSignature" runat="server" EnableViewState="False"
                                                                    MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ����ת���ĺţ�
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_ThisLeveSymboll" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="width: 120px">
                                                        �´�ָ������
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_IndicatorsNumber_2" runat="server" EnableViewState="False"
                                                                    MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        �ۼ���ִ������
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_CumulativeNumber_2" runat="server" EnableViewState="False"
                                                                    MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        ��
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_Balance_2" runat="server" EnableViewState="False" MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                    <th>
                                                        ������������
                                                    </th>
                                                    <td class="text">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_ApplicationsNumber_2" runat="server" EnableViewState="False"
                                                                    MaxLength="25" />
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>
                                                        ��ע��
                                                    </th>
                                                    <td class="text" colspan="3">
                                                        <p>
                                                            <span>
                                                                <asp:TextBox ID="txt_P_Remarks" runat="server" EnableViewState="False" Width="600px"/>
                                                            </span>
                                                        </p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
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
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/zoom.min.js"></script>
</body>
</html>
