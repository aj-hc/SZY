<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientDiagnose_list.aspx.cs" Inherits="RuRo.PatientDiagnose_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <title>诊断信息</title>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <!--datagrid栏-->
    <table id="PatientDiagnoseDg" title="诊断信息" class="easyui-datagrid" style="width: auto; height: 180px"
        fit='false'
        pagination="false" rownumbers="true"
        fitcolumns="true" singleselect="false" toolbar="#toolbarP"
        striped="false"
        selectoncheck="true" checkonselect="true" remotesort="true">
        <thead>
            <tr>
                <th field="ck" checkbox="true"></th>
                <th field="id" width="100" sortable="true" hidden="true">id</th>
                <th field="cardno" width="100" sortable="false">卡号</th>
                <th field="csrq00" width="100" sortable="true">查询日期</th>
                <th field="patientname" width="100" sortable="false">姓名</th>
                <th field="sex" width="100" sortable="false" hidden="true">性别</th>
                <th field="brithday" width="100" sortable="false" hidden="true">出生日期</th>
                <th field="cardid" width="100" sortable="false" hidden="true">身份证号</th>
                <th field="tel" width="100" sortable="false" hidden="true">tel</th>
                <th field="registerno" width="100" sortable="true" hidden="true">registerno</th>
                <th field="icd" width="100" sortable="true">ICD码</th>
                <th field="diagnose" width="100" sortable="true">诊断名称</th>
                <th field="type" width="100" sortable="true">诊断类型</th>
                <th field="flag" width="100" sortable="true">诊断类别</th>
                <th field="diagnosedate" width="100" sortable="true">诊断日期</th>
                <th field="isdel" width="100" sortable="true" hidden="true">isdel</th>
            </tr>
        </thead>
    </table>

    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <div id="toolbarP">
        <table style="width: 100%;">
            <tr>
                <!--button按钮工具栏-->
                <td style="text-align: right;">
                    <%--<a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonInfo" iconCls="icon-search" plain="false" onclick="infoForm();">查看</a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonAdd" iconCls="icon-add" plain="false" onclick="newForm();">添加</a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonEdit" iconCls="icon-edit" plain="false" onclick="editForm();">编辑</a>--%>
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
                        ShowMsg("诊断信息："+data.message);
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