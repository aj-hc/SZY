namespace RuRo.BLL
{
    public class Samples
    {
        //////创建数据层对象
        ////DataWithFP dataWithFP;
        /////// <summary>
        /////// 构造函数
        /////// </summary>
        /////// <param name="username">用户名</param>
        /////// <param name="password">密码</param>
        ////public Samples()
        ////{
        ////    dataWithFP = new DataWithFP();
        ////}
        //DataWithFP dataWithFP = new DataWithFP();
        ///// <summary>
        ///// 泛型方法处理从Fp中获取到的数据
        ///// </summary>
        ///// <typeparam name="T">返回集合参数的类型</typeparam>
        ///// <param name="fpMethod">调用的api方法</param>
        ///// <param name="param">调用方法的参数</param>
        ///// <param name="datawith">从fp返回值中取什么数据</param>
        ///// <returns>返回集合</returns>
        //private List<T> getdata<T>(FpMethod fpMethod, string param, string datawith)
        //{
        //    List<T> list = new List<T>();
        //    string str_Json = getdata(fpMethod, param);
        //    if (ValidationData.checkTotal(str_Json))
        //    {
        //        list = FpJsonHelper.JObjectToList<T>(datawith, str_Json);
        //    }
        //    return list;
        //}
        ////调用数据层方法获取数据
        //private string getdata(FpMethod fpMethod, string param)
        //{
        //    string str_Json = dataWithFP.getDateFromFp(fpMethod, param);
        //    return str_Json;
        //}
        ////样本类型 sample_types
        //public List<Model.SampleTypes> GetSample_Types()
        //{
        //    List<Model.SampleTypes> sample_TypesList = getdata<Model.SampleTypes>(FpMethod.sample_types, "", "SampleTypes");
        //    return sample_TypesList;
        //}
        ////样本自定义字段sample_userfields
        //public Dictionary<string, string> GetSample_UserFields(string sample_id)
        //{
        //    return new Dictionary<string, string>();
        //}
        ////根据日期查询样本samples_by_date  lists samples by date:# today, yesterday, week, month, 1/1/2008
        //public List<Model.Sample> GetSamples_By_Date(string date, string param)
        //{
        //    List<Model.Sample> sample_By_DateList = getdata<Model.Sample>(FpMethod.samples_by_date, string.Format("&date={0}&limit={1}", date, param), "Samples");
        //    return sample_By_DateList;
        //}
        ///// <summary>
        ///// 根据日期获取数据
        ///// </summary>
        ///// <param name="date">日期范围</param>
        ///// <returns>样本信息集合</returns>
        //public List<Model.Sample> GetSamples_By_Date(string date)
        //{
        //    List<Model.Sample> sample_By_DateList = new List<Model.Sample>();
        //    string data = getdata(FpMethod.samples_by_date, string.Format("&date={0}&limit=1", date));
        //    if (ValidationData.checkTotal(data))
        //    {
        //        string total = FpJsonHelper.GetStrFromJsonStr("Total", data);
        //        int limit = 0;
        //        if (int.TryParse(total, out limit))
        //        {
        //            if (0 < limit)
        //            {
        //                if (limit % 500 == 0)
        //                {
        //                    for (int i = 0; i < limit / 500; i++)
        //                    {
        //                        List<Model.Sample> tem_Sample_By_DateList = new List<Model.Sample>();
        //                        tem_Sample_By_DateList = getdata<Model.Sample>(FpMethod.samples_by_date, string.Format("&date={0}&sort=id&start={1}&limit={2}", date, 500 * i, 500), "Samples");
        //                        sample_By_DateList.AddRange(tem_Sample_By_DateList);
        //                    }
        //                }
        //                else
        //                {
        //                    int cishu = (int)limit / 500;
        //                    for (int i = 0; i < cishu; i++)
        //                    {
        //                        sample_By_DateList.AddRange(getdata<Model.Sample>(FpMethod.samples_by_date, string.Format("&date={0}&sort=id&start={1}&limit={2}", date, 500 * i, 500), "Samples"));
        //                    }
        //                    sample_By_DateList.AddRange(getdata<Model.Sample>(FpMethod.samples_by_date, string.Format("&date={0}&sort=id&start={1}&limit={2}", date, 500 * cishu, limit - 500 * cishu), "Samples"));
        //                }
        //            }

        //        }

        //    }

        //    return sample_By_DateList;
        //}

        ////获取出库的样本retrieves a list of samples that are taken out of the freezer:
        //public List<Model.Sample_Out> GetSamples_Out()
        //{
        //    List<Model.Sample_Out> sampleOutList = getdata<Model.Sample_Out>(FpMethod.samples_out, "", "Samples");

        //    return sampleOutList;
        //}
        ////获取删除的样本samples in the trashbin
        //public List<Model.Samples_Trashbin> GetSamples_Trashbin()
        //{
        //    List<Model.Samples_Trashbin> sampleLocationsList = getdata<Model.Samples_Trashbin>(FpMethod.samples_trashbin, "", "Locations");

        //    return sampleLocationsList;
        //}
        ////根据样本源id获取样本
        //public List<Model.Sample> GetSampleSource_Samples(string samplesource_id)
        //{
        //    List<Model.Sample> sampleSource_Sampleslist = getdata<Model.Sample>(FpMethod.samplesource_samples, string.Format("&id={0}", samplesource_id), "Samples");
        //    return sampleSource_Sampleslist;
        //}
        ////根据样本id获取样本的信息
        //public Model.Sample_Info GetSample_Info(string sample_id)
        //{
        //    Model.Sample_Info sample_info = new Model.Sample_Info();
        //    string strJson = dataWithFP.getDateFromFp(FpMethod.sample_info, string.Format("&id={0}", sample_id));
        //    sample_info = FpJsonHelper.JsonStrToObject<Model.Sample_Info>(strJson);
        //    return sample_info;
        //}
    }
}