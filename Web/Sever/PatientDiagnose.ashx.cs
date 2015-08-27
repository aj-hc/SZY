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
                    case "post":/*提交*/
                        PostData(context);
                        break;
                }
            }
            else
                QueryData(context, true);
        }
        private void PostData(HttpContext context)
        {
            string empiInfo = context.Request.Params["formData"];
            string code = context.Request.Params["code"];
            string codeType = context.Request.Params["codeType"];
            BLL.PatientDiagnose bll = new BLL.PatientDiagnose();
            string result = bll.PostData(empiInfo, code, codeType);
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
                string cardno = context.Request["code"];//住院号或门诊号
                string cxrq00 = context.Request["cxrq00"];
                string dateNow = context.Request.Params["dateNow"];
                string codeType = context.Request.Params["codeType"];
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                //Model.QueryRecoder model_Q = new Model.QueryRecoder();
                //model_Q.Uname = Common.CookieHelper.GetCookieValue("username");
                //model_Q.Code = cardno;
                //model_Q.AddDate = Convert.ToDateTime(date);
                //model_Q.LastQueryDate = Convert.ToDateTime(date);
                //BLL.QueryRecoder bll = new BLL.QueryRecoder();
                //bll.Add(model_Q);
                //bll.Add(model_Q);
                BLL.PatientDiagnose n = new BLL.PatientDiagnose();
               // BLL.NormalLisReport cd = new BLL.NormalLisReport();
                string jsonStrResult = n.GetData(new Model.DTO.PatientDiagnoseResuest(cardno,cxrq00));
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