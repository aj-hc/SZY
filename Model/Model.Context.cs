﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SZY_FpExtendEntities : DbContext
    {
        public SZY_FpExtendEntities()
            : base("name=SZY_FpExtendEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<NormalLisReport> NormalLisReport { get; set; }
        public DbSet<PatientDiagnose> PatientDiagnose { get; set; }
        public DbSet<Sample_Clinical_Data> Sample_Clinical_Data { get; set; }
    }
}
