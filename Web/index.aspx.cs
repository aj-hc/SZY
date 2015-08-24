using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RuRo.Web
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Common.CookieHelper.GetCookieValue("username");
            string keshi = Common.CookieHelper.GetCookieValue(username + "department");
            try
            {
                keshi = Common.DEncrypt.DESEncrypt.Decrypt(keshi);
            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
                keshi = "";
            }
            if (keshi == "")
            {
                laName.Text = username;
            }
            else
            {
                laName.Text = keshi + "/" + username;
            }
        }
    }
}