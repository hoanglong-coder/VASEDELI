namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_genhistory
    {
        [Key]
        [Column(Order = 0)]
        public Guid guidsrc { get; set; }

        public Guid? pubid { get; set; }

        [Key]
        [Column(Order = 1)]
        public long generation { get; set; }

        public int? art_nick { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(1001)]
        public byte[] nicknames { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime coldate { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte genstatus { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int changecount { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int subscriber_number { get; set; }
    }
}
