namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerModel")]
    public partial class CustomerModel
    {
        [Key]
        public Guid CustomerId { get; set; }

        [StringLength(100)]
        public string CustomerCode { get; set; }

        [StringLength(1000)]
        public string CustomerName { get; set; }

        [StringLength(4000)]
        public string Address { get; set; }

        public bool? isSAPData { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastEditedTime { get; set; }

        public bool? Actived { get; set; }

        public int? CungDuongCode { get; set; }

        [StringLength(500)]
        public string CungDuongName { get; set; }
    }
}
