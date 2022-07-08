namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SOLineModel")]
    public partial class SOLineModel
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string SOLine { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string SONumber { get; set; }

        [StringLength(100)]
        public string CompanyCode { get; set; }

        [StringLength(100)]
        public string CustomerCode { get; set; }

        [StringLength(1000)]
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string ProductCode { get; set; }

        [StringLength(1000)]
        public string ProductName { get; set; }

        public decimal? Qty { get; set; }

        [StringLength(3)]
        public string UNIT { get; set; }

        public decimal? OverTolerance { get; set; }

        public decimal? UnderTolerance { get; set; }

        public bool? isUnlimited { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DocumentDate { get; set; }

        [StringLength(35)]
        public string PONumber { get; set; }

        public bool? isComplete { get; set; }

        public decimal? SoLuongDaXuat { get; set; }

        public bool? isClosed { get; set; }

        [StringLength(50)]
        public string NhaMaySanXuat { get; set; }

        public Guid rowguid { get; set; }
    }
}
