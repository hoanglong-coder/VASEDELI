namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_partition_groups
    {
        [Key]
        public int partition_id { get; set; }

        public short publication_number { get; set; }

        public long? maxgen_whenadded { get; set; }

        public bool? using_partition_groups { get; set; }

        public bool is_partition_active { get; set; }

        public virtual MSmerge_dynamic_snapshots MSmerge_dynamic_snapshots { get; set; }
    }
}
