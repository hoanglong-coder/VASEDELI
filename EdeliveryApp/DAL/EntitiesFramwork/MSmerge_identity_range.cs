namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_identity_range
    {
        [Key]
        [Column(Order = 0)]
        public Guid subid { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid artid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? range_begin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? range_end { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? next_range_begin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? next_range_end { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool is_pub_range { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? max_used { get; set; }
    }
}
