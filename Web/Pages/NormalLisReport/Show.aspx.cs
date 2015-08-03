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
namespace RuRo.Web.NormalLisReport
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
		RuRo.BLL.NormalLisReport bll=new RuRo.BLL.NormalLisReport();
		RuRo.Model.NormalLisReport model=bll.GetModel(Id);
		this.lblId.Text=model.Id.ToString();
		this.lblhospnum.Text=model.hospnum;
		this.lblpatname.Text=model.patname;
		this.lblSex.Text=model.sex;
		this.lblAge.Text=model.age;
		this.lblage_month.Text=model.age_month;
		this.lblext_mthd.Text=model.ext_mthd;
		this.lblchinese.Text=model.chinese;
		this.lblresult.Text=model.result;
		this.lblunits.Text=model.units;
		this.lblref_flag.Text=model.ref_flag;
		this.lbllowvalue.Text=model.lowvalue;
		this.lblhighvalue.Text=model.highvalue;
		this.lblprint_ref.Text=model.print_ref;
		this.lblcheck_date.Text=model.check_date;
		this.lblcheck_by_name.Text=model.check_by_name;
		this.lblprnt_order.Text=model.prnt_order;

	}


    }
}
