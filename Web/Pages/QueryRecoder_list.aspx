<!--���������ɿƷ�EasyUi����������v3.5(build 20140519)��������������,��Ѱ��Զ����Ӱ�Ȩע��,�뱣����Ȩ��Ϣ�����������Ͷ��ɹ��������и��õĽ����뷢�����䣺843330160@qq.com-->
<!--�༭��form��datagrid�б����ݷֱ��������������aspxҳ����-->
<!--datagridҳ��:QueryRecoder_list.aspx-->
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
<!--datagrid��--> 
<table id="QueryRecoderDg" title="��������" class="easyui-datagrid" style="width:auto;height:460px"
            url="" fit='false'
            pagination="false" rownumbers="true"
            fitcolumns="true" singleselect="false" toolbar="#toolbarN"
            striped="false"
            selectoncheck="true" checkonselect="true" remotesort="true">
    <thead>    
			<tr>
			    <th field="ck" checkbox="true"></th>
                <th field="Id"  sortable="true" hidden="true">id</th>
                <th field="Uname" width="100" sortable="true">��ѯ���û�</th>
                <th field="Adddate" width="100" sortable="true">���ʱ��</th>
                <th field="Lastquerydate" width="100" sortable="true">���һ�β�ѯ����</th>
                <th field="Code" width="100" sortable="true">��ѯ�������</th>
                <th field="Codetype" width="100" sortable="true">���������</th>
                <th field="Querytype" width="100" sortable="true">��ѯ����������</th>
                <th field="Queryresult" width="100" sortable="true">��ѯ���</th>
                <th field="Isdel" width="100" sortable="true" hidden="true">isdel</th>
            </tr>
    </thead>
</table>

<!--toolbar��������datagrid��toolbar�Զ�������--> 
    <!--toolbar��������datagrid��toolbar�Զ�������-->
    <div id="toolbarN">
        <table style="width: 100%;">
            <tr>
                <!--button��ť������-->
                <td style="text-align: right;">
                    <%--<a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonInfo" iconCls="icon-search" plain="false" onclick="infoForm();">�鿴</a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonAdd" iconCls="icon-add" plain="false" onclick="newForm();">���</a>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonEdit" iconCls="icon-edit" plain="false" onclick="editForm();">�༭</a>--%>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick="destroy();">ɾ��</a>
                </td>
            </tr>
        </table>
    </div>

    <div id="footer" style="padding: 5px; margin: 10px" data-options="region:'south',">
        <a href="javascript:void(0)" class="easyui-linkbutton" id="submit" style="width: auto" onclick="postPatientInfo()">������Ϣ</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="cancleSubmit" style="width: auto" onclick="CloseWebPage()">ȡ������</a>
    </div>

<script type="text/javascript">
	/*ɾ��ѡ������,������¼PK���������ö���,�ֿ�*/
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
