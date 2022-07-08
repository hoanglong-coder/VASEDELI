namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSrepl_errors
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime time { get; set; }

        public int? error_type_id { get; set; }

        public int? source_type_id { get; set; }

        [StringLength(100)]
        public string source_name { get; set; }

        [StringLength(128)]
        public string error_code { get; set; }

        [Column(TypeName = "ntext")]
        public string error_text { get; set; }

        [MaxLength(16)]
        public byte[] xact_seqno { get; set; }

        public int? command_id { get; set; }

        public int? session_id { get; set; }
    }
}
