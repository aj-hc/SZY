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
                <th field="Id" width="100" sortable="true">id</th>
                <th field="Uname" width="100" sortable="true">��ѯ���û�</th>
                <th field="Adddate" width="100" sortable="true">adddate</th>
                <th field="Lastquerydate" width="100" sortable="true">���һ�β�ѯ����</th>
                <th field="Code" width="100" sortable="true">��ѯ�������</th>
                <th field="Codetype" width="100" sortable="true">���������</th>
                <th field="Querytype" width="100" sortable="true">��ѯ����������</th>
                <th field="Queryresult" width="100" sortable="true">��ѯ���</th>
                <th field="Isdel" width="100" sortable="true">isdel</th>
            </tr>
    </thead>
</table>

<!--toolbar��������datagrid��toolbar�Զ�������--> 
<div id="toolbar">
<table style="width:100%;">
<tr>
    <td>
        <!--��ѯ������--> 
        <table>
            <tr>
               <!--Page����ѡ��ģʽ-->  
                <td><select onchange="$('#datagrid').datagrid({singleSelect:(this.value==0)})"><option value="0">��ѡģʽ</option><option value="1">��ѡģʽ</option></select></td>

                <!--��ѯ�ؼ�-->
                <td>
                    <!--
                    �����ֶ�<input id="so_�ֶ�����"  class="easyui-combobox" panelHeight="auto"  data-options="valueField:'������Ӧcode�ֶ���',textField:'������Ӧname�ֶ���', url:'/common/codeDataHandler.ashx?tabName=�������'"/>
                    <input id="date"     class="easyui-datebox" type="text" />
                    -->
                </td>
                <!--�����ؼ���-->
                <td><input id="so_keywords"  class="easyui-searchbox" data-options="prompt:'�������ѯ�ؼ���',searcher:searchData" ></input></td>
            </tr>
        </table> 
    </td>
    <!--button��ť������--> 
    <td  style="text-align:right;">
        <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonInfo" iconCls="icon-search" plain="false" onclick="infoForm();">�鿴</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonAdd" iconCls="icon-add" plain="false" onclick="newForm();">���</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonEdit" iconCls="icon-edit" plain="false" onclick="editForm();">�༭</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" id="linkbuttonDel" iconCls="icon-cancel" plain="false" onclick="destroy();">ɾ��</a>
    </td>
</tr>
</table>  
</div>

<!--diaglog���ڣ����ڱ༭����--> 
<div id="dlg"  class="easyui-dialog" closed="true"></div>

<script type="text/javascript">
	var url;
	/*������*/
	function newForm(){
		$('#dlg').dialog({    
            title: 'QueryRecoder-�������',    
            width: 650, 
            height: 450,    
            closed: false,  
            cache: false,    
            href: 'QueryRecoder_info.aspx?mode=ins'
        });     
	}

	/*�鿴����*/
	function infoForm(){
	    var rows = $('#QueryRecoderDg').datagrid('getSelections');
	    if(rows.length>0){
	       if(rows.length==1){
	           var row = $('#QueryRecoderDg').datagrid('getSelected');
				$('#dlg').dialog({    
                    title: 'QueryRecoder-�鿴����',    
                    width: 650,    
                    height: 450,    
                    closed: false,    
                    cache: true,    
                    href: 'QueryRecoder_info.aspx?mode=inf&pk='+ row.id
                });     
			}else{ 
				$.messager.alert('����', '�鿴����ֻ��ѡ��һ������', 'warning'); 
			}  
	    }else{
	         $.messager.alert('����', '��ѡ������', 'warning');
	    }
	}

	/*�޸�����*/
	function editForm(){
		var rows = $('#datagrid').datagrid('getSelections');
	    if(rows.length>0){
	       if(rows.length==1){
				var row = $('#datagrid').datagrid('getSelected');
				$('#dlg').dialog({    
                    title: 'QueryRecoder-�޸�����',    
                    width: 650,    
                    height: 450,    
                    closed: false,    
                    cache: true,    
                    href: 'QueryRecoder_info.aspx?mode=upd&pk='+ row.id
                });     
			}else{ 
				$.messager.alert('����', '�޸Ĳ���ֻ��ѡ��һ������', 'warning'); 
			}  
	    }else{
	         $.messager.alert('����', '��ѡ������', 'warning');
	    }
	}

	/*ɾ��ѡ������,������¼PK���������ö���,�ֿ�*/
	function destroy(){
		var rows = $('#datagrid').datagrid('getSelections');
		if(rows.length>0){ 
				var pkSelect="";
				for(var i=0;i<rows.length;i++){
					row = rows[i];
					if(i==0){
						pkSelect+= row.id;
					}else{
						pkSelect+=','+row.id;
					}
				}
				$.messager.confirm('��ʾ','�Ƿ�ȷ��ɾ�����ݣ�',function(r){
				if (r){
						$.post('QueryRecoder_handler.ashx?mode=del&pk='+pkSelect,function(result){
							if (result.success){
								$.messager.alert('��ʾ', result.msg, 'info',function(){
									$('#datagrid').datagrid('reload');    //���¼���������
								}); 
							} else {
								$.messager.alert('����', result.msg, 'warning');
							}
						},'json');
					}
				}); 
		}else{
			 $.messager.alert('����', '��ѡ������', 'warning');
		}
	}

	/*��ѯ������������*/
	function getSearchParm(){
		//������������׷�Ӳ�������
		/*comboboxֵ��ȡ����,��������������ѯ�������*/
		//var v_so_�ֶ����� = $('#so_�ֶ�����').combobox('getValue');
		var v_parm
		var v_so_keywords = $('#so_keywords').searchbox('getValue');
		v_parm = 'so_keywords='+escape(v_so_keywords);
		return v_parm;
	}

	/*��ѯ����*/
	function searchData(){
		/*��˵���Excel����������������datagrid����load�������ز�����ֱ����URL���ݲ���*/
		var Parm = getSearchParm();//��ò�ѯ����������������URL���ݲ�ѯ����
		var QryUrl='QueryRecoder_handler.ashx?mode=qry&'+Parm; 
		$('#datagrid').datagrid({url:QryUrl});
	}

    ///*�ر�dialog���¼���datagrid����*/
    //$('#dlg').dialog({onClose:function(){ 
    //    $('#datagrid').datagrid('reload'); //���¼���������
    //}});

</script>

</body>
</html>
