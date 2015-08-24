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
using Maticsoft.Common;
using LTP.Accounts.Bus;
namespace RuRo.Web.QueryRecoder
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtUname.Text.Trim().Length==0)
			{
				strErr+="查询的用户不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtAddDate.Text))
			{
				strErr+="AddDate格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtLastQueryDate.Text))
			{
				strErr+="最后一次查询日期格式错误！\\n";	
			}
			if(this.txtCode.Text.Trim().Length==0)
			{
				strErr+="查询的条码号不能为空！\\n";	
			}
			if(this.txtCodeType.Text.Trim().Length==0)
			{
				strErr+="条码号类型不能为空！\\n";	
			}
			if(this.txtQueryType.Text.Trim().Length==0)
			{
				strErr+="查询的数据类型不能为空！\\n";	
			}
			if(this.txtQueryResult.Text.Trim().Length==0)
			{
				strErr+="QueryResult不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string Uname=this.txtUname.Text;
			DateTime AddDate=DateTime.Parse(this.txtAddDate.Text);
			DateTime LastQueryDate=DateTime.Parse(this.txtLastQueryDate.Text);
			string Code=this.txtCode.Text;
			string CodeType=this.txtCodeType.Text;
			string QueryType=this.txtQueryType.Text;
			string QueryResult=this.txtQueryResult.Text;
			bool IsDel=this.chkIsDel.Checked;

			RuRo.Model.QueryRecoder model=new RuRo.Model.QueryRecoder();
			model.Uname=Uname;
			model.AddDate=AddDate;
			model.LastQueryDate=LastQueryDate;
			model.Code=Code;
			model.CodeType=CodeType;
			model.QueryType=QueryType;
			model.QueryResult=QueryResult;
			model.IsDel=IsDel;

			RuRo.BLL.QueryRecoder bll=new RuRo.BLL.QueryRecoder();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
