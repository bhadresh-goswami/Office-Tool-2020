using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DashReportingTool.Models
{
    public class LoginModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }


        public int _GetId
        {
            get
            {
                dbReportingEntities db = new dbReportingEntities();
                ExpertMaster expert = db.ExpertMasters.SingleOrDefault(a=>a.ExpertEmailid == this.EmailId);
                if(expert==null)
                {
                    return 0;
                }
                this.Role = expert.Designation;
                return expert.ExpertId;
            }

        }

    }
}