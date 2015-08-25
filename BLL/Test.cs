
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.BLL
{
    public class Test
    {
        public string PostTestData(BLL.UnameAndPwd up, string test_data_type, Dictionary<string, string> dataDicList)
        {
            string result = FreezerProUtility.Fp_BLL.TestData.ImportTestData(up.GetUp(), test_data_type, dataDicList);
            return result;
        }
        public string PostTestData(BLL.UnameAndPwd up,string test_data_type, string dataDicListStr)
        {
            string result = FreezerProUtility.Fp_BLL.TestData.ImportTestData(up.GetUp(), test_data_type, dataDicListStr);
            return result;
        }
    }
}
