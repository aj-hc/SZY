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
namespace RuRo.Web.QueryRecoder
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
		RuRo.BLL.QueryRecoder bll=new RuRo.BLL.QueryRecoder();
		RuRo.Model.QueryRecoder model=bll.GetModel(Id);
		this.lblId.Text=model.Id.ToString();
		this.lblUname.Text=model.Uname;
		this.lblLastQueryDate.Text=model.LastQueryDate;
		this.lblCode.Text=model.Code;
		this.lblCodeType.Text=model.CodeType;
		this.lblQueryType.Text=model.QueryType;
		this.lblQueryResult.Text=model.QueryResult;

	}


    }
}
