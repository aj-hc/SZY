using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public  class RandomTest
    {
        public  string  CreatName()
        {
          string CnlastNames = "王陈张李周杨吴黄刘徐朱胡沈潘章程方孙郑金叶汪何马赵林蒋俞姚许丁施高余谢董汤钱卢江蔡宋曹邱罗杜郭戴洪唐袁夏童肖姜傅范顾梅盛吕诸邵陆彭韩倪雷郎梁楼万龚储鲍严葛华应冯项崔魏毛阮邹喻曾邓熊任陶费凌虞裘涂苏翁莫卞史季康管黎孔田单娄宣钟饶鲁廖于韦甘石孟柳祝胥殷舒褚薛白向邬尚竺查谈贾温游谭开伍庄成沙柏郝秦尉麻詹赖裴颜尹巴乐厉谷易段钮骆笪阎缪臧樊操卜丰文水兰包平乔伊有牟邢劳来求沃芮闵欧郏柯贺闻桂耿戚符蓝路阚滕霍上卫干支牛计车左申艾仲刑匡印吉宇安戎毕池纪过佘冷时束花迟邰卓宓宗官庞於明练苗茅郁冒洑相郤郦钦奚席晋晏柴聂宿密屠常鄂惠琚窦简蒿阙穆濮";

          Random r = new Random();
          string m = makeRndCnWords(r.Next(1, 2));
          return CnlastNames.Substring(r.Next(CnlastNames.Length),1)+m;
        }

        //按Unicode编码取出随机汉字: 
        public  string makeRndCnWords(int k)
        {
            string rs = "";
            Random rd = new Random();
            for (int i = 0; i < k; i++)
            {
                rs += (char)rd.Next(0x4E00, 0x9FA5);
            }
            return rs;
        }
        public  string CreatSex()
        {
            Random r = new Random();
            string[] s = new string[]{"男","女"};
            return s[r.Next(2)];
        }
        public  string CreatAge()
        {
            return (DateTime.Now.Year - CreatBirthday().Year).ToString();
        }
        public  DateTime CreatBirthday()
        {
            Random r = new Random();
            int year = r.Next(60);
            int month = r.Next(13);
            int day = r.Next(31);
            return DateTime.Now.AddYears(-year).AddMonths(-month).AddDays(-day);
        }
        public  string CreatNum()
        {
            Random r = new Random();
            return r.Next(100000, 999999).ToString();
        }
    }
}
