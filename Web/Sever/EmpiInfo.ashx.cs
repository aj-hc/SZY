using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuRo.Web.Sever
{
    /// <summary>
    /// EmpiInfo 的摘要说明
    /// </summary>
    public class EmpiInfo : IHttpHandler
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
                QueryData(context, false);
        }

        private void QueryData(HttpContext context, bool p)
        {
            if (p)
            {

            }
            else
            {
                string Mzzybz = context.Request["Mzzybz"];//0 门诊 1住院
                string Mzhzyh = context.Request["Mzhzyh"];//住院号或门诊号
                BLL.EmpiInfo EmpiInfo = new BLL.EmpiInfo();
                bool success;
                object obj = EmpiInfo.GetDataByCode(Mzhzyh, Mzzybz, out success);
                ReturnData state = new ReturnData(obj,success);
                string jsonStrResult = state.Res();
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