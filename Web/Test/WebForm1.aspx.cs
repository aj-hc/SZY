using System;

namespace RuRo.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RuRo.Model.QueryRecoder model = new Model.QueryRecoder();
            DateTime dt = new DateTime();
            for (int i = 0; i < 100; i++)
            {
                model.Uname = "kaka" + i.ToString();
                dt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                model.AddDate = dt.AddDays(i);
                model.LastQueryDate = dt.AddDays(i);
                model.Code = "code" + i.ToString();
                model.CodeType = "CodeType" + i.ToString();
                model.QueryType = "QueryType" + i.ToString();
                model.QueryResult = "QueryResult" + i.ToString();
                model.IsDel = true;
                RuRo.BLL.QueryRecoder bll = new BLL.QueryRecoder();
                bll.Add(model);
            }
        }
    }
}