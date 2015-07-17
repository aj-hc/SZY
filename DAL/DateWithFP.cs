using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{

    /// <summary>
    /// 和Fp交换数据
    /// </summary>
    public class DataWithFP
    {
        HttpProc.WebClient webClient = new HttpProc.WebClient();
        
        #region 访问Fp的用户名属性
        private string username = "";
        /// <summary>
        /// Fp访问名称
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        #endregion

        #region 访问Fp的密码属性
        private string passWord = "";
        ///// <summary>
        ///// Fp访问密码
        ///// </summary>
        //public string Password
        //{
        //    get { return password; }
        //    set { password = value; }
        //}
        #endregion

        #region auth_token 属性
        string auth_token;
        public string Auth_token
        {
            get { return auth_token; }
            set { auth_token = value; }
        }
        #endregion

        //连接字符串（包含username、password）
        string uriStr = "";

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uri">“http://192.168.1.100”</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        public DataWithFP()
        {
            Username = AccountHelper.GetActiveAccountUesrName()[0];
            passWord = AccountHelper.GetActiveAccountUesrName()[1];
            uriStr = string.Format("{0}/api?", XmlHelper.Read("configXML\\UriConfigXml.xml", "Uri"));
            auth_token = GetAuth_token();
        }
        //指定账号密码
        public DataWithFP(string username, string password)
        {
            Username = username;
            passWord = password;
            uriStr = string.Format("{0}/api?", XmlHelper.Read("configXML\\UriConfigXml.xml", "Uri"));
            auth_token = GetAuth_token();
        }
        #endregion

        #region 无参取数据方法(如sample_source_type) + public string getDateFromFp(FpMethod fpMethod)

        /// <summary>
        /// 无参取数据方法(如sample_source_type)
        /// </summary>
        /// <param name="fpMethod">具体方法</param>
        /// <returns>返回取到的字符串</returns>
        public string getDateFromFp(FpMethod fpMethod)
        {
            //string s = webClient.GetHtml(string.Format("{0}username={1}&auth_token={2}&method={3}", uriStr, username, auth_token, fpMethod));
            //return s;
            return webClient.Post(uriStr, string.Format("username={0}&auth_token={1}", username, auth_token) + "&method=" + fpMethod);
        }
        #endregion

        #region 有参取数据方法 + public string getDateFromFp(FpMethod fpMethod, string parameters)
        /// <summary>
        /// 有参取数据方法
        /// </summary>
        /// <param name="fpMethod">具体方法</param>
        /// <param name="parameters">可选参数如（&id=1）“没有就空字符串”</param>
        /// <returns>查询到的结果字符串</returns>
        public string getDateFromFp(FpMethod fpMethod, string parameters)
        {
            webClient.Encoding = Encoding.UTF8;
            string result = webClient.Post(string.Format("{0}username={1}&auth_token={2}&method={3}", uriStr, username, auth_token, fpMethod), parameters);
            return result;
        }
        #endregion

        #region post数据到fp（update_source）public string postDateToFp(FpMethod fpMethod, string date)

        ///// <summary>
        ///// post数据到fp（update_source）
        ///// </summary>
        ///// <param name="fpMethod">api方法</param>
        ///// <param name="date">数据（要包含参数,不包含账号和密码）</param>
        ///// <returns>返回结果</returns>
        //public string postDateToFp(FpMethod fpMethod, string date)
        //{
        //    HttpProc.WebClient webClient = new HttpProc.WebClient();
        //    string result = webClient.Post(string.Format("{0}username={1}&password={2}", uriStr, username, passWord) + "&method=" + fpMethod, date);
        //    return   result;
        //} 
        #endregion

        #region post数据到fp + public string postDateToFp(FpMethod fpMethod, string date) 如import_source

        /// <summary>
        /// post数据到fp（update_source）
        /// </summary>
        /// <param name="fpMethod">api方法</param>
        /// <param name="date">数据（要包含参数,不包含账号和密码、auth_token）</param>
        /// <returns>返回结果</returns>
        public string postDateToFp(FpMethod fpMethod, string date)
        {
            webClient.Encoding = Encoding.UTF8;
            string result = webClient.Post(string.Format("{0}username={1}&auth_token={2}&method={3}", uriStr, username, auth_token, fpMethod), date);
            return result;
        }
        #endregion

        #region 获取当前回话的auth_token,代替password，构造函数中调用 + public string GetAuth_token()
        //获取auth_token;
        /// <summary>
        /// 获取当前回话的auth_token,代替password，构造函数中调用
        /// </summary>
        /// <returns>auth_token</returns>
        public string GetAuth_token()
        {
            string ss = webClient.Post(string.Format("{0}username={1}&password={2}", uriStr, username, passWord) + "&method=gen_token", "");
            return FpJsonHelper.GetStrFromJsonStr("auth_token", ss);
        }
        #endregion
    }
}
