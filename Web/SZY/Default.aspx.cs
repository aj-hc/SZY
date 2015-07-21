using RuRo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        private void AddUrlToiframe()
        {
            url = System.Configuration.ConfigurationManager.AppSettings["FpUrl"];
            FreezerPro.Attributes.Add("src", url);
        }
    }
}