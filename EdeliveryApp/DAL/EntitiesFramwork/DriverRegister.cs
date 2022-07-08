namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DriverRegister")]
    public partial class DriverRegister
    {
        [Key]
        public Guid DriverId { get; set; }

        [StringLength(50)]
        public string DriverName { get; set; }

        [StringLength(50)]
        public string VehicleNumber { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public Guid? Creator { get; set; }

        [StringLength(50)]
        public string DriverCardNo { get; set; }

        public bool? Active { get; set; }

        public Guid? OwnerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string Place { get; set; }
    }
}
