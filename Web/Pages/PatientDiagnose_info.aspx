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
            <input class="easyui-textbox" name="Id" id="Id"/>
            <input class="easyui-textbox" name="Patientname"  id="Patientname"/>
            <input class="easyui-textbox" name="Sex" id="Sex" />
            <input class="easyui-textbox" id="Brithday" name="Brithday"/>
            <input class="easyui-textbox" id="Tel" name="Tel" />
            <input class="easyui-textbox" id="Cardid" name="Cardid" />
            <input class="easyui-textbox" id="Registerno" name="Registerno" />
        </div>
        <table>
            <tr>
                <td>卡号：</td>
                <td>
                    <input class="easyui-textbox" name="CardId" id="CardId" /></td>
            </tr>
            <tr>
                <td>ICD码：</td>
                <td>
                    <input class="easyui-textbox" name="Icd" id="Icd" data-options="required:false" /></td>

                <td>诊断名称：</td>
                <td>
                    <input class="easyui-textbox" name="Diagnose" id="Diagnose" data-options="required:false" /></td>

                <td>诊断类型：</td>
                <td>
                    <select id="Type" class="easyui-combobox" name="Type" style="width: 200px;" panelHeight="auto">
                        <option value="1">中医疾病</option>
                        <option value="2">中医症候</option>
                        <option value="3">西医主诊断</option>
                        <option value="4">西医其他诊断</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td >诊断类别：</td>
                <td>
                    <select id="Flag" class="easyui-combobox" name="Flag" style="width: 200px;" panelHeight="auto">
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
        var mode = $('#mode').val();
        var pk = $('#pk').val();

        /*编辑或查看状态下控件赋值*/
        if (mode == 'upd' || mode == 'inf') {
            url = 'PatientDiagnose_handler.ashx?mode=inf&pk=' + pk;
            $.post(url, function (data) {
                $('#frmAjax').form('load', data);
            }, 'json');
            $('#linkbuttonClear').linkbutton({ disabled: true });
        }

        /*查看状态下disabled控件*/
        if (mode == 'inf') {
            $('#linkbuttonSave').linkbutton({ disabled: true });
            $('input').attr('disabled', 'disabled');
            $('textarea').attr('disabled', 'disabled');
        }

        if (mode == 'ins') url = 'PatientDiagnose_handler.ashx?mode=ins';
        if (mode == 'upd') url = 'PatientDiagnose_handler.ashx?mode=upd&pk=' + pk;

        /*清空充填*/
        function clearForm() {
            $('#frmAjax').form('clear');
        }

    </script>

</body>
</html>
