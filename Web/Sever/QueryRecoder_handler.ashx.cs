using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace RuRo
{
    public class QueryRecoder_handler : IHttpHandler
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
                        QueryData(context);
                        //queryDataSZY(context);
                        break;
                    case "qryt":/*导出*/
                        QueryTData(context);
                        break;
                    case "post":/*上传*/
                        PostData(context);
                        break;
                }
            }
            else
                QueryData(context);
        }

        /// <summary>
        /// 上传到freezerpro临床检验数据
        /// </summary>
        /// <param name="context"></param>
        private void PostData(HttpContext context)
        {
            string code = context.Request.Params["code"];
            string codeType = context.Request.Params["codeType"];
            string strRecoder = context.Request.Params["Recoder"];
            string username = Common.CookieHelper.GetCookieValue("username");
            BLL.SZY.QueryRecoder bll = new BLL.SZY.QueryRecoder();
            string result = bll.PostData(strRecoder, username);
            context.Response.Write(result);
        }

        /// <summary>
        /// 查询info数据实体类
        /// </summary>
        /// <param name="context"></param>
        private static void InfoData(HttpContext context)
        {
            //BLL.QueryRecoder_BLL bll_QueryRecoder = new BLL.QueryRecoder_BLL();
            //Model.QueryRecoder model_QueryRecoder = new Model.QueryRecoder();
            //DataTable dt = new DataTable();
            //if (context.Request["pk"] != null)
            //{
            //    int pk = int.Parse(context.Request["pk"]);
            //    model_QueryRecoder = bll_QueryRecoder.GetModel(pk);
            //    bll_QueryRecoder.GetModel(ref dt, pk);
            //}
            //string strJson = JSONHelper.DataTable2Json(dt, true);
            //context.Response.Write(strJson);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="context"></param>
        private static void SaveData(HttpContext context)
        {
            //result rlt = new result(); 
            //try
            //{
            //    //获取保存方式
            //    string mode = context.Request["mode"].ToString();
            //    int strPk = 0;
            //    if (mode == "upd")
            //    {
            //        strPk = int.Parse(context.Request["pk"].ToString());
            //    }
            //    BLL.QueryRecoder_BLL bll_QueryRecoder = new BLL.QueryRecoder_BLL();
            //    Model.QueryRecoder model_QueryRecoder = new Model.QueryRecoder(); 
            //    #region 实体类赋值
            //    if (mode == "ins")
            //    {
            //        //编码表采用Max获取，请注意设置字段长度
            //        model_QueryRecoder._id = bll_QueryRecoder.GetMax_id();//主键赋值
            //    }

            //    if(context.Request["uname"]!=null)
            //        model_QueryRecoder._uname = context.Request["uname"];

            //    if(context.Request["adddate"]!=null)
            //        model_QueryRecoder._adddate = Convert.ToDateTime(context.Request["adddate"]);

            //    if(context.Request["lastquerydate"]!=null)
            //        model_QueryRecoder._lastquerydate = Convert.ToDateTime(context.Request["lastquerydate"]);

            //    if(context.Request["code"]!=null)
            //        model_QueryRecoder._code = context.Request["code"];

            //    if(context.Request["codetype"]!=null)
            //        model_QueryRecoder._codetype = context.Request["codetype"];

            //    if(context.Request["querytype"]!=null)
            //        model_QueryRecoder._querytype = context.Request["querytype"];

            //    if(context.Request["queryresult"]!=null)
            //        model_QueryRecoder._queryresult = context.Request["queryresult"];

            //    if(context.Request["isdel"]!=null)
            //        model_QueryRecoder._isdel = Convert.ToBoolean(context.Request["isdel"]);

            //    #endregion
            //    if (mode == "ins")
            //    {
            //        if (bll_QueryRecoder.Insert(model_QueryRecoder))
            //        {
            //            rlt.success = true;
            //            rlt.msg = "新增保存成功";
            //        }
            //        else
            //        {
            //            rlt.success = false;
            //            rlt.msg = "新增保存失败:" + DbError.getErrorMsg();
            //        }
            //    }

            //    if (mode == "upd")
            //    {
            //        if (bll_QueryRecoder.Update(model_QueryRecoder, strPk))
            //        {
            //            rlt.success = true;
            //            rlt.msg = "修改保存成功";
            //        }
            //        else
            //        {
            //            rlt.success = false;
            //            rlt.msg = "修改保存失败:" + DbError.getErrorMsg();
            //        }
            //    }
            //}
            //catch(Exception exception)
            //{
            //    rlt.success = false;
            //    rlt.msg = exception.Message;
            //}
            //context.Response.Write(JSONHelper.Convert2Json(rlt)); 
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="context"></param>
        private static void DeleteData(HttpContext context)
        {
            string mes = "";
            if (context.Request["pk"] != null)
            {
                string pk = context.Request["pk"];
                string[] ArrayPk = pk.Split(',');
                ArrayList list = new ArrayList();
                for (int i = 0; i < ArrayPk.Length-1; i++)
                {
                    string strnull = ArrayPk[i];
                    if (strnull!="")
                    {
                        list.Add(ArrayPk[i]);
                    }
                }
                RuRo.BLL.QueryRecoder bll_QueryRecoder = new BLL.QueryRecoder();
                int successNumber = 0;
                string errorMessage = "";
                foreach (string strPk in list)
                {
                    if (bll_QueryRecoder.Delete(int.Parse(strPk)))
                    {
                        successNumber += 1;
                    }
                }
                mes = "成功删除[" + successNumber.ToString() + "/" + ArrayPk.Length.ToString() + "]条数据;" + errorMessage;
            }
            else
            {
                mes="PK字段为Null";
            }
            context.Response.Write(mes);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="context"></param>
        private static void QueryData(HttpContext context)
        {
            string adddate=context.Request.Params["adddate"].ToString();
            string username = Common.CookieHelper.GetCookieValue("username");
            RuRo.BLL.QueryRecoder bll = new BLL.QueryRecoder();
            List<Model.QueryRecoder> list = bll.GetModelList("IsDel=0 and AddDate!='"+adddate+"' and Uname='"+username+"'");
            string mes = FreezerProUtility.Fp_Common.FpJsonHelper.ObjectToJsonStr(list);
            context.Response.Write(mes);
        }

        private static void QueryTData(HttpContext context)
        {
            string adddate = context.Request.Params["adddate"].ToString();
            string username = Common.CookieHelper.GetCookieValue("username");
            string pageNum = context.Request.Params["pageNum"].ToString();
            string pageSize = context.Request.Params["pageSize"].ToString();
            string strwhere = "IsDel=0 and AddDate='" + adddate + "' and Uname='" + username + "'";
            string strorder = "AddDate ASC";
            int startIndex =Convert.ToInt32(pageNum);
            int endIndex = Convert.ToInt32(pageSize);
           // RuRo.BLL.QueryRecoder bll = new BLL.QueryRecoder();
            RuRo.BLL.SZY.QueryRecoder bll_QueryRecoder = new BLL.SZY.QueryRecoder();
           // DataSet ds = bll.GetListByPage(strwhere,strorder,startIndex,endIndex);
            DataSet ds = bll_QueryRecoder.GetQueryRecoderTrue_bll(endIndex, startIndex, strwhere, strorder);//按照页码获取列表
            List<Model.QueryRecoder> list = bll_QueryRecoder.DataTableToList(ds.Tables[0]);//转换为List
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //List<Model.QueryRecoder> list = bll.GetModelList("IsDel=0 and AddDate!='" + adddate + "' and Uname='" + username + "' order by AddDate DESC");
            string kk = FreezerProUtility.Fp_Common.FpJsonHelper.ObjectToJsonStr(list);
            dic.Add("Qdata",kk);//添加到DIC
            int count = bll_QueryRecoder.GetRecordCount("IsDel=0 and AddDate='" + adddate + "' and Uname='" + username + "'");//获取表的总记录数
            dic.Add("total", count.ToString());//添加到DIC
            string mes = FreezerProUtility.Fp_Common.FpJsonHelper.ObjectToJsonStr(dic);
            context.Response.Write(mes);
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

