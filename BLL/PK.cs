using RuRo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.BLL
{
    public class PK
    {

        public string GetHTTPWebServiceData(string request) 
        {
            //修改为读取HTTP请求WEBSERVICE
            string xml = PostData(request);
            //本地读取XML
            string xml2 = "";
            return xml2;
        }

        /// <summary>
        /// 使用HTTP上传
        /// </summary>
        /// <returns></returns>
        public string PostData(string request)
        {
            StringBuilder jsonData = new StringBuilder();
            RuRo.Common.HttpHelper http = new RuRo.Common.HttpHelper();
            RuRo.Common.HttpItem item = new RuRo.Common.HttpItem()
            {
                URL = "http://svc.chas.gdhtcm.com:9080/ChasSvc/services/ChasCommonPort",//URL     必需项  
                Method = "post",//URL     可选项 默认为Get  
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Cookie = "",//字符串Cookie     可选项  
                Referer = "",//来源URL     可选项  
                Postdata = request,//Post数据     可选项GET时不需要写  
                PostEncoding = Encoding.UTF8,
                Timeout = 100000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000  
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值  
                Allowautoredirect = true,//是否根据301跳转     可选项  
                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数  
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024  
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数  
                //ProxyPwd = "123456",//代理服务器密码     可选项  
                //ProxyUserName = "administrator",//代理服务器账户名     可选项  
                ResultType = ResultType.String
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            string cookie = result.Cookie;
            return html;
        }  
        //获取传入参数
        public List<string> GetRequestStrForDischargeDate(Model.DTO.PatientDiagnose_list_F model)
        {
            List<string> list = new List<string>();
            DateTime ksrq00 = new DateTime();
            DateTime jsrq00 = new DateTime();
            if (DateTime.TryParse(model.ksrq00, out ksrq00) && DateTime.TryParse(model.jsrq00, out jsrq00))
            {
                if (ksrq00 <= jsrq00)
                {
                    string str = CreatRequestStrDischargeDate(model.code, ksrq00, jsrq00);
                    list.Add(str);
                }
                if (ksrq00 > jsrq00)
                {
                    string str = CreatRequestStrDischargeDate(model.code, jsrq00, ksrq00);
                    list.Add(str);
                }
            }
            return list;
        }

        //获取出院日期传参
        private string CreatRequestStrDischargeDate(string code, DateTime ksrq00, DateTime jsrq00)
        {
            StringBuilder sb = new StringBuilder();
            string date = DateTime.Now.ToString();
            sb.Append("<?xml version='1.0' encoding='UTF-8' standalone='true'?>");
            sb.Append("<QueryPatientRequest xmlns='http://chas.hit.com/transport/integration/common/msg'>");
            sb.Append("<reqHeader><systemId>AJ</systemId>");
            sb.Append("<reqTimestamp>" + date + "</reqTimestamp>");
            sb.Append("<terminalIp>192.168.1.40</terminalIp></reqHeader>");
            sb.Append("<patientNo>" + code + "</patientNo>");
            sb.Append("<startDate>" + ksrq00.ToString("yyyy-MM-dd") + "</startDate>");
            sb.Append("<endDate>" + jsrq00.ToString("yyyy-MM-dd") + "</endDate>");
            sb.Append("<dateField>DischargeDate</dateField>");
            sb.Append("<queryMode>ALL</queryMode></QueryPatientRequest>");
            return sb.ToString();
        }
        //获取传入参数
        public List<string> GetRequestStrForAdmissionDate(Model.DTO.PatientDiagnose_list_F model)
        {
            List<string> list = new List<string>();
            DateTime ksrq00 = new DateTime();
            DateTime jsrq00 = new DateTime();
            if (DateTime.TryParse(model.ksrq00, out ksrq00) && DateTime.TryParse(model.jsrq00, out jsrq00))
            {
                if (ksrq00 <= jsrq00)
                {
                    string str = CreatRequestStrAdmissionDate(model.code, ksrq00, jsrq00);
                    list.Add(str);
                }
                if (ksrq00 > jsrq00)
                {
                    string str = CreatRequestStrAdmissionDate(model.code, jsrq00, ksrq00);
                    list.Add(str);
                }
            }
            return list;
        }

        //获取住院日期传参
        private string CreatRequestStrAdmissionDate(string code, DateTime ksrq00, DateTime jsrq00)
        {
            StringBuilder sb=new StringBuilder();
            string date=DateTime.Now.ToString();
            sb.Append("<?xml version='1.0' encoding='UTF-8' standalone='true'?>");
            sb.Append("<QueryPatientRequest xmlns='http://chas.hit.com/transport/integration/common/msg'>");
            sb.Append("<reqHeader><systemId>AJ</systemId>");
            sb.Append("<reqTimestamp>"+date+"</reqTimestamp>");
            sb.Append("<terminalIp>192.168.1.40</terminalIp></reqHeader>");
            sb.Append("<patientNo>"+code+"</patientNo>");
            sb.Append("<startDate>"+ksrq00.ToString("yyyy-MM-dd")+"</startDate>");
            sb.Append("<endDate>"+jsrq00.ToString("yyyy-MM-dd")+"</endDate>");
            sb.Append("<dateField>AdmissionDate</dateField>");
            sb.Append("<queryMode>ALL</queryMode></QueryPatientRequest>");
            return sb.ToString();
        }
    }
}
