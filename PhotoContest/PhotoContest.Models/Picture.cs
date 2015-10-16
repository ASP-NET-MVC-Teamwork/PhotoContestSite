using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoContest.Models.Common;

namespace PhotoContest.Models
{
    class Picture : AuditInfo, IDeletableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; } 
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
