using System;

namespace RuRo.Web
{
    public partial class ExtendPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Login login = new Login();
                //页面第一次加载
                if (!login.CheckLoginByCookie())
                {
                    //  Response.Redirect("Login.aspx");
                }
                string username = Common.CookieHelper.GetCookieValue("username");
                //string keshi = Common.CookieHelper.GetCookieValue(username + "department");
                //try
                //{
                //    keshi = Common.DEncrypt.DESEncrypt.Decrypt(keshi);
                //}
                //catch (Exception ex)
                //{
                //    Common.LogHelper.WriteError(ex);
                //    keshi = "";
                //}
                //if (keshi == "")
                //{
                //    lakeshi.Text = username;
                //}
                //else
                //{
                //    lakeshi.Text = keshi + "/" + username;
                //}
                // lakeshi.Text = username;
            }
        }
    }
}