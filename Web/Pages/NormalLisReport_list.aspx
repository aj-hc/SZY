<!--�༭��form��datagrid�б����ݷֱ��������������aspxҳ����-->
<!--datagridҳ��:NormalLisReport_list.aspx-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NormalLisReport_list.aspx.cs" Inherits="RuRo.NormalLisReport_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head">
    <title>NormalLisReport</title>
    <script src="../include/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script src="../include/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../include/js/jquery.cookie.js"></script>
    <link href="../include/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <link href="../include/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" />
    <script src="../include/jquery-easyui-1.4.3/locale/easyui-lang-zh_CN.js"></script>
    <script src="../include/js/DateFmt.js"></script>
    <script src="../include/js/sy_func.js"></script>
    <script src="../include/js/DgGRUD.js"></script>
</head>
<body>
    <form id="NormalLisReportForm">
        <table id="datagridN" title="�����Ϣ" class="easyui-datagrid" style="width: auto; height: 260px"
            fit='false'
            pagination="false" rownumbers="true"
            fitcolumns="true" singleselect="false" toolbar="#toolbarN"
            striped="false" <%-- pageList="[5,10,15]"--%>
            selectoncheck="true" checkonselect="true" remotesort="false">
            <thead>
                <tr>
                    <th field="ck" checkbox="true"></th>
                    <th field="id" width="100" sortable="true" hidden="hidden">id</th>
                    <th field="hospnum" width="100" sortable="true" hidden="hidden">��������š�סԺ��</th>
                    <th field="patname" width="100" sortable="true">����</th>
                    <th field="sex" width="100" sortable="true">�Ա�</th>
                    <th field="age" width="100" sortable="true">����</th>
                    <th field="age_month" width="100" sortable="true" hidden="hidden">��</th>
                    <th field="ext_mthd" width="100" sortable="true">��Ŀ�ܳ�</th>
                    <th field="chinese" width="100" sortable="true">��Ŀ����</th>
                    <th field="result" width="100" sortable="true" hidden="hidden">���</th>
                    <th field="units" width="100" sortable="true" hidden="hidden">��λ</th>
                    <th field="ref_flag" width="100" sortable="true">�ߵ�</th>
                    <th field="lowvalue" width="100" sortable="true" hidden="hidden">������ֵ</th>
                    <th field="highvalue" width="100" sortable="true" hidden="hidden">������ֵ</th>
                    <th field="print_ref" width="100" sortable="true" hidden="hidden">������Χ</th>
                    <th field="check_date" width="100" sortable="true">��׼ʱ��</th>
                    <th field="check_by_name" width="100" sortable="true" hidden="hidden">��׼��</th>
                    <th field="prnt_order" width="100" sortable="true" hidden="hidden">��ӡ˳�����</th>
                </tr>
            </thead>
        </table>
    </form>

    <!--toolbar��������datagrid��toolbar�Զ�������-->
    <div id="toolbarN">
        <table style="width: 100%;">
            <tr>
                <!--button��ť������-->
                <td style="text-align: right;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconcls="icon-cancel" plain="false" onclick=" destroy($('#datagridN'), '/Sever/NormalLisReport.ashx');">ɾ��</a>
                </td>
            </tr>
        </table>
    </div>

    <!--diaglog���ڣ����ڱ༭����-->
    <div id="dlg" class="easyui-dialog" closed="true"></div>
</body>
</html>
