﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DashReportingTool.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbReportingEntities : DbContext
    {
        public dbReportingEntities()
            : base("name=dbReportingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CandidateBatchMaster> CandidateBatchMasters { get; set; }
        public virtual DbSet<CandidateCourseMaster> CandidateCourseMasters { get; set; }
        public virtual DbSet<CandidateMaster> CandidateMasters { get; set; }
        public virtual DbSet<CourseMaster> CourseMasters { get; set; }
        public virtual DbSet<ExpertMaster> ExpertMasters { get; set; }
        public virtual DbSet<LocationMaster> LocationMasters { get; set; }
        public virtual DbSet<SessionMaster> SessionMasters { get; set; }
        public virtual DbSet<StatusMaster> StatusMasters { get; set; }
        public virtual DbSet<TechExpertMaster> TechExpertMasters { get; set; }
        public virtual DbSet<TechMaster> TechMasters { get; set; }
        public virtual DbSet<BatchMaster> BatchMasters { get; set; }
        public virtual DbSet<TaskTitleMaster> TaskTitleMasters { get; set; }
        public virtual DbSet<TaskMaster> TaskMasters { get; set; }
        public virtual DbSet<LoginLogMaster> LoginLogMasters { get; set; }
    }
}
