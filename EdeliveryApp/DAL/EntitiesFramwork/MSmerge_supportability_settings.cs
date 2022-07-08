namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_supportability_settings
    {
        public Guid? pubid { get; set; }

        public Guid? subid { get; set; }

        [StringLength(128)]
        public string web_server { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int support_options { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int log_severity { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int log_modules { get; set; }

        [StringLength(255)]
        public string log_file_path { get; set; }

        [StringLength(128)]
        public string log_file_name { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int log_file_size { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int no_of_log_files { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int upload_interval { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int delete_after_upload { get; set; }

        [StringLength(2048)]
        public string custom_script { get; set; }

        [StringLength(2000)]
        public string message_pattern { get; set; }

        public DateTime? last_log_upload_time { get; set; }

        public byte[] agent_xe { get; set; }

        public byte[] agent_xe_ring_buffer { get; set; }

        public byte[] sql_xe { get; set; }
    }
}
