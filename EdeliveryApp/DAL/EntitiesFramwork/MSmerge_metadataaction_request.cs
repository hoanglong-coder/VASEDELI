namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_metadataaction_request
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
        public byte action { get; set; }

        public long? generation { get; set; }

        public int? changed { get; set; }
    }
}
