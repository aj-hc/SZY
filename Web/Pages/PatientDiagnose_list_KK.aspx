<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientDiagnose_list_KK.aspx.cs" Inherits="RuRo.Web.PatientDiagnose" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>诊断信息</title>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <script src="../include/js/default.js"></script>
    <script type="text/javascript">
        function GetData() {
            $('#PatientDiagnoseDg').datagrid('loadData', { total: 0, rows: [] });
            var codeType = $('#codeType').combobox('getValue');
            var code = $('#code').textbox('getValue');//获取数据源
            if (/.*[\u4e00-\u9fa5]+.*$/.test(code)) { $.messager.alert('错误', '不能输入中文', 'error'); $('#In_Code').textbox('clear'); return; }
            if (code.length > 14) { $.messager.alert('错误', '条码最高不能超过15位', 'error'); $('#In_Code').textbox('clear'); return; }
            if (isEmptyStr(codeType) || isEmptyStr(code)) { $.messager.alert('提示', '请检查条码类型和条码号', 'error'); }
            var ksrq00 = $('#ksrq00').datebox('getValue');
            var jsrq00 = $('#jsrq00').datebox('getValue');
            ajaxLoading("诊断数据查询中。。。");
            $.ajax({
                type: "POST",
                url: "/Sever/PatientDiagnose.ashx",
                data: {
                    "mode": "qry2",
                    "codeType": codeType,
                    "code": code,
                    "ksrq00": ksrq00,
                    "jsrq00": jsrq00,
                },
                onLoadError: function () {
                    $("#PatientDiagnoseDg").datagrid("loaded");
                },
                onLoadSuccess: function () {
                    $("#PatientDiagnoseDg").datagrid("loaded");
                },
                success: function (response) {
                    ajaxLoadEnd();
                    $('#oldCodeType').textbox('setValue', codeType);
                    $('#oldCode').textbox('setValue', code);
                    if (!response) { $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error'); return; }
                    else {
                        var obj = $.parseJSON(response);
                        if (obj.Statu == "err") {
                            ShowMsg("诊断数据：" + obj.Msg);
                            return;
                        }
                        else if (obj.Statu == "ok") {
                            var Qdata = obj.Data;
                            if (Qdata.length > 0) {
                                for (var i = 0; i < Qdata.length; i++) {
                                    if (Qdata[i].Type == "1") {
                                        Qdata[i].Type = "中医疾病";
                                    }
                                    else if (Qdata[i].Type == "2") {
                                        Qdata[i].Type = "中医症候";
                                    }
                                    else if (Qdata[i].Type == "3") {
                                        Qdata[i].Type = "西医主诊断";
                                    }
                                    else if (Qdata[i].Type == "4") {
                                        Qdata[i].Type = "西医其他诊断";
                                    }
                                    if (Qdata[i].Flag == "1") {
                                        Qdata[i].Flag = "西医诊断";
                                    }
                                    else if (Qdata[i].Flag == "0") {
                                        Qdata[i].Flag = "中医诊断";
                                    }
                                }
                                $('#PatientDiagnoseDg').datagrid("loadData", Qdata);
                            }
                            else
                            {
                                $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error'); return;
                            }
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
        <%-- <div>
            <ul>
                <li><b>查询诊断信息</b></li>
            </ul>
        </div>--%>
        <form id="querybycodeform">
            <div id="Div1" runat="server">
                <select id="codeType" class="easyui-combobox" name="codeType" style="width: 150px;" data-options="panelHeight: 'auto'">
                    <option selected="selected" value="0">卡号</option>
                    <option value="1">住院号</option>
                    <%--//0 门诊 1住院--%>
                </select>
                <input class="easyui-textbox" id="code" name="code" data-options="prompt:'请输入条码',required:true" />
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

    <table id="PatientDiagnoseDg" title="诊断信息" class="easyui-datagrid" style="width: auto; height: 480px"
        fit='false'
        pagination="false" rownumbers="true"
        fitcolumns="true" singleselect="false" toolbar="#toolbarP"
        striped="false"
        selectoncheck="true" checkonselect="true" remotesort="true">
        <thead>
            <tr>
                <th field="ck" checkbox="true"></th>
                <th field="id" width="100" sortable="true" hidden="true">id</th>
                <th field="Cardno" width="100" sortable="false">卡号</th>
                <th field="Csrq00" width="100" sortable="true">查询日期</th>
                <th field="PatientName" width="100" sortable="false">姓名</th>
                <th field="Sex" width="100" sortable="false" hidden="true">性别</th>
                <th field="Brithday" width="100" sortable="false" hidden="true">出生日期</th>
                <th field="Cardid" width="100" sortable="false" hidden="true">身份证号</th>
                <th field="Tel" width="100" sortable="false" hidden="true">tel</th>
                <th field="Registerno" width="100" sortable="true" hidden="true">registerno</th>
                <th field="Icd" width="100" sortable="true">ICD码</th>
                <th field="Diagnose" width="100" sortable="true">诊断名称</th>
                <th field="Type" width="100" sortable="true">诊断类型</th>
                <th field="Flag" width="100" sortable="true">诊断类别</th>
                <th field="DiagnoseDate" width="100" sortable="true">诊断日期</th>
                <th field="Isdel" width="100" sortable="true" hidden="true">isdel</th>
            </tr>
        </thead>
    </table>
    <div class="h"></div>
    <div id="footer" style="padding: 5px; margin: 5px" data-options="region:'south'">
        <a href="javascript:void(0)" class="easyui-linkbutton" id="submit" style="width: auto" plain="false" onclick="PostPatientDiagnose_list();">导入信息</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="cancleSubmit" style="width: auto" plain="false" onclick="CloseWebPage();">取消导入</a>
    </div>
    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <div id="toolbarP">
        <table style="width: 100%;">
            <tr>
                <!--button按钮工具栏-->
                <td style="text-align: right;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="toolbarPDel" iconcls="icon-cancel" plain="false" onclick="destroy();">删除</a>
                </td>
            </tr>
        </table>
    </div>

    <!--diaglog窗口，用于编辑数据-->
    <div id="dlg" class="easyui-dialog" closed="true"></div>

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
            var $PatientDiagnoseDg = $('#PatientDiagnoseDg');
            var row = $('#PatientDiagnoseDg').datagrid('getSelections');
            for (var i = 0; i < row.length; i++) {
                var rowIndex = $PatientDiagnoseDg.datagrid('getRowIndex', row[i]);
                $PatientDiagnoseDg.datagrid('deleteRow', rowIndex);
            }
            $("#PatientDiagnoseDg").datagrid("clearSelections");
        }

        //POST临床数据到后台
        function PostPatientDiagnose_list() {
            var _PatientDiagnose = $('#PatientDiagnoseDg').datagrid('getChecked');
            //if (_PatientDiagnose.length <= 0) {
            //    $.messager.alert('提示', '未选择诊断信息或诊断信息为空', 'error'); return;
            //}
            var code = $('#oldCode').textbox('getValue');
            var codeType = $('#oldCodeType').textbox('getValue');
            var count = Math.random();
            var rowPatientDiagnose = JSON.stringify(_PatientDiagnose);
            ajaxLoading();
            $.ajax({
                type: 'post',
                dataType: "json",
                url: '/Sever/PatientDiagnose.ashx' + "?count" + count,
                data: {
                    "mode": "post",
                    //"count": count,
                    "PatientDiagnoseLis": rowPatientDiagnose,
                    "code": code,
                    "codeType": codeType
                },
                success: function (data) {
                    ajaxLoadEnd();
                    if (data.success) {
                        ShowMsg("诊断信息：" + data.message);
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
