using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuRo.Web.Sever
{
    /// <summary>
    /// GetDataSever 的摘要说明
    /// </summary>
    public class GetDataSever : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request.Params["action"].Trim();
            switch (action)
            {
                case "gethisdata":
                    GetData(context);
                    break;
                default:
                    break;
            }

        }

        private void GetData(HttpContext context)
        {
            string Mzzybz = context.Request["Mzzybz"];//0 门诊 1住院
            string Mzhzyh = context.Request["Mzhzyh"];//住院号或门诊号
            string jsonStrResult = "";
            BLL.GetDataFromHospital hospitalDataByBarcode = new BLL.GetDataFromHospital();
            jsonStrResult = hospitalDataByBarcode.GetDataByBarcode(Mzhzyh, Mzzybz);

            if (jsonStrResult == "")//直接无数据返回（可能是链接有问题）
            {
                string result = "{\"success\":false,\"msg\":\"获取数据失败，请检查住院号或门诊号\"}";
                context.Response.Write(result);
                context.Response.End();
            }
            if (jsonStrResult.Contains("ErrorMsg"))
            {
                string oPListForSpecimen = FreezerProUtility.Fp_Common.FpJsonHelper.GetStrFromJsonStr("ErrorMsg", jsonStrResult);
                string result = "{\"success\":false,\"msg\":" + oPListForSpecimen + "}";
                context.Response.Write(result);
                context.Response.End();
            }
            if (jsonStrResult.Contains("Name"))
            {
                string result = "{\"success\":true,\"msg\":" + jsonStrResult + "}";
                context.Response.Write(result);
                context.Response.End();
            }
            else
            {
                string result = "";
                context.Response.Write(result);
                context.Response.End();
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