namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_sessions
    {
        public Guid? subid { get; set; }

        [Key]
        [Column(Order = 0)]
        public int session_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int agent_id { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }

        public int? duration { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int delivery_time { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int upload_time { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int download_time { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int schema_change_time { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prepare_snapshot_time { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal delivery_rate { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int time_remaining { get; set; }

        [Key]
        [Column(Order = 9)]
        public decimal percent_complete { get; set; }

        public int? upload_inserts { get; set; }

        public int? upload_updates { get; set; }

        public int? upload_deletes { get; set; }

        public int? upload_conflicts { get; set; }

        public int? upload_rows_retried { get; set; }

        public int? download_inserts { get; set; }

        public int? download_updates { get; set; }

        public int? download_deletes { get; set; }

        public int? download_conflicts { get; set; }

        public int? download_rows_retried { get; set; }

        public int? schema_changes { get; set; }

        public int? bulk_inserts { get; set; }

        public int? metadata_rows_cleanedup { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int runstatus { get; set; }

        public int? estimated_upload_changes { get; set; }

        public int? estimated_download_changes { get; set; }

        public int? connection_type { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] timestamp { get; set; }

        public int? current_phase_id { get; set; }

        public short? spid { get; set; }

        public DateTime? spid_login_time { get; set; }
    }
}
