using System;
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

        #region form提交过来的数据

        //private void PostData(HttpContext context)
        //{
        //    string empiInfo = context.Request.Params["formData"];
        //    string code = context.Request.Params["code"].Trim();
        //    string codeType = context.Request.Params["codeType"].Trim();
        //    string strusername = Common.CookieHelper.GetCookieValue("username").Trim();
        //    BLL.PatientDiagnose bll = new BLL.PatientDiagnose();
        //    string result = bll.PostData(empiInfo, code, codeType, strusername, false);
        //    context.Response.Write(result);
        //}
        //private void QueryData(HttpContext context, bool p)
        //{
        //    if (p)
        //    {
        //    }
        //    else
        //    {
        //        //查询数据
        //        //string Mzzybz = context.Request["Mzzybz"];//0 门诊 1住院
        //        string cardno = context.Request["code"].Trim();//住院号或门诊号
        //        //string cxrq00 = context.Request["cxrq00"].Trim();
        //        string dateNow = context.Request.Params["dateNow"].Trim();
        //        string codeType = context.Request.Params["codeType"].Trim();
        //        string date = DateTime.Now.ToString("yyyy-MM-dd");
        //        BLL.PatientDiagnose n = new BLL.PatientDiagnose();
        //        Model.QueryRecoder qqq = new Model.QueryRecoder();
        //        qqq.Code = cardno;
        //        qqq.CodeType = codeType;
        //        qqq.QueryType = "PatientDiagnose";
        //        qqq.Uname = Common.CookieHelper.GetCookieValue("username").Trim();
        //        qqq.AddDate = DateTime.Parse(dateNow);
        //        qqq.IsDel = false;
        //        BLL.Request.PatientDiagnoseResuest nnnnn = new BLL.Request.PatientDiagnoseResuest(qqq);
        //        BLL.PatientDiagnose NData = new BLL.PatientDiagnose();
        //        string jsonStrResult = NData.GetData(qqq, true);
        //        //string jsonStrResult = n.GetData(new Model.DTO.PatientDiagnoseResuest(cardno,cxrq00));
        //        context.Response.Write(jsonStrResult);
        //    }
        //}

        #endregion form提交过来的数据

        private void PostData(HttpContext context)
        {
            string code = context.Request.Params["code"].Trim();
            string codeType = context.Request.Params["codeType"].Trim();
            string strNormalLis = context.Request.Params["PatientDiagnoseLis"];
            string strusername = Common.CookieHelper.GetCookieValue("username").Trim();
            BLL.PatientDiagnose bll = new BLL.PatientDiagnose();
            string result = bll.PostData(code, codeType, strNormalLis, strusername, false);
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
                //  Model.DTO.NormalLisReportRequest request = new Model.DTO.NormalLisReportRequest(code, dateNow);
                // BLL.NormalLisReport normalLisReport = new BLL.NormalLisReport();
                //bool success;
                //object obj = n.GetData(code, ksrq00, jsrq00, out success);
                //ReturnData resd = new ReturnData(obj, success);
                //string jsonStrResult = resd.Res();

                //string result = normalLisReport.GetData(request, codeType);
                //context.Response.Write(result);
                Model.QueryRecoder qqq = new Model.QueryRecoder();
                qqq.Code = code;
                qqq.CodeType = codeType;
                qqq.QueryType = "PatientDiagnose";
                qqq.Uname = Common.CookieHelper.GetCookieValue("username");
                qqq.AddDate = DateTime.Parse(dateNow);
                qqq.IsDel = false;
                BLL.Request.PatientDiagnoseResuest nnnnn = new BLL.Request.PatientDiagnoseResuest(qqq);
                BLL.PatientDiagnose NData = new BLL.PatientDiagnose();
                string result = NData.GetData(qqq, true);
                context.Response.Write(result);
                //NData.GetData(BLL.Request.Request r);
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