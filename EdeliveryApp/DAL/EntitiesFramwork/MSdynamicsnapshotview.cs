namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MSdynamicsnapshotview
    {
        [Key]
        public string dynamic_snapshot_view_name { get; set; }
    }
}
