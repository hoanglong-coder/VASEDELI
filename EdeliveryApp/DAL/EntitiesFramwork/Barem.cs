namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Barem")]
    public partial class Barem
    {
        [Key]
        [StringLength(255)]
        public string Material { get; set; }

        [StringLength(50)]
        public string MANDT { get; set; }

        [StringLength(50)]
        public string VKORG { get; set; }

        [StringLength(50)]
        public string MACTHEP { get; set; }

        [StringLength(50)]
        public string PHITHEP { get; set; }

        [Column("BAREM")]
        public decimal? BAREM1 { get; set; }

        public int? CAY_BO { get; set; }
    }
}
