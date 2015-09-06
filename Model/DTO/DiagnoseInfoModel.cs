namespace RuRo.Model.DTO
{
    public class DiagnoseInfoModel
    {
        //<RegisterNo>11111111</RegisterNo>
        //<Icd>Icd123456</Icd>
        //<Diagnose>诊断名称</Diagnose>
        //<Type>3</Type>
        //<Flag>1</Flag>
        //<DiagnoseDate>2012-09-01</DiagnoseDate>

        public string RegisterNo { get; set; }
        public string Icd { get; set; }
        public string Diagnose { get; set; }
        public string Type { get; set; }
        public string Flag { get; set; }
        public string DiagnoseDate { get; set; }
    }
}