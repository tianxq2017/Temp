<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDataYiMiaoInoculation.ascx.cs" Inherits="join.pms.web.userctrl.ucDataYiMiaoInoculation" %>
<!-- ScriptManager�ؼ�����ҳ����� -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<tr>
    <th width="163" bgcolor="#F9F9F9"><strong>
      <input name="ChkPoliomyelitis2" id="ChkPoliomyelitis2" type="checkbox" value="6" checked="checked" />
      &nbsp;&nbsp;
      �������ࣺ</strong></th>
    <td width="249" bgcolor="#F9F9F9"><p><strong> ������ </strong></p></td>
    <th width="89" bgcolor="#F9F9F9"><strong>����С�ࣺ</strong></th>
    <td width="324" bgcolor="#F9F9F9"><p><strong> �Ҹ�����(��ƽ�ĸ) </strong></p></td>
  </tr>
  <tr>
    <th>���ֲ�λ��</th>
    <td><p>
    <asp:DropDownList runat="server" ID="ddlInoculationPart">
    <asp:ListItem Text="���ϱ�" Value="1"></asp:ListItem>
    <asp:ListItem Text="���ϱ�" Value="2"></asp:ListItem>
    <asp:ListItem Text="�����" Value="3"></asp:ListItem>
    <asp:ListItem Text="�Ҵ���" Value="4"></asp:ListItem>
    <asp:ListItem Text="���β�" Value="5"></asp:ListItem>
    <asp:ListItem Text="���β�" Value="6"></asp:ListItem>
    <asp:ListItem Text="�ڷ�" Value="7"></asp:ListItem>
    <asp:ListItem Text="����" Value="8"></asp:ListItem>                            
    </asp:DropDownList>

      </p></td>
    <th><span class="xing">*</span>�������ţ�</th>
    <td><p>
        <asp:DropDownList runat="server" ID="ddlPiHao" AutoPostBack="true" OnSelectedIndexChanged="ddlPiHao_SelectedIndexChanged">                                
        </asp:DropDownList>                                  
      </p></td>
  </tr>
  <tr>
    <th>���̣�</th>
    <td colspan="3"><asp:Literal runat="server" ID="litCompany"></asp:Literal>������̳ &nbsp;&nbsp;&nbsp;&nbsp;������0.5����<asp:Literal runat="server" ID="litDrpBacZhushe"></asp:Literal> &nbsp;&nbsp;&nbsp;&nbsp;ע�䷽ʽ������<asp:Literal ID="litDrpBacFangShi" runat="server"></asp:Literal></td>
  </tr>
  
  <input type="hidden" name="txtDrpBacName" id="txtDrpBacName" value="" runat="server" style="display:none;"/>
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
