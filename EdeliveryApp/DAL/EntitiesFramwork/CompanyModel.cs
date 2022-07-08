namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyModel")]
    public partial class CompanyModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyCode { get; set; }

        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string DbName { get; set; }
    }
}
