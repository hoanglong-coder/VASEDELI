namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicle_VehicleOwner_Mapping
    {
        [Key]
        [Column(Order = 0)]
        public Guid VehicleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string VehicleOwner { get; set; }
    }
}
