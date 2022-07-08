namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_errorlineage
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tablenick { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid rowguid { get; set; }

        [MaxLength(311)]
        public byte[] lineage { get; set; }
    }
}
