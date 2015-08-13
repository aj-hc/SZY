<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="RuRo.Web.PatientDiagnose.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		id
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		卡号
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCardno" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		查询日期
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCsrq00" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		姓名
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblPatientName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		性别
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblSex" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		出生日期
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblBrithday" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		身份证号
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCardId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		Tel
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblTel" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		RegisterNo
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRegisterNo" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		ICD码
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblIcd" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		诊断名称
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblDiagnose" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		诊断类型:1：中医疾病 2：中
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblType" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		诊断类别:1：西医诊断 2 中
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblFlag" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		诊断日期
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblDiagnoseDate" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		isDel
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblisDel" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




