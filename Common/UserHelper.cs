using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
   public   class UserHelper
    {
        #region 获取当前的用户名 +  private string GetUserName()
        /// <summary>
        /// 获取当前的用户名
        /// </summary>
        /// <returns></returns>
       public static string GetUserName()
        {
            string Username = "";
            Username = AccountHelper.GetActiveAccountUesrName()[0];
            return Username;
        }
        #endregion
    }
}
