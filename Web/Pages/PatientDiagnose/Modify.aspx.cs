using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using RuRo.Common;
using LTP.Accounts.Bus;
namespace RuRo.Web.PatientDiagnose
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int id=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(id);
				}
			}
		}
			
	private void ShowInfo(int id)
	{
		RuRo.BLL.PatientDiagnose bll=new RuRo.BLL.PatientDiagnose();
		RuRo.Model.PatientDiagnose model=bll.GetModel(id);
		this.lblid.Text=model.id.ToString();
		this.txtCardno.Text=model.Cardno;
		this.txtCsrq00.Text=model.Csrq00;
		this.txtPatientName.Text=model.PatientName;
		this.txtSex.Text=model.Sex;
		this.txtBrithday.Text=model.Brithday.ToString();
		this.txtCardId.Text=model.CardId;
		this.txtTel.Text=model.Tel;
		this.txtRegisterNo.Text=model.RegisterNo;
		this.txtIcd.Text=model.Icd;
		this.txtDiagnose.Text=model.Diagnose;
		this.txtType.Text=model.Type;
		this.txtFlag.Text=model.Flag;
		this.txtDiagnoseDate.Text=model.DiagnoseDate;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtCardno.Text.Trim().Length==0)
			{
				strErr+="卡号不能为空！\\n";	
			}
			if(this.txtCsrq00.Text.Trim().Length==0)
			{
				strErr+="查询日期不能为空！\\n";	
			}
			if(this.txtPatientName.Text.Trim().Length==0)
			{
				strErr+="姓名不能为空！\\n";	
			}
			if(this.txtSex.Text.Trim().Length==0)
			{
				strErr+="性别不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtBrithday.Text))
			{
				strErr+="出生日期格式错误！\\n";	
			}
			if(this.txtCardId.Text.Trim().Length==0)
			{
				strErr+="身份证号不能为空！\\n";	
			}
			if(this.txtTel.Text.Trim().Length==0)
			{
				strErr+="Tel不能为空！\\n";	
			}
			if(this.txtRegisterNo.Text.Trim().Length==0)
			{
				strErr+="RegisterNo不能为空！\\n";	
			}
			if(this.txtIcd.Text.Trim().Length==0)
			{
				strErr+="ICD码不能为空！\\n";	
			}
			if(this.txtDiagnose.Text.Trim().Length==0)
			{
				strErr+="诊断名称不能为空！\\n";	
			}
			if(this.txtType.Text.Trim().Length==0)
			{
				strErr+="诊断类型:1：中医疾病 2：中不能为空！\\n";	
			}
			if(this.txtFlag.Text.Trim().Length==0)
			{
				strErr+="诊断类别:1：西医诊断 2 中不能为空！\\n";	
			}
			if(this.txtDiagnoseDate.Text.Trim().Length==0)
			{
				strErr+="诊断日期不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int id=int.Parse(this.lblid.Text);
			string Cardno=this.txtCardno.Text;
			string Csrq00=this.txtCsrq00.Text;
			string PatientName=this.txtPatientName.Text;
			string Sex=this.txtSex.Text;
			DateTime Brithday=DateTime.Parse(this.txtBrithday.Text);
			string CardId=this.txtCardId.Text;
			string Tel=this.txtTel.Text;
			string RegisterNo=this.txtRegisterNo.Text;
			string Icd=this.txtIcd.Text;
			string Diagnose=this.txtDiagnose.Text;
			string Type=this.txtType.Text;
			string Flag=this.txtFlag.Text;
			string DiagnoseDate=this.txtDiagnoseDate.Text;


			RuRo.Model.PatientDiagnose model=new RuRo.Model.PatientDiagnose();
			model.id=id;
			model.Cardno=Cardno;
			model.Csrq00=Csrq00;
			model.PatientName=PatientName;
			model.Sex=Sex;
			model.Brithday=Brithday;
			model.CardId=CardId;
			model.Tel=Tel;
			model.RegisterNo=RegisterNo;
			model.Icd=Icd;
			model.Diagnose=Diagnose;
			model.Type=Type;
			model.Flag=Flag;
			model.DiagnoseDate=DiagnoseDate;

			RuRo.BLL.PatientDiagnose bll=new RuRo.BLL.PatientDiagnose();
			bll.Update(model);
			RuRo.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
