namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysmergesubscription
    {
        [StringLength(128)]
        public string subscriber_server { get; set; }

        [Key]
        [Column(Order = 0)]
        public string db_name { get; set; }

        public Guid? pubid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int datasource_type { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid subid { get; set; }

        [Key]
        [Column(Order = 3)]
        [MaxLength(6)]
        public byte[] replnickname { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid replicastate { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte status { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int subscriber_type { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int subscription_type { get; set; }

        [Key]
        [Column(Order = 8)]
        public byte sync_type { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public float? priority { get; set; }

        public long? recgen { get; set; }

        public Guid? recguid { get; set; }

        public long? sentgen { get; set; }

        public Guid? sentguid { get; set; }

        public int? schemaversion { get; set; }

        public Guid? schemaguid { get; set; }

        public DateTime? last_validated { get; set; }

        public DateTime? attempted_validate { get; set; }

        public DateTime? last_sync_date { get; set; }

        public int? last_sync_status { get; set; }

        [StringLength(128)]
        public string last_sync_summary { get; set; }

        [Key]
        [Column(Order = 9)]
        public DateTime metadatacleanuptime { get; set; }

        public int? partition_id { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool cleanedup_unsent_changes { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int replica_version { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int supportability_mode { get; set; }

        [StringLength(128)]
        public string application_name { get; set; }

        [Key]
        [Column(Order = 13)]
        public int subscriber_number { get; set; }

        public DateTime? last_makegeneration_datetime { get; set; }
    }
}
