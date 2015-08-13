using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuRo.Web.Sever
{
    /// <summary>
    /// PatientDiagnose 的摘要说明
    /// </summary>
    public class PatientDiagnose : IHttpHandler
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
                }
            }
            else
                QueryData(context, true);
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
                string code = context.Request["code"];//住院号或门诊号
                string cxrq00 = context.Request["cxrq00"];
                BLL.PatientDiagnose n = new BLL.PatientDiagnose();
                bool success;
                object obj = n.GetData(code, cxrq00,out success);
                ReturnData resd = new ReturnData(obj,success);
                string jsonStrResult = resd.Res();
                context.Response.Write(jsonStrResult);
            }
        }

        private void DeleteData(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private void SaveData(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private void InfoData(HttpContext context)
        {
            throw new NotImplementedException();
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