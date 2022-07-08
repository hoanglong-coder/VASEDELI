namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysmergearticle
    {
        [Key]
        [Column(Order = 0)]
        public string name { get; set; }

        public byte? type { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int objid { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sync_objid { get; set; }

        public byte? view_type { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid artid { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public byte? pre_creation_command { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid pubid { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nickname { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int column_tracking { get; set; }

        public byte? status { get; set; }

        [StringLength(128)]
        public string conflict_table { get; set; }

        [StringLength(255)]
        public string creation_script { get; set; }

        [StringLength(255)]
        public string conflict_script { get; set; }

        [StringLength(255)]
        public string article_resolver { get; set; }

        [StringLength(128)]
        public string ins_conflict_proc { get; set; }

        [StringLength(128)]
        public string insert_proc { get; set; }

        [StringLength(128)]
        public string update_proc { get; set; }

        [StringLength(128)]
        public string select_proc { get; set; }

        [StringLength(128)]
        public string metadata_select_proc { get; set; }

        [StringLength(128)]
        public string delete_proc { get; set; }

        [MaxLength(8)]
        public byte[] schema_option { get; set; }

        [Key]
        [Column(Order = 7)]
        public string destination_object { get; set; }

        [StringLength(128)]
        public string destination_owner { get; set; }

        [StringLength(50)]
        public string resolver_clsid { get; set; }

        [StringLength(1000)]
        public string subset_filterclause { get; set; }

        public int? missing_col_count { get; set; }

        [MaxLength(128)]
        public byte[] missing_cols { get; set; }

        [MaxLength(128)]
        public byte[] excluded_cols { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int excluded_col_count { get; set; }

        [MaxLength(128)]
        public byte[] columns { get; set; }

        [MaxLength(128)]
        public byte[] deleted_cols { get; set; }

        [StringLength(517)]
        public string resolver_info { get; set; }

        [StringLength(290)]
        public string view_sel_proc { get; set; }

        public long? gen_cur { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int vertical_partition { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int identity_support { get; set; }

        public int? before_image_objid { get; set; }

        public int? before_view_objid { get; set; }

        public int? verify_resolver_signature { get; set; }

        [Key]
        [Column(Order = 11)]
        public bool allow_interactive_resolver { get; set; }

        [Key]
        [Column(Order = 12)]
        public bool fast_multicol_updateproc { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int check_permissions { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maxversion_at_cleanup { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int processing_order { get; set; }

        [Key]
        [Column(Order = 16)]
        public byte upload_options { get; set; }

        [Key]
        [Column(Order = 17)]
        public bool published_in_tran_pub { get; set; }

        [Key]
        [Column(Order = 18)]
        public bool lightweight { get; set; }

        [StringLength(32)]
        public string procname_postfix { get; set; }

        public bool? well_partitioned_lightweight { get; set; }

        public int? before_upd_view_objid { get; set; }

        public bool? delete_tracking { get; set; }

        [Key]
        [Column(Order = 19)]
        public bool compensate_for_errors { get; set; }

        public long? pub_range { get; set; }

        public long? range { get; set; }

        public int? threshold { get; set; }

        [Key]
        [Column(Order = 20)]
        public bool stream_blob_columns { get; set; }

        [Key]
        [Column(Order = 21)]
        public bool preserve_rowguidcol { get; set; }
    }
}
