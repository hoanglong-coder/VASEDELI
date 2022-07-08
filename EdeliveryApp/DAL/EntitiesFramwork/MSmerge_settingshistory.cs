namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_settingshistory
    {
        public DateTime? eventtime { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid pubid { get; set; }

        public Guid? artid { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte eventtype { get; set; }

        [StringLength(128)]
        public string propertyname { get; set; }

        [StringLength(128)]
        public string previousvalue { get; set; }

        [StringLength(128)]
        public string newvalue { get; set; }

        [StringLength(2000)]
        public string eventtext { get; set; }
    }
}
