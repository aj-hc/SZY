<!--基础代码由科发EasyUi代码生成器v3.5(build 20140519)代码生成器生成,免费版自动增加版权注释,请保留版权信息，尊重作者劳动成果，如您有更好的建议请发至邮箱：843330160@qq.com-->
<!--编辑表单form与datagrid列表数据分别放在两个独立的aspx页面中-->
<!--datagrid页面:QueryRecoder_list.aspx-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryRecoder_list.aspx.cs" Inherits="RuRo.QueryRecoder_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
<table id="QueryRecoderDg" title="批量导入" class="easyui-datagrid" style="width:auto;height:460px"
            url="" fit='false'
            pagination="false" rownumbers="true"
            fitcolumns="true" singleselect="false" toolbar="#toolbarN"
            striped="false"
            selectoncheck="true" checkonselect="true" remotesort="true">
    <thead>    
			<tr>
			    <th field="ck" checkbox="true"></th>
                <th field="Id"  sortable="true" hidden="true">id</th>
                <th field="Uname" width="100" sortable="true">查询的用户</th>
                <th field="Adddate" width="100" sortable="true">添加时间</th>
                <th field="Lastquerydate" width="100" sortable="true">最后一次查询日期</th>
                <th field="Code" width="100" sortable="true">查询的条码号</th>
                <th field="Codetype" width="100" sortable="true">条码号类型</th>
                <th field="Querytype" width="100" sortable="true">查询的数据类型</th>
                <th field="Queryresult" width="100" sortable="true">查询结果</th>
                <th field="Isdel" width="100" sortable="true" hidden="true">isdel</th>
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
                    <%--<a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonInfo" iconCls="icon-search" plain="false" onclick="infoForm();">查看</a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonAdd" iconCls="icon-add" plain="false" onclick="newForm();">添加</a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonEdit" iconCls="icon-edit" plain="false" onclick="editForm();">编辑</a>--%>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick="destroy();">删除</a>
                </td>
            </tr>
        </table>
    </div>

    <div id="footer" style="padding: 5px; margin: 10px" data-options="region:'south',">
        <a href="javascript:void(0)" class="easyui-linkbutton" id="submit" style="width: auto" onclick="postPatientInfo()">导入信息</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="cancleSubmit" style="width: auto" onclick="CloseWebPage()">取消导入</a>
    </div>

<script type="text/javascript">
	/*删除选择数据,多条记录PK主键参数用逗号,分开*/
    function destroy() {
        var $QueryRecoderDg = $('#QueryRecoderDg');
        var row = $('#QueryRecoderDg').datagrid('getSelections');
        for (var i = 0; i < row.length; i++) {
            var rowIndex = $QueryRecoderDg.datagrid('getRowIndex', row[i]);
            $QueryRecoderDg.datagrid('deleteRow', rowIndex);
        }
        $("#QueryRecoderDg").datagrid("clearSelections");
    }
</script>

</body>
</html>
