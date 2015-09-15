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
                    case "del":/*ɾ��*/
                        DeleteData(context);  
                        break;
                    case "qry":/*��ѯ*/
                        QueryTData(context);
                        //queryDataSZY(context);
                        break;
                    case "qryt":/*����*/
                        QueryTData(context);
                        break;
                    case "post":/*�ϴ�*/
                        PostData(context);
                        break;
                }
            }
            else
                QueryTData(context);
        }

        /// <summary>
        /// �ϴ���freezerpro�ٴ���������
        /// </summary>
        /// <param name="context"></param>
        private void PostData(HttpContext context)
        {
            string code = context.Request.Params["code"];
            string codeType = context.Request.Params["codeType"];
            string strRecoder = context.Request.Params["Recoder"];
            BLL.SZY.QueryRecoder bll = new BLL.SZY.QueryRecoder();
            string result = bll.PostData(strRecoder);
            context.Response.Write(result);
        }
        /// <summary>
        /// ɾ������
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
                mes = "�ɹ�ɾ��[" + successNumber.ToString() + "/" + ArrayPk.Length.ToString() + "]������;" + errorMessage;
            }
            else
            {
                mes="PK�ֶ�ΪNull";
            }
            context.Response.Write(mes);
        }


        private static void QueryTData(HttpContext context)
        {
            string adddate = context.Request.Params["adddate"].ToString();
            string username = Common.CookieHelper.GetCookieValue("username");
            string pageNum = context.Request.Params["pageNum"].ToString();
            string pageSize = context.Request.Params["pageSize"].ToString();
            string strwhere = "IsDel=0 and AddDate!='" + adddate + "' and Uname='" + username + "'";
            string strorder = "AddDate ASC";
            int startIndex =Convert.ToInt32(pageNum);
            int endIndex = Convert.ToInt32(pageSize);
           // RuRo.BLL.QueryRecoder bll = new BLL.QueryRecoder();
            RuRo.BLL.SZY.QueryRecoder bll_QueryRecoder = new BLL.SZY.QueryRecoder();
           // DataSet ds = bll.GetListByPage(strwhere,strorder,startIndex,endIndex);
            DataSet ds = bll_QueryRecoder.GetQueryRecoderTrue_bll(endIndex, startIndex, strwhere, strorder);//����ҳ���ȡ�б�
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<Model.QueryRecoder> list = bll_QueryRecoder.DataTableToList(ds.Tables[0]);//ת��ΪList
                Dictionary<string, string> dic = new Dictionary<string, string>();
                //List<Model.QueryRecoder> list = bll.GetModelList("IsDel=0 and AddDate!='" + adddate + "' and Uname='" + username + "' order by AddDate DESC");
                string kk = FreezerProUtility.Fp_Common.FpJsonHelper.ObjectToJsonStr(list);
                dic.Add("Qdata", kk);//��ӵ�DIC
                int count = bll_QueryRecoder.GetRecordCount("IsDel=0 and AddDate!='" + adddate + "' and Uname='" + username + "'");//��ȡ����ܼ�¼��
                dic.Add("total", count.ToString());//��ӵ�DIC
                string mes = FreezerProUtility.Fp_Common.FpJsonHelper.ObjectToJsonStr(dic);
                context.Response.Write(mes);
            }
            else
            { context.Response.Write(""); }
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

