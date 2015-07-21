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
namespace RuRo.Web.NormalLisReport
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txthospnum.Text.Trim().Length==0)
			{
				strErr+="病人门诊号、住院号不能为空！\\n";	
			}
			if(this.txtpatname.Text.Trim().Length==0)
			{
				strErr+="姓名不能为空！\\n";	
			}
			if(this.txtSex.Text.Trim().Length==0)
			{
				strErr+="性别不能为空！\\n";	
			}
			if(this.txtAge.Text.Trim().Length==0)
			{
				strErr+="年龄不能为空！\\n";	
			}
			if(this.txtage_month.Text.Trim().Length==0)
			{
				strErr+="月不能为空！\\n";	
			}
			if(this.txtext_mthd.Text.Trim().Length==0)
			{
				strErr+="项目总称不能为空！\\n";	
			}
			if(this.txtchinese.Text.Trim().Length==0)
			{
				strErr+="项目名称不能为空！\\n";	
			}
			if(this.txtresult.Text.Trim().Length==0)
			{
				strErr+="结果不能为空！\\n";	
			}
			if(this.txtunits.Text.Trim().Length==0)
			{
				strErr+="单位不能为空！\\n";	
			}
			if(this.txtref_flag.Text.Trim().Length==0)
			{
				strErr+="高低 不能为空！\\n";	
			}
			if(this.txtlowvalue.Text.Trim().Length==0)
			{
				strErr+="正常低值不能为空！\\n";	
			}
			if(this.txthighvalue.Text.Trim().Length==0)
			{
				strErr+="正常高值不能为空！\\n";	
			}
			if(this.txtprint_ref.Text.Trim().Length==0)
			{
				strErr+="正常范围不能为空！\\n";	
			}
			if(this.txtcheck_date.Text.Trim().Length==0)
			{
				strErr+="批准时间不能为空！\\n";	
			}
			if(this.txtcheck_by_name.Text.Trim().Length==0)
			{
				strErr+="批准人不能为空！\\n";	
			}
			if(this.txtprnt_order.Text.Trim().Length==0)
			{
				strErr+="打印顺序序号不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string hospnum=this.txthospnum.Text;
			string patname=this.txtpatname.Text;
			string Sex=this.txtSex.Text;
			string Age=this.txtAge.Text;
			string age_month=this.txtage_month.Text;
			string ext_mthd=this.txtext_mthd.Text;
			string chinese=this.txtchinese.Text;
			string result=this.txtresult.Text;
			string units=this.txtunits.Text;
			string ref_flag=this.txtref_flag.Text;
			string lowvalue=this.txtlowvalue.Text;
			string highvalue=this.txthighvalue.Text;
			string print_ref=this.txtprint_ref.Text;
			string check_date=this.txtcheck_date.Text;
			string check_by_name=this.txtcheck_by_name.Text;
			string prnt_order=this.txtprnt_order.Text;

			RuRo.Model.NormalLisReport model=new RuRo.Model.NormalLisReport();
			model.hospnum=hospnum;
			model.patname=patname;
			model.Sex=Sex;
			model.Age=Age;
			model.age_month=age_month;
			model.ext_mthd=ext_mthd;
			model.chinese=chinese;
			model.result=result;
			model.units=units;
			model.ref_flag=ref_flag;
			model.lowvalue=lowvalue;
			model.highvalue=highvalue;
			model.print_ref=print_ref;
			model.check_date=check_date;
			model.check_by_name=check_by_name;
			model.prnt_order=prnt_order;

			RuRo.BLL.NormalLisReport bll=new RuRo.BLL.NormalLisReport();
			bll.Add(model);
			RuRo.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
