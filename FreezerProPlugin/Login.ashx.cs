using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FreezerProPlugin
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Request["type"] == "CheckLogin")
            {
                if (context.Session["Username"] == null)
                {
                    if (context.Request.Cookies["loginCookie"] == null)
                    {
                        context.Response.Write("<button id='dologin' style=\"width:50px;\" onclick=\"dologin()\">登录</button>使用FreezerPro协同助手");
                    }
                    else
                    {
                        context.Response.Write("<button style=\"width:50px;\" onclick=\"doimport()\">导入</button><button style=\"width:50px;\" onclick=\"logout()\">注销</button>");
                    }
                }
                else
                {
                    context.Response.Write("<button style=\"width:50px;\" onclick=\"doimport()\">导入</button><button style=\"width:50px;\" onclick=\"logout()\">注销</button>");
                }
            }
            else if (context.Request["type"] == "Logout") 
            {
                context.Session["Username"] = null;
                context.Session["Password"] = null;
                context.Response.Cookies["loginCookie"].Expires.AddDays(0);
                context.Response.Write("<button style=\"width:50px;\" onclick=\"dologin()\">登录</button>使用FreezerPro协同助手");
            }
            else
            {
                //获取用户输入的账号密码验证数据是否正确
                string username = context.Request["username"];
                string password = context.Request["password"];
                BLL.Token token = new BLL.Token(username,password);
                if (token.checkAuth_Token())
                {
                    context.Session["Username"] = username;
                    context.Session["Password"] = password;
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Values.Add("Username", username);
                    loginCookie.Values.Add("Password", password);
                    loginCookie.Expires = DateTime.Now.AddDays(1);
                    context.Response.Cookies.Add(loginCookie);
                    context.Response.Write("恭喜你,登录成功,欢迎使用FreezerPro协同助手！");
                }
                else
                {
                    context.Response.Write("对不起,密码错误,请重新输入！");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}