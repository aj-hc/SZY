<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpiInfo_info.aspx.cs" Inherits="RuRo.EmpiInfo_info" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <title>基本信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="include/js/DateFmt.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="include/js/jquery.cookie.js"></script>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <link href="include/css/default.css" rel="stylesheet" />
    <script src="../include/js/default.js"></script>
    <script src="include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <!--存储参数属性input控件，判断是新增还是修改-->
    <input value="" id="mode" type="text" style="display: none" runat="server" />
    <input value="" id="pk" type="text" style="display: none" runat="server" />
    <!--编辑数据-->
    <!--编辑容器，按钮固定在底部-->
    <div class="easyui-layout" data-options="fit:true">
        <form id="BaseInfoForm">
            <div style="display: none">
                <input id="SourceType" name="SourceType" />
            </div>
            <table>
                <tr>
                    <td>姓名：</td>
                    <td>
                        <input class="easyui-textbox" name="PatientName" id="PatientName" data-options="required:true" /></td>
                    <td class="name">性别：</td>
                    <td>
                        <input class="easyui-textbox" name="Sex" id="Sex" data-options="required:false" /></td>
                    <td class="name">出生日期：</td>
                    <td>
                        <input class="easyui-datebox" name="Birthday" id="Birthday" data-options="required:false" /></td>
                </tr>
                <tr>
                    <td>身份证号：</td>
                    <td>
                        <input class="easyui-textbox" name="CardId" id="CardId" data-options="required:false" /></td>
                </tr>
            </table>
        </form>
    </div>
    <script type="text/ecmascript">
        function postEmpiInfo() {
            var empiInfo = $("#BaseInfoForm").serializeArray();
            var Tem;
            if (empiInfo) { Tem = JSON.stringify(empiInfo); } else {
                return;
            }
            var code = $('#oldCode').textbox('getValue');
            var codeType = $('#oldCodeType').textbox('getValue');
            //[{ "name": "SourceType", "value": "" }, { "name": "PatientName", "value": "项恺" }, { "name": "Sex", "value": "男" }, { "name": "Birthday", "value": "2015-08-24" }, { "name": "CardId", "value": "110" }]

            $.ajax({
                type: "POST",
                url: "/Sever/EmpiInfo.ashx?mode=post",
                data: {
                    "mode": "post",
                    "empiInfo": Tem,
                    "code":code,
                    "codeType": codeType
                },
                success: function (response) {
                    if (response) {
                        var res = JSON.parse(response);
                        //|| res.Msg.indexOf('should be unique.') > -1
                        if (res.success ) {
                            //调用方法查询数据
                            PostNormalLisReport_list();
                            alert(res.success);
                            postPatientDiagnose_info();
                        }
                    }
                    alert(response.Msg);
                }
            });
        }
    </script>
</body>
</html>
