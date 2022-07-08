namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProviderModel")]
    public partial class ProviderModel
    {
        [Key]
        public Guid ProviderId { get; set; }

        [StringLength(100)]
        public string ProviderCode { get; set; }

        [StringLength(1000)]
        public string ProviderName { get; set; }

        [StringLength(4000)]
        public string Address { get; set; }

        [StringLength(50)]
        public string AccountGroup { get; set; }

        public bool? isSAPData { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastEditedTime { get; set; }

        public bool? Actived { get; set; }
    }
}
