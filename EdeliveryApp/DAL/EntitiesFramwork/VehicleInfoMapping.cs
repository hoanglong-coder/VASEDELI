namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VehicleInfoMapping")]
    public partial class VehicleInfoMapping
    {
        [Key]
        [Column(Order = 0)]
        public Guid VehicleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string VehicleOwner { get; set; }

        public Guid? RomoocId { get; set; }

        public Guid? DriverId { get; set; }
    }
}
