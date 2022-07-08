namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sysmergepartitioninfo")]
    public partial class sysmergepartitioninfo
    {
        [Key]
        [Column(Order = 0)]
        public Guid artid { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid pubid { get; set; }

        public int? partition_view_id { get; set; }

        public int? repl_view_id { get; set; }

        public string partition_deleted_view_rule { get; set; }

        public string partition_inserted_view_rule { get; set; }

        [StringLength(128)]
        public string membership_eval_proc_name { get; set; }

        public string column_list { get; set; }

        public string column_list_blob { get; set; }

        [StringLength(128)]
        public string expand_proc { get; set; }

        public int? logical_record_parent_nickname { get; set; }

        public int? logical_record_view { get; set; }

        public string logical_record_deleted_view_rule { get; set; }

        public bool? logical_record_level_conflict_detection { get; set; }

        public bool? logical_record_level_conflict_resolution { get; set; }

        public byte? partition_options { get; set; }
    }
}
