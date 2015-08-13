<%@ Page Title="NormalLisReport" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="RuRo.Web.NormalLisReport.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!--Title -->

            <!--Title end -->

            <!--Add  -->

            <!--Add end -->

            <!--Search -->
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td style="width: 80px" align="right" class="tdbg">
                         <b>关键字：</b>
                    </td>
                    <td class="tdbg">                       
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询"  OnClick="btnSearch_Click" >
                    </asp:Button>                    
                        
                    </td>
                    <td class="tdbg">
                    </td>
                </tr>
            </table>
            <!--Search end-->
            <br />
            <asp:GridView ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"  OnPageIndexChanging ="gridView_PageIndexChanging"
                    BorderWidth="1px" DataKeyNames="Id" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="false" PageSize="10"  RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
                    <Columns>
                    <asp:TemplateField ControlStyle-Width="30" HeaderText="选择"    >
                                <ItemTemplate>
                                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            
		<asp:BoundField DataField="hospnum" HeaderText="病人门诊号、住院号" SortExpression="hospnum" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="patname" HeaderText="姓名" SortExpression="patname" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="sex" HeaderText="性别" SortExpression="sex" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="age" HeaderText="年龄" SortExpression="age" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="age_month" HeaderText="月" SortExpression="age_month" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ext_mthd" HeaderText="项目总称" SortExpression="ext_mthd" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="chinese" HeaderText="项目名称" SortExpression="chinese" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="result" HeaderText="结果" SortExpression="result" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="units" HeaderText="单位" SortExpression="units" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ref_flag" HeaderText="高低 " SortExpression="ref_flag" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="lowvalue" HeaderText="正常低值" SortExpression="lowvalue" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="highvalue" HeaderText="正常高值" SortExpression="highvalue" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="print_ref" HeaderText="正常范围" SortExpression="print_ref" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="check_date" HeaderText="批准时间" SortExpression="check_date" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="check_by_name" HeaderText="批准人" SortExpression="check_by_name" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="prnt_order" HeaderText="打印顺序序号" SortExpression="prnt_order" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="isDel" HeaderText="isDel" SortExpression="isDel" ItemStyle-HorizontalAlign="Center"  /> 
                            
                            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Show.aspx?id={0}"
                                Text="详细"  />
                            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                                Text="编辑"  />
                            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除"   Visible="false"  >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                         Text="删除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView>
               <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
                <tr>
                    <td style="width: 1px;">                        
                    </td>
                    <td align="left">
                        <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click"/>                       
                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>
