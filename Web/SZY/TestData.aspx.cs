using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RuRo.Web
{
    public partial class TestData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BLL.Bll_ClinicalData b = new BLL.Bll_ClinicalData();
            //b.AddSample_Clinical_DataByDateQuery();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

 ////http://172.16.219.130/api?username=admin&password=admin&method=import_tests&test_data_type=Blood test&json={"Sample Source":”11q”,"cell count":5}
            //string data = "";
            //data = "[{\"Sample Source\": \"00149100\",\"姓名\": \"00149100张三\"},{ \"Sample Source\": \"00370717\",\"姓名\": \"00370717张三\"},{\"Sample Source\": \"2\",\"姓名\": \"2张三\"},{ \"Sample Source\": \"20030682\",\"姓名\": \"20030682张三\"}]";
            ////data = "{\"Sample Source\": \"00149100\",\"姓名\": \"1张三\"}";
            //BLL.TestData test = new BLL.TestData();
            //string result = test.ImportTestData("临床检验数据", data);
            //Response.Write(result);
        }
    }
}