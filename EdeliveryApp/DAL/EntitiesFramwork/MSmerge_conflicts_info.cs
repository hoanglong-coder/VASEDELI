namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_conflicts_info
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tablenick { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid rowguid { get; set; }

        [StringLength(255)]
        public string origin_datasource { get; set; }

        public int? conflict_type { get; set; }

        public int? reason_code { get; set; }

        [StringLength(720)]
        public string reason_text { get; set; }

        public Guid? pubid { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime MSrepl_create_time { get; set; }

        public Guid? origin_datasource_id { get; set; }
    }
}
