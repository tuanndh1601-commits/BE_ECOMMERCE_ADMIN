using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECOM.DOMAIN.Entities
{
    public class DM_MenuGroup
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public int? SortOrder { get; set; } = 0;
        public bool? IsVisible { get; set; } = true;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
