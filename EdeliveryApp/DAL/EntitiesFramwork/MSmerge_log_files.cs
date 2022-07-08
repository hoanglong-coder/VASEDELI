namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_log_files
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        public Guid? pubid { get; set; }

        public Guid? subid { get; set; }

        [StringLength(128)]
        public string web_server { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2000)]
        public string file_name { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime upload_time { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int log_file_type { get; set; }

        public byte[] log_file { get; set; }
    }
}
