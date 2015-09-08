using System;

namespace RuRo.BLL
{
    public class UnameAndPwd
    {
        private string username = string.Empty;
        private string password = string.Empty;

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

        public UnameAndPwd(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public FreezerProUtility.Fp_Common.UnameAndPwd GetUp()
        {
            return new FreezerProUtility.Fp_Common.UnameAndPwd(this.username, this.password);
        }
    }
}