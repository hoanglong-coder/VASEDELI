namespace DAL.EntitiesFramwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserModel")]
    public partial class UserModel
    {
        [Key]
        public Guid UserId { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string PasswordEnscrypt { get; set; }

        [StringLength(100)]
        public string RoldCode { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastEditedTime { get; set; }

        public bool? Actived { get; set; }

        [StringLength(255)]
        public string DeviceToken { get; set; }

        [StringLength(255)]
        public string GroupUser { get; set; }

        [StringLength(50)]
        public string CompanyCode { get; set; }

        [StringLength(500)]
        public string UserBPMCode { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }
    }
}
