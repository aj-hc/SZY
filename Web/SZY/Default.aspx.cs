using System;

namespace RuRo.Web
{
    public partial class _in : System.Web.UI.Page
    {
        public string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddUrlToiframe();
            }
            else
            {
            }
        }

        protected void AddUrlToiframe()
        {
            url = System.Configuration.ConfigurationManager.AppSettings["FpUrl"];
        }
    }
}