using System;

namespace Forms.Web.Models
{
    public class BaseEntity
    {
        
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
