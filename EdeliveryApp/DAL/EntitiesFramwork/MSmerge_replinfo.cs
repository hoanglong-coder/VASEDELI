namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_replinfo
    {
        [Key]
        [Column(Order = 0)]
        public Guid repid { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool use_interactive_resolver { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int validation_level { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long resync_gen { get; set; }

        [Key]
        [Column(Order = 4)]
        public string login_name { get; set; }

        [StringLength(128)]
        public string hostname { get; set; }

        [MaxLength(16)]
        public byte[] merge_jobid { get; set; }

        [Key]
        [Column(Order = 5)]
        public int sync_info { get; set; }
    }
}
