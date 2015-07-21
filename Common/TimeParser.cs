using System;
using System.Collections.Generic;
using System.Text;

namespace RuRo.Common
{
    public class TimeParser
    {
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));           
        }

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >=1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        //计算时间的差值
        public static string DateDiffDays(DateTime DateTime1, DateTime dateNow)
        {
            #region note
            //C#中使用TimeSpan计算两个时间的差值
            //可以反加两个日期之间任何一个时间单位。
            TimeSpan ts3 = dateNow - DateTime1;
            //double dDays = ts.TotalDays;//带小数的天数，比如1天12小时结果就是1.5 
            int nDays = ts3.Days;//整数天数，1天12小时或者1天20小时结果都是1  
            return nDays.ToString();
            #endregion
        }
        #endregion
    }
}
