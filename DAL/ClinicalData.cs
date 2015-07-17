using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    public class ClinicalData
    {
        //增删改查
        Model.SZY_FpExtendEntities sZY_FpExtendEntities = new Model.SZY_FpExtendEntities();

        //01.增加Sample_Clinical_Data记录
        public int AddSample_Clinical_Data(Model.Sample_Clinical_Data sample_Clinical_Data)
        {
            sZY_FpExtendEntities.Sample_Clinical_Data.Add(sample_Clinical_Data);
            return sZY_FpExtendEntities.SaveChanges();
        }
        //02.查找
        public List<Model.Sample_Clinical_Data> GetSample_Clinical_DataListBy(Expression<Func<Model.Sample_Clinical_Data, bool>> whereLambda)
        {
            return sZY_FpExtendEntities.Sample_Clinical_Data.Where(whereLambda).ToList();
        }
        //修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的属性名称</param>
        /// <returns></returns>
        public int Modify(Model.Sample_Clinical_Data model, params string[] proNames)
        {
            DbEntityEntry entry = sZY_FpExtendEntities.Entry<Model.Sample_Clinical_Data>(model);
            entry.State = System.Data.EntityState.Unchanged;
            foreach (string proName in proNames)
            {
                entry.Property(proName).IsModified = true;
            }
            return sZY_FpExtendEntities.SaveChanges();
        }
        //修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的属性名称</param>
        /// <returns>修改后成功的个数</returns>
        public int Modify<T>(T model, params string[] proNames) where T:class
        {
            DbEntityEntry entry = sZY_FpExtendEntities.Entry<T>(model);
            entry.State = System.Data.EntityState.Unchanged;
            foreach (string proName in proNames)
            {
                entry.Property(proName).IsModified = true;
            }
            return sZY_FpExtendEntities.SaveChanges();
        }

        public int AddNormalLisReport(Model.NormalLisReport normalLisReport)
        {
            sZY_FpExtendEntities.NormalLisReport.Add(normalLisReport);
            return sZY_FpExtendEntities.SaveChanges();
        }
        //02.查找
        public List<Model.NormalLisReport> GetNormalLisReporListBy(Expression<Func<Model.NormalLisReport, bool>> whereLambda)
        {
            return sZY_FpExtendEntities.NormalLisReport.Where(whereLambda).ToList();
        }
        public int AddPatientDiagnose(Model.PatientDiagnose patientDiagnose)
        {
            sZY_FpExtendEntities.PatientDiagnose.Add(patientDiagnose);
            return sZY_FpExtendEntities.SaveChanges();
        }
        public List<Model.PatientDiagnose> GetPatientDiagnoseListBy(Expression<Func<Model.PatientDiagnose, bool>> whereLambda)
        {
            return sZY_FpExtendEntities.PatientDiagnose.Where(whereLambda).ToList();
        }


    }
}
