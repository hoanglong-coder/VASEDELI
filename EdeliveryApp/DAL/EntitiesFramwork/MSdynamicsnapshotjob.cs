namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSdynamicsnapshotjob
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string name { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid pubid { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid job_id { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int agent_id { get; set; }

        [StringLength(128)]
        public string dynamic_filter_login { get; set; }

        [StringLength(128)]
        public string dynamic_filter_hostname { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string dynamic_snapshot_location { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partition_id { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool computed_dynsnap_location { get; set; }
    }
}
