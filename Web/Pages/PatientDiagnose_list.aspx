<!--编辑表单form与datagrid列表数据分别放在两个独立的aspx页面中-->
<!--datagrid页面:PatientDiagnose_list.aspx-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientDiagnose_list.aspx.cs" Inherits="RuRo.PatientDiagnose_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head">
<title>PatientDiagnose</title>
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="include/js/jquery.cookie.js"></script>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <script src="../include/js/sy_func.js"></script>
    <script src="include/js/DateFmt.js"></script>
</head>
<body>
<!--datagrid栏--> 
<table id="datagridP" title="诊断信息" class="easyui-datagrid" style="width:auto;height:260px"
              fit='false'
             pagination="false" rownumbers="true" 
             fitColumns="true"  singleSelect="false" toolbar="#toolbarP"
             striped="false" <%--pageList="[15,30,50,100,500]--%>"
             SelectOnCheck="true" CheckOnSelect="true" remoteSort="false">
    <thead>    
			<tr>
			    <th field="ck" checkbox="true"></th>
                <th field="id" width="100" sortable="true" hidden="hidden">id</th>
                <th field="Cardno" width="100" sortable="true" editor:'textbox'>卡号</th>
                <th field="Csrq00" width="100" sortable="true" >查询日期</th>
                <th field="Patientname" width="100" sortable="true">姓名</th>
                <th field="Sex" width="100" sortable="true" hidden="hidden">性别</th>
                <th field="Brithday" width="100" sortable="true" hidden="hidden">出生日期</th>
                <th field="Cardid" width="100" sortable="true" hidden="hidden">身份证号</th>
                <th field="Tel" width="100" sortable="true" hidden="hidden">tel</th>
                <th field="Registerno" width="100" sortable="true" hidden="hidden">registerno</th>
                <th field="Icd" width="100" sortable="true" hidden="hidden">ICD码</th>
                <th field="Diagnose" width="100" sortable="true">诊断名称</th>
                <th field="Type" width="100" sortable="true">诊断类型</th>
                <th field="Flag" width="100" sortable="true">诊断类别</th>
                <th field="Diagnosedate" width="100" sortable="true">诊断日期</th>
            </tr>
    </thead>
</table>

<!--toolbar栏，用于datagrid的toolbar自定义内容--> 
<div id="toolbarP">
<table style="width:100%;">
<tr>
    <!--button按钮工具栏--> 
    <td  style="text-align:right;">
        <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconCls="icon-cancel" plain="false" onclick="destroy($('#datagridP'),'/Sever/PatientDiagnose.ashx');">删除</a>
    </td>
</tr>
</table>  
</div>

<!--diaglog窗口，用于编辑数据--> 
<div id="dlg"  class="easyui-dialog" closed="true"></div>
</body>
</html>
