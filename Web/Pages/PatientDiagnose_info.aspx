<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientDiagnose_info.aspx.cs" Inherits="RuRo.PatientDiagnose_info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <title>诊断信息</title>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <link href="../include/css/kfmis.css" rel="stylesheet" />
</head>
<body>
    <!--存储参数属性input控件，判断是新增还是修改-->
    <input value="" id="mode" type="text" style="display: none" runat="server" />
    <input value="" id="pk" type="text" style="display: none" runat="server" />
    <form id="PatientDiagnoseForm" method="post">
        <div id="hidden" style="display: none">
            <input class="easyui-textbox" name="Id" id="Id" />
            <input class="easyui-textbox" name="PatientName" id="PatientName" />
            <input class="easyui-textbox" name="Sex" id="Sex" />
            <input class="easyui-textbox" id="Brithday" name="Brithday" />
            <input class="easyui-textbox" id="Tel" name="Tel" />
            <input class="easyui-textbox" id="CardId" name="CardId" />
            <input class="easyui-textbox" id="RegisterNo" name="RegisterNo" />
            <input class="easyui-textbox" id="Cardno" name="Cardno" />
        </div>
        <table>
            <tr>
                <td>ICD码：</td>
                <td>
                    <input class="easyui-textbox" name="Icd" id="Icd" data-options="required:false" /></td>
                <td>诊断名称：</td>
                <td>
                    <input class="easyui-textbox" name="Diagnose" id="Diagnose" data-options="required:false" /></td>

                <td>诊断类型：</td>
                <td>
                    <select id="Type" class="easyui-combobox" name="Type" style="width: 200px;" panelheight="auto">
                        <option value="1">中医疾病</option>
                        <option value="2">中医症候</option>
                        <option value="3">西医主诊断</option>
                        <option value="4">西医其他诊断</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>诊断类别：</td>
                <td>
                    <select id="Flag" class="easyui-combobox" name="Flag" style="width: 200px;" panelheight="auto">
                        <option value="1">西医诊断</option>
                        <option value="2">中医诊断</option>
                    </select>
                </td>
                <td class="name">诊断日期：</td>
                <td>
                    <input class="easyui-datebox" name="DiagnoseDate" id="DiagnoseDate" data-options="required:false" /></td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
        function postPatientDiagnose_info() {
            var formData = $("#PatientDiagnoseForm").serializeArray();
            var Tem;
            if (formData) { Tem = JSON.stringify(formData); } else {
                return;
            }
            var code = $('#oldCode').textbox('getValue');
            var codeType = $('#oldCodeType').textbox('getValue');
            ajaxLoading();
            $.ajax({
                type: "POST",
                url: "/Sever/PatientDiagnose.ashx?mode=post",
                data: {
                    "mode": "post",
                    "formData": Tem,
                    "code": code,
                    "codeType": codeType
                },
                success: function (response) {
                    ajaxLoadEnd();
                    var obj = $.parseJSON(response);
                    if (obj.success) {
                    } else {
                        $.messager.alert('提示', obj.message, 'error'); return;
                    }
                }
            });
            ajaxLoadEnd();
        }
    </script>
</body>
</html>