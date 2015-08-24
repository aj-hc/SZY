using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.BLL
{
    public class UnameAndPwd
    {

        string username = string.Empty;
        string password = string.Empty;
        public UnameAndPwd()
        {
            username = Common.CookieHelper.GetCookieValue("username");
            string tempass = Common.CookieHelper.GetCookieValue("password");
            try
            {
                password = string.IsNullOrEmpty(tempass) ? "" : Common.DEncrypt.DESEncrypt.Decrypt(tempass);

            }
            catch (Exception ex)
            {
                Common.LogHelper.WriteError(ex);
            }
        }
        public  FreezerProUtility.Fp_Common.UnameAndPwd GetUp()
        {
            return new FreezerProUtility.Fp_Common.UnameAndPwd(this.username,this.password);
        }
    }
}
