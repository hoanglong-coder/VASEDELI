namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sysmergeschemaarticle
    {
        [Key]
        [Column(Order = 0)]
        public string name { get; set; }

        public byte? type { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int objid { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid artid { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public byte? pre_creation_command { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid pubid { get; set; }

        public byte? status { get; set; }

        [StringLength(255)]
        public string creation_script { get; set; }

        [MaxLength(8)]
        public byte[] schema_option { get; set; }

        [Key]
        [Column(Order = 4)]
        public string destination_object { get; set; }

        [StringLength(128)]
        public string destination_owner { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int processing_order { get; set; }
    }
}
