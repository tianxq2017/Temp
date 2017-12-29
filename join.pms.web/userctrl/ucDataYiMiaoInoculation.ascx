<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDataYiMiaoInoculation.ascx.cs" Inherits="join.pms.web.userctrl.ucDataYiMiaoInoculation" %>
<!-- ScriptManager控件请在页面添加 -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<!------start--------->
<tr>
    <th width="163" bgcolor="#F9F9F9"><strong>
      <input name="ChkPoliomyelitis2" id="ChkPoliomyelitis2" type="checkbox" value="6" checked="checked" />
      &nbsp;&nbsp;
      疫苗种类：</strong></th>
    <td width="249" bgcolor="#F9F9F9"><p><strong> 卡介苗 </strong></p></td>
    <th width="89" bgcolor="#F9F9F9"><strong>疫苗小类：</strong></th>
    <td width="324" bgcolor="#F9F9F9"><p><strong> 乙肝疫苗(酿酒酵母) </strong></p></td>
  </tr>
  <tr>
    <th>接种部位：</th>
    <td><p>
    <asp:DropDownList runat="server" ID="ddlInoculationPart">
    <asp:ListItem Text="左上臂" Value="1"></asp:ListItem>
    <asp:ListItem Text="右上臂" Value="2"></asp:ListItem>
    <asp:ListItem Text="左大腿" Value="3"></asp:ListItem>
    <asp:ListItem Text="右大腿" Value="4"></asp:ListItem>
    <asp:ListItem Text="左臀部" Value="5"></asp:ListItem>
    <asp:ListItem Text="右臀部" Value="6"></asp:ListItem>
    <asp:ListItem Text="口服" Value="7"></asp:ListItem>
    <asp:ListItem Text="其他" Value="8"></asp:ListItem>                            
    </asp:DropDownList>

      </p></td>
    <th><span class="xing">*</span>生产批号：</th>
    <td><p>
        <asp:DropDownList runat="server" ID="ddlPiHao" AutoPostBack="true" OnSelectedIndexChanged="ddlPiHao_SelectedIndexChanged">                                
        </asp:DropDownList>                                  
      </p></td>
  </tr>
  <tr>
    <th>厂商：</th>
    <td colspan="3"><asp:Literal runat="server" ID="litCompany"></asp:Literal>北京天坛 &nbsp;&nbsp;&nbsp;&nbsp;剂量：0.5毫升<asp:Literal runat="server" ID="litDrpBacZhushe"></asp:Literal> &nbsp;&nbsp;&nbsp;&nbsp;注射方式：肌内<asp:Literal ID="litDrpBacFangShi" runat="server"></asp:Literal></td>
  </tr>
  
  <input type="hidden" name="txtDrpBacName" id="txtDrpBacName" value="" runat="server" style="display:none;"/>
<!------end--------->
</ContentTemplate></asp:UpdatePanel>
