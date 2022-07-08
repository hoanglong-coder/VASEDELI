namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_conflict_Edelivery_Replication_Edelivery_GNH
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string SONumber { get; set; }

        [StringLength(50)]
        public string SOItems { get; set; }

        [StringLength(50)]
        public string MADONVICUNGCAP { get; set; }

        public string TENDONVICUNGCAP { get; set; }

        [StringLength(50)]
        public string MAKHACHANG { get; set; }

        public string TENKHACHHANG { get; set; }

        [StringLength(50)]
        public string MAHANGHOA { get; set; }

        public string TENHANGHOA { get; set; }

        public decimal? SOBC { get; set; }

        public decimal? SOCAY { get; set; }

        public decimal? SOCAYLE { get; set; }

        public decimal? TRONGLUONG { get; set; }

        public decimal? SOLUONGBEBO { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(50)]
        public string BranchCode { get; set; }

        public bool? IsBarem { get; set; }

        [StringLength(50)]
        public string UserLockId { get; set; }

        [StringLength(50)]
        public string SONumberBPM { get; set; }

        [StringLength(50)]
        public string NhaMaySanXuat { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid rowguid { get; set; }

        public Guid? origin_datasource_id { get; set; }
    }
}
