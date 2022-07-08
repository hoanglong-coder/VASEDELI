namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysmergesubsetfilter
    {
        [Key]
        [Column(Order = 0)]
        public string filtername { get; set; }

        [Key]
        [Column(Order = 1)]
        public int join_filterid { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid pubid { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid artid { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int art_nickname { get; set; }

        [Key]
        [Column(Order = 5)]
        public string join_articlename { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int join_nickname { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int join_unique_key { get; set; }

        [StringLength(128)]
        public string expand_proc { get; set; }

        [StringLength(1000)]
        public string join_filterclause { get; set; }

        [Key]
        [Column(Order = 8)]
        public byte filter_type { get; set; }
    }
}
