<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpiInfo_info.aspx.cs" Inherits="RuRo.EmpiInfo_info" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <title>基本信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="include/js/DateFmt.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/js/jquery.cookie.js"></script>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <link href="include/css/default.css" rel="stylesheet" />
    <script src="../include/js/default.js"></script>
    <script src="include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <script src="../include/js/sy_func.js"></script>
    <script src="../include/js/page.js"></script>
</head>
<body>
    <div class="easyui-panel" style="height: 80px;">
        <div id="cca"><b style="font-size: 20px;">查找患者</b></div>
        <br />
        <form id="querybycodeform">
            <div runat="server">
                查找方式：
                <input id="codeType" class="easyui-combobox" name="codeType" style="width: 200px;" data-options="prompt:'请选择条码类型',required:true" />
                <input id="code" class="easyui-textbox" name="code" data-options="prompt:'请输入条码',required:true" />
                <a href="javascript:void(0)" class="easyui-linkbutton" id="btnGet" name="btnGet" plain="false" onclick="bycodequery();">查询患者信息</a>
            </div>
        </form>
        <div style="display: none">
            <input class="easyui-textbox" id="oldCode" name="oldCode" />
            <input class="easyui-textbox" id="oldCodeType" name="oldCodeType" />
        </div>
    </div>
    <div class="h"></div>
    <!--存储参数属性input控件，判断是新增还是修改-->
    <input value="" id="mode" type="text" style="display: none" runat="server" />
    <input value="" id="pk" type="text" style="display: none" runat="server" />
    <!--编辑数据-->
    <!--编辑容器，按钮固定在底部-->
    <div class="easyui-layout" data-options="fit:true">
        <div></div>
        <form id="BaseInfoForm">
            <div style="display: none">
                <input id="SourceType" name="SourceType" />
            </div>
            <ul>
                <li style="list-style-type: none;">姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：<input class="easyui-textbox" name="PatientName" id="PatientName" data-options="required:true" /></li>
                <br />
                <li style="list-style-type: none;">性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别：<input class="easyui-textbox" name="Sex" id="Sex" data-options="required:false" /></li>
                <br />
                <li style="list-style-type: none;">出生日期：<input class="easyui-datebox" name="Birthday" id="Birthday" data-options="required:false" /></li>
                <br />
                <li style="list-style-type: none;">身份证号：<input class="easyui-textbox" name="CardId" id="CardId" data-options="required:false" /></li>
                <BR />
                <li style="list-style-type: none;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="submit" style="width: auto;height:50px;" plain="false" onclick="postEmpiInfo();">导入信息</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="cancleSubmit" style="width: auto;height:50px;" plain="false" onclick="clearPage();">取消导入</a>
                </li>
            </ul>
            <%--<table>
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
            </table>--%>
        </form>
        <div id="footer" style="padding: 5px; margin: 5px" data-options="region:'south'">
            
            
        </div>
    </div>

    <script type="text/ecmascript">
        //绑定数据
        function bycodequery() {
            clearForm();
            var codeType = $('#codeType').combobox('getValue');
            var code = $('#code').textbox('getValue');//获取数据源
            if (/.*[\u4e00-\u9fa5]+.*$/.test(code)) { $.messager.alert('错误', '不能输入中文', 'error'); $('#In_Code').textbox('clear'); return; }
            if (code.length > 14) { $.messager.alert('错误', '条码最高不能超过15位', 'error'); $('#In_Code').textbox('clear'); return; }
            if (isEmptyStr(codeType) || isEmptyStr(code)) { $.messager.alert('提示', '请检查条码类型和条码号', 'error'); }
            else {
                ajaxLoading();
                $.ajax({
                    type: 'GET',
                    url: '/Sever/EmpiInfo.ashx?mode=qry&codeType=' + codeType + '&code=' + code,
                    onSubmit: function () { },
                    success: function (data) {
                        ajaxLoadEnd();
                        $('#code').textbox('setValue', '');
                        clearForm();
                        if (!data) { $.messager.alert('提示', '查询不到数据,请检查数据是否存在！', 'error'); }
                        else {
                            //测试代码
                            var obj = $.parseJSON(data);
                            if (obj.Statu == "err") {
                                ShowMsg(obj.Msg);
                                return;
                            }
                            else {
                                var Qdata = obj.Data;//此处需要大写Data 不清楚原因
                                if (Qdata.Sex == "M") { Qdata.Sex = "男"; }
                                else if (Qdata.Sex == "F") { Qdata.Sex = "女"; }
                                $("#BaseInfoForm").form("load", Qdata);
                                $('#oldCode').textbox('setValue', code);
                                $('#oldCodeType').textbox('setValue', codeType);
                            }
                        }
                    }
                });
                ajaxLoadEnd();
            }
        }
        //上传数据
        function postEmpiInfo() {
            var username = $.cookie('username');
            var kk = $("#BaseInfoForm").form('validate');
            if (kk) {
                var empiInfo = $("#BaseInfoForm").serializeArray();
                var Tem;
                if (empiInfo) { Tem = JSON.stringify(empiInfo); } else {
                    return;
                }
                var code = $('#oldCode').textbox('getValue');
                var codeType = $('#oldCodeType').textbox('getValue');
                ajaxLoading();
                $.ajax({
                    type: "POST",
                    url: "/Sever/EmpiInfo.ashx?mode=post",
                    data: {
                        "mode": "post",
                        "empiInfo": Tem,
                        "code": code,
                        "codeType": codeType
                    },
                    success: function (response) {
                        ajaxLoadEnd();
                        if (response) {
                            var res = JSON.parse(response);
                            //alert(response);
                            if (res.success || res.message.indexOf('should be unique.') > -1) {
                                ShowMsg("患者信息：" + "导入成功" + res.message);
                            }
                        }
                        else { $.messager.alert('提示', '查询不到样品源', 'error'); }
                    }
                });
                ajaxLoadEnd();
            }
        }
        //取消导入 清空所有
        function clearPage()
        {
            $('#BaseInfoForm').form('clear');
        }

    </script>
</body>
</html>
