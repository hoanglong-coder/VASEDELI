namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_altsyncpartners
    {
        [Key]
        [Column(Order = 0)]
        public Guid subid { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid alternate_subid { get; set; }

        [StringLength(255)]
        public string description { get; set; }
    }
}
