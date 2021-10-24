using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.Models
{
    public class AuditEntity
    {
        public int Id { get; set; }

        public string TableName { get; set; }

        public DateTime DateTime { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Action")]
        public string Action { get; set; }
    }
}
