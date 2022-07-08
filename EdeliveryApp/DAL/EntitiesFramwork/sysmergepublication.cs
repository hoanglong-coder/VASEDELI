namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysmergepublication
    {
        [Key]
        [Column(Order = 0)]
        public string publisher { get; set; }

        [Key]
        [Column(Order = 1)]
        public string publisher_db { get; set; }

        [Key]
        [Column(Order = 2)]
        public string name { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public int? retention { get; set; }

        public byte? publication_type { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid pubid { get; set; }

        public Guid? designmasterid { get; set; }

        public Guid? parentid { get; set; }

        public byte? sync_mode { get; set; }

        public int? allow_push { get; set; }

        public int? allow_pull { get; set; }

        public int? allow_anonymous { get; set; }

        public int? centralized_conflicts { get; set; }

        public byte? status { get; set; }

        public byte? snapshot_ready { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool enabled_for_internet { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool dynamic_filters { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool snapshot_in_defaultfolder { get; set; }

        [StringLength(255)]
        public string alt_snapshot_folder { get; set; }

        [StringLength(255)]
        public string pre_snapshot_script { get; set; }

        [StringLength(255)]
        public string post_snapshot_script { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool compress_snapshot { get; set; }

        [StringLength(128)]
        public string ftp_address { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ftp_port { get; set; }

        [StringLength(255)]
        public string ftp_subdirectory { get; set; }

        [StringLength(128)]
        public string ftp_login { get; set; }

        [StringLength(524)]
        public string ftp_password { get; set; }

        public int? conflict_retention { get; set; }

        public int? keep_before_values { get; set; }

        public bool? allow_subscription_copy { get; set; }

        public bool? allow_synctoalternate { get; set; }

        [StringLength(500)]
        public string validate_subscriber_info { get; set; }

        [StringLength(128)]
        public string ad_guidname { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int backward_comp_level { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int max_concurrent_merge { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int max_concurrent_dynamic_snapshots { get; set; }

        public short? use_partition_groups { get; set; }

        [StringLength(500)]
        public string dynamic_filters_function_list { get; set; }

        [StringLength(128)]
        public string partition_id_eval_proc { get; set; }

        [Key]
        [Column(Order = 12)]
        public short publication_number { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int replicate_ddl { get; set; }

        [Key]
        [Column(Order = 14)]
        public bool allow_subscriber_initiated_snapshot { get; set; }

        [StringLength(128)]
        public string distributor { get; set; }

        [MaxLength(16)]
        public byte[] snapshot_jobid { get; set; }

        public bool? allow_web_synchronization { get; set; }

        [StringLength(500)]
        public string web_synchronization_url { get; set; }

        public bool? allow_partition_realignment { get; set; }

        [Key]
        [Column(Order = 15)]
        public byte retention_period_unit { get; set; }

        public int? decentralized_conflicts { get; set; }

        public int? generation_leveling_threshold { get; set; }

        [Key]
        [Column(Order = 16)]
        public bool automatic_reinitialization_policy { get; set; }
    }
}
