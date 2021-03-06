<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryRecoder_list.aspx.cs" Inherits="RuRo.QueryRecoder_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <title>QueryRecoder</title>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <link href="../include/css/kfmis.css" rel="stylesheet" />
</head>
<body>
    <!--datagrid栏-->
    <table id="QueryRecoderDg" title="批量导入" class="easyui-datagrid" style="width: auto; height: 460px"
        url="" fit='false'
        pagination="true" rownumbers="true"
        fitcolumns="true" singleselect="false" toolbar="#toolbarN"
        striped="false"
        selectoncheck="true" checkonselect="true" remotesort="true">
        <thead>
            <tr>
                <th field="ck" checkbox="true"></th>
                <th field="Id" sortable="true" hidden="true">id</th>
                <th field="Uname" width="100" sortable="true">查询的用户</th>
                <th field="AddDate" width="100" sortable="true">添加时间</th>
                <th field="LastQueryDate" width="100" sortable="true">最后一次查询日期</th>
                <th field="Code" width="100" sortable="true">查询的条码号</th>
                <th field="CodeType" width="100" sortable="true" hidden="true">条码号类型</th>
                <th field="QueryType" width="100" sortable="true">查询的数据类型</th>
                <th field="QueryResult" width="100" sortable="true">查询结果</th>
                <th field="IsDel" width="100" sortable="true" hidden="true">isdel</th>
            </tr>
        </thead>
    </table>

    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <!--toolbar栏，用于datagrid的toolbar自定义内容-->
    <div id="toolbarN">
        <table style="width: 100%;">
            <tr>
                <!--button按钮工具栏-->
                <td style="text-align: right;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonInfo" iconcls="icon-search" plain="false" onclick="infoForm();">查询</a>
                    <%--<a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonAdd" iconCls="icon-add" plain="false" onclick="newForm();">添加</a>--%>
                    <%--<a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonEdit" iconCls="icon-edit" plain="false" onclick="editForm();">编辑</a>--%>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick="destroy();">删除</a>
                </td>
            </tr>
        </table>
    </div>

    <div id="footer" style="padding: 5px; margin: 10px" data-options="region:'south',">
        <a href="javascript:void(0)" class="easyui-linkbutton" id="submit" style="width: auto" onclick="postQueryRecoder()">导入信息</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="cancleSubmit" style="width: auto" onclick="CloseWebPage()">取消导入</a>
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
        //设置分页
        $(function () {
            $("#QueryRecoderDg").datagrid("getPager").pagination({
                beforePageText: '第',
                afterPageText: '页    共 {pages} 页',
                pageList: [10],
                displayMsg: "显示 {from} 到 {to} 条记录 ,  共 {total} 条记录",
                onSelectPage: function (pageNumber, pageSize) {
                    var page = pageNumber;
                    var myDate = new Date();
                    var year = myDate.getFullYear();       //年
                    var month = myDate.getMonth() + 1;     //月
                    var day = myDate.getDate();
                    var adddate = year + "-" + month + "-" + day
                    $.ajax({
                        type: "POST",
                        url: "/Sever/QueryRecoder_handler.ashx?mode=qryt",
                        data: {
                            "mode": "qryt",
                            "adddate": adddate,
                            "pageNum": pageNumber,
                            "pageSize": pageSize
                        },
                        success: function (data) {
                            var obj = $.parseJSON(data);
                            if (obj.Qdata == "") {
                            }
                            else {
                                var Qdata = $.parseJSON(obj.Qdata);
                                var total = Number(obj.total);
                                for (var i = 0; i < Qdata.length; i++) {
                                    var txtadddate = Qdata[i].AddDate.substring(0, 10);
                                    Qdata[i].AddDate = txtadddate;
                                    var txtqueryDate = Qdata[i].LastQueryDate.substring(0, 10);
                                    Qdata[i].LastQueryDate = txtqueryDate;
                                }
                                $('#QueryRecoderDg').datagrid('loadData', Qdata).datagrid('reload');
                                $("#QueryRecoderDg").datagrid("getPager").pagination({ total: total, pageNumber: page });
                            }
                        }
                    });
                }
            });
        })
        /*删除选择数据,多条记录PK主键参数用逗号,分开*/

        function destroy() {
            var $QueryRecoderDg = $('#QueryRecoderDg');
            var row = $('#QueryRecoderDg').datagrid('getSelections');
            var pk = "";
            for (var i = 0; i < row.length; i++) {
                var rowIndex = $QueryRecoderDg.datagrid('getRowIndex', row[i]);
                pk = pk + row[i].Id + ",";
                $QueryRecoderDg.datagrid('deleteRow', rowIndex);
            }
            //删除数据库数据
            $.ajax({
                type: "POST",
                url: "/Sever/QueryRecoder_handler.ashx?mode=del",
                data: {
                    "mode": "del",
                    "pk": pk
                },
                success: function (data) { $.messager.alert('提示', data); return; }
            });
            $("#QueryRecoderDg").datagrid("clearSelections");
        }
        //查询数据
        function infoForm() {
            var myDate = new Date();
            var year = myDate.getFullYear();       //年
            var month = myDate.getMonth() + 1;     //月
            var day = myDate.getDate();
            var adddate = year + "-" + month + "-" + day
            $.ajax({
                type: "POST",
                url: "/Sever/QueryRecoder_handler.ashx?mode=qryt",
                data: {
                    "mode": "qryt",
                    "adddate": adddate,
                    "pageNum": 1,
                    "pageSize": 10
                },
                success: function (data) {
                    var obj = $.parseJSON(data);
                    if (obj.Qdata == "") { $.messager.alert('错误', '查询不到数据', 'error'); }
                    else
                    {
                        var Qdata = $.parseJSON(obj.Qdata);
                        var total = Number(obj.total);
                        for (var i = 0; i < Qdata.length; i++) {
                            var txtadddate = Qdata[i].AddDate.substring(0, 10);
                            Qdata[i].AddDate = txtadddate;
                            var txtqueryDate = "";
                            if (Qdata[i].LastQueryDate != null) { txtqueryDate = Qdata[i].LastQueryDate.substring(0, 10); }
                            Qdata[i].LastQueryDate = txtqueryDate;
                        }
                        $('#QueryRecoderDg').datagrid('loadData', Qdata).datagrid('reload');
                        $("#QueryRecoderDg").datagrid("getPager").pagination({ total: total });
                    }
                }
            });
        }
        //上传数据
        function postQueryRecoder() {
            var _QueryRecoder = $('#QueryRecoderDg').datagrid('getChecked');
            if (_QueryRecoder.length <= 0) { $.messager.alert('提示', '未选择导入信息或导入信息为空', 'error'); return; }
            var count = Math.random();
            var rowQueryRecoder = JSON.stringify(_QueryRecoder);
            ajaxLoading();
            $.ajax({
                type: 'post',
                dataType: "json",
                url: '/Sever/QueryRecoder_handler.ashx' + "?count" + count,
                data: {
                    "mode": "post",
                    //"count": count,
                    "Recoder": rowQueryRecoder
                },
                success: function (data) {
                    ajaxLoadEnd();
                    ShowMsg(data.message);
                    return;
                }
            });
            ajaxLoadEnd();
        }
        //采用jquery easyui loading css效果
        function ajaxLoading() {
            $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", height: $(window).height() }).appendTo("body");
            $("<div class=\"datagrid-mask-msg\"></div>").html("正在处理，请稍候。。。").appendTo("body").css({ display: "block", left: ($(document.body).outerWidth(true) - 190) / 2, top: ($(window).height() - 45) / 2 });
        }
        function ajaxLoadEnd() {
            $(".datagrid-mask").remove();
            $(".datagrid-mask-msg").remove();
        }
    </script>
</body>
</html>