using System;

namespace RuRo.Web
{
    public partial class EmpiInfo_info_D : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Login login = new Login();
                //ҳ���һ�μ���
                //
                if (!login.CheckLoginByCookie())
                {
                }
                string username = Common.CookieHelper.GetCookieValue("username");
            }
        }
    }
}