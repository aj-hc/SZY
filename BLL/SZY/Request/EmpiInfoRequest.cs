namespace RuRo.BLL.Request
{
    public class EmpiInfoRequest : Request
    {
        // string request = string.Format("<Request><Mzhzyh>{0}</Mzhzyh><Mzzybz>{1}</Mzzybz></Request>", Mzhzyh, Mzzybz);
        public EmpiInfoRequest(string code, string codeType)
        {
            this.Code = code;
            this.CodeType = codeType;
        }

        public override void CreatRequest(bool quertByCode)
        {
            this.RequestStr.Add(string.Format("<Request><Mzhzyh>{0}</Mzhzyh><Mzzybz>{1}</Mzzybz></Request>", this.Code, this.CodeType));
        }
    }
}