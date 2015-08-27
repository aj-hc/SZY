using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RuRo.Web.Test
{
    public partial class PostTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string ip = i.Text.Trim();
            string un = u.Text.Trim();
            string pw = p.Text.Trim();
            string da = GetData();
            BLL.UnameAndPwd up = new BLL.UnameAndPwd(un, pw);
            BLL.Test test = new BLL.Test();
            r.InnerText = test.PostTestData(up, "临床检验数据", da);
        }
        private string GetData()
        {
            string da;
            da = d.Text.Trim();
            if (da == "1")
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                list.Add(new Dictionary<string, string>() { { "Sample Source", "21" }, { "姓名", "张三1" } });
                list.Add(new Dictionary<string, string>() { { "Sample Source", "21" }, { "姓名", "张三2" } });
                list.Add(new Dictionary<string, string>() { { "Sample Source", "21" }, { "姓名", "张三3" } });
                list.Add(new Dictionary<string, string>() { { "Sample Source", "21" }, { "姓名", "张三4" } });
                da = FreezerProUtility.Fp_Common.FpJsonHelper.ObjectToJsonStr(list);
            }
            else if (da == "2")
            {
                Dictionary<string, string> dic = new Dictionary<string, string>() { { "Sample Source", "21" }, { "姓名", "李四" } };
                da = FreezerProUtility.Fp_Common.FpJsonHelper.ObjectToJsonStr(dic);
            }
            else if (da == "3")
            {
                bool statu = false;


            }
            return da;
        }
        private bool GetBool(ref string str)
        {
            //判断字符串长度>5，并返回字符串+长度  111   111+3
            str+="+"+str.Length;
            
            return str.Length > 5;
        }
    }
}