//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CandidateBatchMaster
    {
        public int CBId { get; set; }
        public int RefCandidateCourseId { get; set; }
        public int RefBatchTitle { get; set; }
    
        public virtual CandidateCourseMaster CandidateCourseMaster { get; set; }
        public virtual BatchMaster BatchMaster { get; set; }
    }
}
