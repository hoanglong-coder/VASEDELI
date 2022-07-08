namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VehicleModel")]
    public partial class VehicleModel
    {
        [Key]
        public Guid VehicleId { get; set; }

        public int? Type { get; set; }

        [StringLength(50)]
        public string VehicleNumber { get; set; }

        [StringLength(100)]
        public string VehicleOwner { get; set; }

        public Guid? DriverId { get; set; }

        [StringLength(100)]
        public string DriverName { get; set; }

        [StringLength(100)]
        public string DriverCardNo { get; set; }

        public decimal? VehicleWeight { get; set; }

        public int? isRoMooc { get; set; }

        public decimal? TrongLuongDangKiem { get; set; }

        public decimal? TyLeVuot { get; set; }

        public bool? isLock { get; set; }

        public bool? isLockEdit { get; set; }

        public Guid? CreatedUserId { get; set; }

        public DateTime? CreatedTime { get; set; }

        public Guid? LastEditUserId { get; set; }

        public DateTime? LastEditTime { get; set; }

        public decimal? TempWeight { get; set; }

        public DateTime? UpdateTempWeightTime { get; set; }

        public bool? isContainer { get; set; }

        public bool? isDauKeo { get; set; }
    }
}
