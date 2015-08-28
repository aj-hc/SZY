using RuRo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace RuRo.Web.Sever
{
    /// <summary>
    /// NormalLisReport 的摘要说明
    /// </summary>
    public class NormalLisReport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["mode"] != null)
            {
                string mode = context.Request["mode"].ToString();
                switch (mode)
                {
                    case "inf":/*查询实体类*/
                        InfoData(context);
                        break;
                    case "ins":/*新增*/
                        SaveData(context);
                        break;
                    case "upd":/*修改*/
                        SaveData(context);
                        break;
                    case "del":/*删除*/
                        DeleteData(context);
                        break;
                    case "qry":/*查询*/
                        QueryData(context, false);
                        break;
                    case "post":/*上传*/
                        PostData(context);
                        break;
                }
            }
            else
                QueryData(context, true);
        }

        private void PostData(HttpContext context)
        {
            string code = context.Request.Params["code"];
            string codeType = context.Request.Params["codeType"];
            string strNormalLis = context.Request.Params["NormalLis"];
            BLL.NormalLisReport bll = new BLL.NormalLisReport();
            string result = bll.PostData(code, codeType, strNormalLis);
            context.Response.Write(result);
        }

        private void QueryData(HttpContext context, bool p)
        {
            if (p)
            {
            }
            else
            {
                //查询数据
                //string Mzzybz = context.Request["Mzzybz"];//0 门诊 1住院
                string code = context.Request.Params["code"];//住院号或门诊号
                string codeType = context.Request.Params["codeType"];
                string dateNow = context.Request.Params["dateNow"];
                Model.DTO.NormalLisReportRequest request = new Model.DTO.NormalLisReportRequest(code,dateNow);
                BLL.NormalLisReport normalLisReport = new BLL.NormalLisReport();
                //bool success;
                //object obj = n.GetData(code, ksrq00, jsrq00, out success);
                //ReturnData resd = new ReturnData(obj, success);
                //string jsonStrResult = resd.Res();
                string result = normalLisReport.GetData(request, codeType);
                context.Response.Write(result);


                Model.QueryRecoder qqq = new Model.QueryRecoder();
                qqq.Code= code;
                qqq.CodeType = codeType;
                qqq.QueryType = "N";
                qqq.Uname =Common.CookieHelper.GetCookieValue("username");
                qqq.AddDate = DateTime.Parse(dateNow);
                qqq.IsDel = false;

                BLL.Request.NormalLisReportRequest nnnnn = new BLL.Request.NormalLisReportRequest(qqq);
                BLL.NormalLisReport NData = new BLL.NormalLisReport();
                //NData.GetData(BLL.Request.Request r);

            }
        }

        private void DeleteData(HttpContext context)
        {
            string pk = context.Request["pk"];
            bool success = true;
            Object obj = pk;
            //string msg = "删除成功";
            //ReturnData resd = new ReturnData(obj, success,msg);
            //string jsonStrResult = resd.Res();
            //context.Response.Write(jsonStrResult);
        }

        private void SaveData(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static void InfoData(HttpContext context)
        {
            
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