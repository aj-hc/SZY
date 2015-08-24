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
                    case "post":/*查询*/
                        PostData(context);
                        break;
                }
            }
            else
                QueryData(context, false);
        }

        private void PostData(HttpContext context)
        {
            string empiInfo = context.Request.Params["empiInfo"];
            BLL.EmpiInfo bll = new BLL.EmpiInfo();
            string result = bll.PostData(empiInfo);
        }

        private void QueryData(HttpContext context, bool p)
        {
            if (p)
            {

            }
            else
            {
                string mzzybz = context.Request["Mzzybz"];//0 门诊 1住院
                string mzhzyh = context.Request["Mzhzyh"];//住院号或门诊号
                BLL.EmpiInfo EmpiInfo = new BLL.EmpiInfo();
                Model.DTO.EmpiInfoRequest request = new Model.DTO.EmpiInfoRequest(mzhzyh, mzzybz);
                string result = EmpiInfo.GetSampleSourceData(request);
                //object obj = EmpiInfo.GetDataByCode(Mzhzyh, Mzzybz, out success);
                //ReturnData state = new ReturnData(obj,success);
                //string jsonStrResult = state.Res();
                context.Response.Write(result);
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