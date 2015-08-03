<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientDiagnose_list.aspx.cs" Inherits="RuRo.Web.SZY.PatientDiagnose_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/color.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <link href="../include/css/kfmis.css" rel="stylesheet" />
    <script src="../include/js/DgGRUD.js"></script>
    <title></title>
</head>
<body>
    <!--datagrid栏-->
    <table id="datagrid" title="诊断信息" class="easyui-datagrid" style="width: auto; height: 260px"
        url="PatientDiagnose_handler.ashx?mode=qry" fit='false'
        pagination="true" idfield="id" rownumbers="true"
        fitcolumns="true" singleselect="true" toolbar="#toolbarP"
        striped="false" pagelist="[5,10,15]"
        selectoncheck="true" checkonselect="true" remotesort="true">
        <thead>
            <tr>
                <th field="ck" checkbox="true"></th>
                <th field="Id" width="100" sortable="true" hidden="hidden">Id</th>
                <th field="cardno" width="100" sortable="true">卡号</th>
                <th field="csrq00" width="100" sortable="true" hidden="hidden">查询日期</th>
                <th field="patientname" width="100" sortable="true" hidden="hidden">姓名</th>
                <th field="sex" width="100" sortable="true" hidden="hidden">性别</th>
                <th field="brithday" width="100" sortable="true" hidden="hidden">出生日期</th>
                <th field="cardid" width="100" sortable="true" hidden="hidden">身份证号</th>
                <th field="tel" width="100" sortable="true" hidden="hidden">tel</th>
                <th field="registerno" width="100" sortable="true" hidden="hidden">registerno</th>
                <th field="icd" width="100" sortable="true">ICD码</th>
                <th field="diagnose" width="100" sortable="true">诊断名称</th>
                <th field="type" width="100" sortable="true">诊断类型</th>
                <th field="flag" width="100" sortable="true">诊断类别</th>
                <th field="diagnosedate" width="100" sortable="true">诊断日期</th>
            </tr>
        </thead>
    </table>

    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <div id="toolbarP">
        <table style="width: 100%;">
            <tr>
                <%--    <td>
        <!--查询输入栏--> 
        <table>
            <tr>
               <!--Page数据选择模式-->  
                <td><select onchange="$('#datagrid').datagrid({singleSelect:(this.value==0)})"><option value="0">单选模式</option><option value="1">多选模式</option></select></td>

                <!--查询控件-->
                <td>
                    <!--
                    编码字段<input id="so_字段名称"  class="easyui-combobox" panelHeight="auto"  data-options="valueField:'编码表对应code字段名',textField:'编码表对应name字段名', url:'/common/codeDataHandler.ashx?tabName=编码表名'"/>
                    <input id="date"     class="easyui-datebox" type="text" />
                    -->
                </td>
                <!--检索关键字-->
                <td><input id="so_keywords"  class="easyui-searchbox" data-options="prompt:'请输入查询关键字',searcher:searchData" ></input></td>
            </tr>
        </table> 
    </td>--%>
                <!--button按钮工具栏-->
                <td style="text-align: left;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick="destroy('/Sever/NormalLisReport.ashx');">删除</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
