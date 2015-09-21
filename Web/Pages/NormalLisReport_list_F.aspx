<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NormalLisReport_list_F.aspx.cs" Inherits="RuRo.Web.NormalLisReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>临床检测信息</title>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <script src="../include/js/default.js"></script>
    <script type="text/javascript">
        function GetData() {
            $('#NormalLisReportDg').datagrid('loadData', { total: 0, rows: [] });
            var codeType = $('#codeType').combobox('getValue');
            var code = $('#code').textbox('getValue');//获取数据源
            if (/.*[\u4e00-\u9fa5]+.*$/.test(code)) { $.messager.alert('错误', '不能输入中文', 'error'); $('#In_Code').textbox('clear'); return; }
            if (code.length > 14) { $.messager.alert('错误', '条码最高不能超过15位', 'error'); $('#In_Code').textbox('clear'); return; }
            if (isEmptyStr(codeType) || isEmptyStr(code)) { $.messager.alert('提示', '请检查条码类型和条码号', 'error'); }
            var ksrq00 = $('#ksrq00').datebox('getValue');
            var jsrq00 = $('#jsrq00').datebox('getValue');
            ajaxLoading("临床数据查询中。。。");
            $.ajax({
                type: "POST",
                url: "/Sever/NormalLisReport.ashx",
                data: {
                    "mode": "qry2",
                    "codeType": codeType,
                    "code": code,
                    "ksrq00": ksrq00,
                    "jsrq00": jsrq00,
                },
                onLoadError: function () {
                    $("#NormalLisReportDg").datagrid("loaded");
                },
                onLoadSuccess: function () {
                    $("#NormalLisReportDg").datagrid("loaded");
                },
                success: function (response) {
                    ajaxLoadEnd();
                    $('#oldCodeType').textbox('setValue', codeType);
                    $('#oldCode').textbox('setValue', code);
                    if (!response) { $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error'); return; }
                    var obj = $.parseJSON(response);
                    if (obj.Statu == "err") {
                        ShowMsg("临床监测数据：" + obj.Msg);
                        return;
                    }
                    else if (obj.Statu == "ok") {
                        var Qdata = obj.Data;
                        if (Qdata.length > 0)
                        {
                            for (var i = 0; i < Qdata.length; i++) {
                                if (Qdata[i].ref_flag == "1") {
                                    Qdata[i].ref_flag = "高";
                                }
                                else if (Qdata[i].ref_flag == "2") {
                                    Qdata[i].ref_flag = "低";
                                }
                                else if (Qdata[i].ref_flag == "3") {
                                    Qdata[i].ref_flag = "阳性";
                                }
                            }
                            $('#NormalLisReportDg').datagrid("loadData", Qdata);
                        }
                        else {
                            $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error'); return;
                        }
                    }
                }
            });
            ajaxLoadEnd();
        }
    </script>
</head>
<body>
    <div class="easyui-panel">
        <form id="querybycodeform">
            <div id="Div1" runat="server">
                <select id="codeType" class="easyui-combobox" name="codeType" style="width: 150px;" data-options="panelHeight: 'auto'">
                    <option selected="selected" value="0">卡号</option>
                    <option value="1">住院号</option>
                </select>
                <input id="code" class="easyui-textbox" name="code" data-options="prompt:'请输入条码',required:true" />
                开始日期：<input class="easyui-datebox" id="ksrq00" name="ksrq00" style="width: 100px" data-options="required:true" />
                结束日期：<input class="easyui-datebox" id="jsrq00" name="jsrq00" style="width: 100px" />
                <a href="javascript:void(0)" class="easyui-linkbutton" id="btnGet" onclick="GetData();">查询诊断信息</a>
            </div>
        </form>
        <div style="display: none">
            <input class="easyui-textbox" id="oldCode" name="oldCode" />
            <input class="easyui-textbox" id="oldCodeType" name="oldCodeType" />
        </div>
    </div>
    <!--datagrid栏-->
    <table id="NormalLisReportDg" title="临床检测" class="easyui-datagrid" style="width: auto; height: 480px"
        fit='false'
        pagination="false" rownumbers="true"
        fitcolumns="true" singleselect="false" toolbar="#toolbarN"
        striped="false"
        selectoncheck="true" checkonselect="true" remotesort="true">
        <thead>
            <tr>
                <th field="ck" checkbox="true"></th>
                <th field="Id" width="100" hidden="true">id</th>
                <th field="hospnum" width="100">门诊或住院号</th>
                <th field="patname" width="100">姓名</th>
                <th field="Sex" width="100" sortable="true" hidden="true">性别</th>
                <th field="Age" width="100" sortable="true" hidden="true">年龄</th>
                <th field="age_month" width="100" sortable="true" hidden="true">月</th>
                <th field="ext_mthd" width="100" sortable="true">项目总称</th>
                <th field="chinese" width="100" sortable="true">项目名称</th>
                <th field="result" width="100" sortable="true" hidden="true">结果</th>
                <th field="units" width="100" sortable="true" hidden="true">单位</th>
                <th field="ref_flag" width="100" sortable="true">高低</th>
                <th field="lowvalue" width="100" sortable="true" hidden="true">正常低值</th>
                <th field="highvalue" width="100" sortable="true" hidden="true">正常高值</th>
                <th field="print_ref" width="100" sortable="true" hidden="true">正常范围</th>
                <th field="check_date" width="100" sortable="true">批准时间</th>
                <th field="check_by_name" width="100" sortable="true" hidden="true">批准人</th>
                <th field="prnt_order" width="100" sortable="true" hidden="true">打印顺序序号</th>
                <th field="Isdel" width="100" sortable="true" hidden="true">isdel</th>
            </tr>
        </thead>
    </table>
    <div class="h"></div>
    <div id="footer" style="padding: 5px; margin: 5px" data-options="region:'south'">
        <a href="javascript:void(0)" class="easyui-linkbutton" id="submit" style="width: auto" plain="false" onclick="PostNormalLisReport_list();">导入信息</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="cancleSubmit" style="width: auto" plain="false" onclick="CloseWebPage();">取消导入</a>
    </div>
    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <div id="toolbarN">
        <table style="width: 100%;">
            <tr>
                <!--button按钮工具栏-->
                <td style="text-align: right;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick="destroy();">删除</a>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function ShowMsg(msg) {
            $.messager.show({
                title: "提示",
                msg: msg,
                timeout: 2000,
                showType: 'fade'
            });
        }
        /*删除选择数据,多条记录PK主键参数用逗号,分开*/
        function destroy() {
            var $NormalLisReportDg = $('#NormalLisReportDg');
            var row = $('#NormalLisReportDg').datagrid('getSelections');
            for (var i = 0; i < row.length; i++) {
                var rowIndex = $NormalLisReportDg.datagrid('getRowIndex', row[i]);
                $NormalLisReportDg.datagrid('deleteRow', rowIndex);
            }
            $("#NormalLisReportDg").datagrid("clearSelections");
        }
        //POST临床数据到后台
        function PostNormalLisReport_list() {
            var _NormalLisReport = $('#NormalLisReportDg').datagrid('getChecked');
            var code = $('#oldCode').textbox('getValue');
            var codeType = $('#oldCodeType').textbox('getValue');
            var count = Math.random();
            var rowNormalLisReport = JSON.stringify(_NormalLisReport);
            ajaxLoading();
            $.ajax({
                type: 'post',
                dataType: "json",
                url: '/Sever/NormalLisReport.ashx' + "?count" + count,
                data: {
                    "mode": "post",
                    "NormalLis": rowNormalLisReport,
                    "code": code,
                    "codeType": codeType
                },
                success: function (data) {
                    ajaxLoadEnd();
                    if (data.success) {
                        ShowMsg("临床检验信息：" + data.message);
                    }
                    else {
                        $.messager.alert('提示', data.message);
                    }
                }
            });
            ajaxLoadEnd();
        }
    </script>
</body>
</html>
