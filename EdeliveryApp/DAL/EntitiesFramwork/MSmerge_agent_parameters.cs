namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSmerge_agent_parameters
    {
        [Key]
        [Column(Order = 0)]
        public string profile_name { get; set; }

        [Key]
        [Column(Order = 1)]
        public string parameter_name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string value { get; set; }
    }
}
