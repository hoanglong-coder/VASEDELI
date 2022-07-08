namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_dynamic_snapshots
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partition_id { get; set; }

        [StringLength(255)]
        public string dynamic_snapshot_location { get; set; }

        public DateTime? last_updated { get; set; }

        public DateTime? last_started { get; set; }

        public virtual MSmerge_partition_groups MSmerge_partition_groups { get; set; }
    }
}
