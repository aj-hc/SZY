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
namespace RuRo.Web.PatientDiagnose
{
    public partial class Show : Page
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int id=(Convert.ToInt32(strid));
					ShowInfo(id);
				}
			}
		}
		
	private void ShowInfo(int id)
	{
		RuRo.BLL.PatientDiagnose bll=new RuRo.BLL.PatientDiagnose();
		RuRo.Model.PatientDiagnose model=bll.GetModel(id);
		this.lblid.Text=model.id.ToString();
		this.lblCardno.Text=model.Cardno;
		this.lblCsrq00.Text=model.Csrq00;
		this.lblPatientName.Text=model.PatientName;
		this.lblSex.Text=model.Sex;
		this.lblBrithday.Text=model.Brithday.ToString();
		this.lblCardId.Text=model.CardId;
		this.lblTel.Text=model.Tel;
		this.lblRegisterNo.Text=model.RegisterNo;
		this.lblIcd.Text=model.Icd;
		this.lblDiagnose.Text=model.Diagnose;
		this.lblType.Text=model.Type;
		this.lblFlag.Text=model.Flag;
		this.lblDiagnoseDate.Text=model.DiagnoseDate;

	}


    }
}
