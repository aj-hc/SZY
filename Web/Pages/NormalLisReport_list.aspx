<!--编辑表单form与datagrid列表数据分别放在两个独立的aspx页面中-->
<!--datagrid页面:NormalLisReport_list.aspx-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NormalLisReport_list.aspx.cs" Inherits="RuRo.NormalLisReport_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <title>NormalLisReport</title>
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/js/jquery.cookie.js"></script>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <script src="../include/js/DateFmt.js"></script>
    <script src="../include/js/sy_func.js"></script>
    <script src="../include/js/DgGRUD.js"></script>
</head>
<body>
    <form id="NormalLisReportForm">
        <table id="datagridN" title="检测信息" class="easyui-datagrid" style="width: auto; height: 260px"
            fit='false'
            pagination="false" rownumbers="true"
            fitcolumns="true" singleselect="false" toolbar="#toolbarN"
            striped="false" <%-- pageList="[5,10,15]"--%>
            selectoncheck="true" checkonselect="true" remotesort="false">
            <thead>
                <tr>
                    <th field="ck" checkbox="true"></th>
                    <th field="id" width="100" sortable="true" hidden="hidden">id</th>
                    <th field="hospnum" width="100" sortable="true" hidden="hidden">病人门诊号、住院号</th>
                    <th field="patname" width="100" sortable="true">姓名</th>
                    <th field="sex" width="100" sortable="true">性别</th>
                    <th field="age" width="100" sortable="true">年龄</th>
                    <th field="age_month" width="100" sortable="true" hidden="hidden">月</th>
                    <th field="ext_mthd" width="100" sortable="true">项目总称</th>
                    <th field="chinese" width="100" sortable="true">项目名称</th>
                    <th field="result" width="100" sortable="true" hidden="hidden">结果</th>
                    <th field="units" width="100" sortable="true" hidden="hidden">单位</th>
                    <th field="ref_flag" width="100" sortable="true">高低</th>
                    <th field="lowvalue" width="100" sortable="true" hidden="hidden">正常低值</th>
                    <th field="highvalue" width="100" sortable="true" hidden="hidden">正常高值</th>
                    <th field="print_ref" width="100" sortable="true" hidden="hidden">正常范围</th>
                    <th field="check_date" width="100" sortable="true">批准时间</th>
                    <th field="check_by_name" width="100" sortable="true" hidden="hidden">批准人</th>
                    <th field="prnt_order" width="100" sortable="true" hidden="hidden">打印顺序序号</th>
                </tr>
            </thead>
        </table>
    </form>

    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <div id="toolbarN">
        <table style="width: 100%;">
            <tr>
                <!--button按钮工具栏-->
                <td style="text-align: right;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick=" destroy($('#datagridN'), '/Sever/NormalLisReport.ashx');">删除</a>
                </td>
            </tr>
        </table>
    </div>

    <!--diaglog窗口，用于编辑数据-->
    <div id="dlg" class="easyui-dialog" closed="true"></div>
</body>
</html>
