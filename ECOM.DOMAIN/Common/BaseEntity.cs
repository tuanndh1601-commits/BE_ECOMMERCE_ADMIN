using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECOM.DOMAIN.Common
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        int? DeletedBy { get; set; }
        DateTime? DeletedAt { get; set; }
    }

    public abstract class BaseEntity<TId> : ISoftDelete
    {
        public TId Id { get; set; } = default!;

        public int? CreatedBy { get; set; } = 0;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
