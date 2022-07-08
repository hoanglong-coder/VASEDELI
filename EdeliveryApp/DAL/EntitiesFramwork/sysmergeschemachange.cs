namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sysmergeschemachange")]
    public partial class sysmergeschemachange
    {
        [Key]
        [Column(Order = 0)]
        public Guid pubid { get; set; }

        public Guid? artid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int schemaversion { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid schemaguid { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int schematype { get; set; }

        [Key]
        [Column(Order = 4)]
        public string schematext { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte schemastatus { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int schemasubtype { get; set; }
    }
}
