namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VehicleOwnerModel")]
    public partial class VehicleOwnerModel
    {
        [Key]
        [StringLength(100)]
        public string VehicleOwner { get; set; }

        [StringLength(1000)]
        public string VehicleOwnerName { get; set; }

        [StringLength(1000)]
        public string ProviderCode { get; set; }

        [StringLength(1000)]
        public string CustomerCode { get; set; }

        public bool? Actived { get; set; }
    }
}
