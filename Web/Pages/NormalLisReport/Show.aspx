<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="RuRo.Web.NormalLisReport.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		Id
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		病人门诊号、住院号
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblhospnum" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		姓名
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblpatname" runat="server"></asp:Label>
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
		年龄
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblAge" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		月
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblage_month" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		项目总称
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblext_mthd" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		项目名称
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblchinese" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		结果
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblresult" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		单位
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblunits" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		高低 
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblref_flag" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		正常低值
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbllowvalue" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		正常高值
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblhighvalue" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		正常范围
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblprint_ref" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		批准时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblcheck_date" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		批准人
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblcheck_by_name" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		打印顺序序号
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblprnt_order" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




