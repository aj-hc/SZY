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
namespace RuRo.Web.EmpiInfo
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
					int Id=(Convert.ToInt32(strid));
					ShowInfo(Id);
				}
			}
		}
		
	private void ShowInfo(int Id)
	{
		RuRo.BLL.EmpiInfo bll=new RuRo.BLL.EmpiInfo();
		RuRo.Model.EmpiInfo model=bll.GetModel(Id);
		this.lblId.Text=model.Id.ToString();
		this.lblPatientName.Text=model.PatientName;
		this.lblSex.Text=model.Sex;
		this.lblBirthday.Text=model.Birthday;
		this.lblCardId.Text=model.CardId;
		this.lblisDel.Text=model.isDel?"是":"否";

	}


    }
}
