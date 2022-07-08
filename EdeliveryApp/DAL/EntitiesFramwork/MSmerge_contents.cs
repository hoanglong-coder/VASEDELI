namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_contents
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tablenick { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid rowguid { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long generation { get; set; }

        public long? partchangegen { get; set; }

        [Key]
        [Column(Order = 3)]
        [MaxLength(311)]
        public byte[] lineage { get; set; }

        [MaxLength(2953)]
        public byte[] colv1 { get; set; }

        public Guid? marker { get; set; }

        public Guid? logical_record_parent_rowguid { get; set; }

        [MaxLength(311)]
        public byte[] logical_record_lineage { get; set; }
    }
}
