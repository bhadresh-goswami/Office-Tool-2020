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
    
    public partial class SessionMaster
    {
        public int SessionId { get; set; }
        public string SessionTitle { get; set; }
        public string SessionTopic { get; set; }
        public System.DateTime SessionDate { get; set; }
        public int RefBatchTitle { get; set; }
        public string PresentCandidates { get; set; }
    
        public virtual BatchMaster BatchMaster { get; set; }
    }
}
