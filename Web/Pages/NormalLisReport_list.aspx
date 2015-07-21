<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NormalLisReport_list.aspx.cs" Inherits="RuRo.Web.SZY.NormalLisReport_list" %>

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
    <script src="../include/js/page.js"></script>
    <title></title>
</head>
<body>
    <!--datagrid栏-->
    <table id="datagrid" title="检验报告" class="easyui-datagrid" style="width: auto; height: 260px"
        fit='false'
        pagination="true" idfield="id" rownumbers="true"
        fitcolumns="true" singleselect="true" toolbar="#toolbarN"
        striped="false" pagelist="[5,10,15]"
        selectoncheck="true" checkonselect="true" remotesort="true">
        <thead>
            <tr>
                <th field="ck" checkbox="true"></th>
                <th field="Id" width="20" sortable="true" hidden="hidden">Id</th>
                <th field="hospnum" width="100" sortable="true">卡号住院号</th>
                <th field="patname" width="100" sortable="true" hidden="hidden">姓名</th>
                <th field="sex" width="100" sortable="true" hidden="hidden">性别</th>
                <th field="age" width="100" sortable="true">年龄</th>
                <th field="age_month" width="100" sortable="true" hidden="hidden">月</th>
                <th field="ext_mthd" width="100" sortable="true" hidden="hidden">项目总称</th>
                <th field="chinese" width="100" sortable="true">项目名称</th>
                <th field="result" width="100" sortable="true" hidden="hidden">结果</th>
                <th field="units" width="100" sortable="true" hidden="hidden">单位</th>
                <th field="ref_flag" width="100" sortable="true">高低</th>
                <th field="lowvalue" width="100" sortable="true" hidden="hidden">正常低值</th>
                <th field="highvalue" width="100" sortable="true" hidden="hidden">正常高值</th>
                <th field="print_ref" width="100" sortable="true">正常范围</th>
                <th field="check_date" width="100" sortable="true" hidden="hidden">批准时间</th>
                <th field="check_by_name" width="100" sortable="true" hidden="hidden">批准人</th>
                <th field="prnt_order" width="100" sortable="true" hidden="hidden">打印顺序序号</th>
            </tr>
        </thead>
    </table>

    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <div id="toolbarN">
        <table style="width: 100%;">
            <tr>
                <%--<td>
                    <!--查询输入栏-->
                    <table>
                        <tr>
                            <!--Page数据选择模式-->
                            <td>
                                <select onchange="$('#datagrid').datagrid({singleSelect:(this.value==0)})">
                                    <option value="0">单选模式</option>
                                    <option value="1">多选模式</option>
                                </select></td>

                            <!--查询控件-->
                            <td>
                                <!--
                    编码字段<input id="so_字段名称"  class="easyui-combobox" panelHeight="auto"  data-options="valueField:'编码表对应code字段名',textField:'编码表对应name字段名', url:'/common/codeDataHandler.ashx?tabName=编码表名'"/>
                    <input id="date"     class="easyui-datebox" type="text" />
                    -->
                            </td>
                            <!--检索关键字-->
                            <td>
                                <input id="so_keywords" class="easyui-searchbox" data-options="prompt:'请输入查询关键字',searcher:searchData"></input></td>
                        </tr>
                    </table>
                </td>--%>
                <!--button按钮工具栏-->
                <td style="text-align: left;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick="destroy('NormalLisReport_handler.ashx');">删除</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
