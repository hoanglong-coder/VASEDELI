using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.EntitiesFramwork
{
    public partial class DbConnection : DbContext
    {
        public DbConnection()
            : base("name=DbConnection")
        {
        }

        public virtual DbSet<Barem> Barems { get; set; }
        public virtual DbSet<CompanyModel> CompanyModels { get; set; }
        public virtual DbSet<CustomerModel> CustomerModels { get; set; }
        public virtual DbSet<DK_Edelivery> DK_Edelivery { get; set; }
        public virtual DbSet<DriverRegister> DriverRegisters { get; set; }
        public virtual DbSet<Edelivery_GNH> Edelivery_GNH { get; set; }
        public virtual DbSet<LOG> LOGs { get; set; }
        public virtual DbSet<MSdynamicsnapshotview> MSdynamicsnapshotviews { get; set; }
        public virtual DbSet<MSmerge_dynamic_snapshots> MSmerge_dynamic_snapshots { get; set; }
        public virtual DbSet<MSmerge_partition_groups> MSmerge_partition_groups { get; set; }
        public virtual DbSet<ProviderModel> ProviderModels { get; set; }
        public virtual DbSet<SOLineModel> SOLineModels { get; set; }
        public virtual DbSet<sysreplserver> sysreplservers { get; set; }
        public virtual DbSet<UserModel> UserModels { get; set; }
        public virtual DbSet<Vehicle_VehicleOwner_Mapping> Vehicle_VehicleOwner_Mapping { get; set; }
        public virtual DbSet<VehicleInfoMapping> VehicleInfoMappings { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }
        public virtual DbSet<VehicleOwnerModel> VehicleOwnerModels { get; set; }
        public virtual DbSet<MSdynamicsnapshotjob> MSdynamicsnapshotjobs { get; set; }
        public virtual DbSet<MSmerge_agent_parameters> MSmerge_agent_parameters { get; set; }
        public virtual DbSet<MSmerge_altsyncpartners> MSmerge_altsyncpartners { get; set; }
        public virtual DbSet<MSmerge_articlehistory> MSmerge_articlehistory { get; set; }
        public virtual DbSet<MSmerge_conflict_Edelivery_Replication_DK_Edelivery> MSmerge_conflict_Edelivery_Replication_DK_Edelivery { get; set; }
        public virtual DbSet<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH> MSmerge_conflict_Edelivery_Replication_Edelivery_GNH { get; set; }
        public virtual DbSet<MSmerge_conflict_Edelivery_Replication_SOLineModel> MSmerge_conflict_Edelivery_Replication_SOLineModel { get; set; }
        public virtual DbSet<MSmerge_conflicts_info> MSmerge_conflicts_info { get; set; }
        public virtual DbSet<MSmerge_contents> MSmerge_contents { get; set; }
        public virtual DbSet<MSmerge_current_partition_mappings> MSmerge_current_partition_mappings { get; set; }
        public virtual DbSet<MSmerge_errorlineage> MSmerge_errorlineage { get; set; }
        public virtual DbSet<MSmerge_generation_partition_mappings> MSmerge_generation_partition_mappings { get; set; }
        public virtual DbSet<MSmerge_genhistory> MSmerge_genhistory { get; set; }
        public virtual DbSet<MSmerge_history> MSmerge_history { get; set; }
        public virtual DbSet<MSmerge_identity_range> MSmerge_identity_range { get; set; }
        public virtual DbSet<MSmerge_log_files> MSmerge_log_files { get; set; }
        public virtual DbSet<MSmerge_metadataaction_request> MSmerge_metadataaction_request { get; set; }
        public virtual DbSet<MSmerge_past_partition_mappings> MSmerge_past_partition_mappings { get; set; }
        public virtual DbSet<MSmerge_replinfo> MSmerge_replinfo { get; set; }
        public virtual DbSet<MSmerge_sessions> MSmerge_sessions { get; set; }
        public virtual DbSet<MSmerge_settingshistory> MSmerge_settingshistory { get; set; }
        public virtual DbSet<MSmerge_supportability_settings> MSmerge_supportability_settings { get; set; }
        public virtual DbSet<MSmerge_tombstone> MSmerge_tombstone { get; set; }
        public virtual DbSet<MSrepl_errors> MSrepl_errors { get; set; }
        public virtual DbSet<sysmergearticle> sysmergearticles { get; set; }
        public virtual DbSet<sysmergepartitioninfo> sysmergepartitioninfoes { get; set; }
        public virtual DbSet<sysmergepublication> sysmergepublications { get; set; }
        public virtual DbSet<sysmergeschemaarticle> sysmergeschemaarticles { get; set; }
        public virtual DbSet<sysmergeschemachange> sysmergeschemachanges { get; set; }
        public virtual DbSet<sysmergesubscription> sysmergesubscriptions { get; set; }
        public virtual DbSet<sysmergesubsetfilter> sysmergesubsetfilters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barem>()
                .Property(e => e.MANDT)
                .IsUnicode(false);

            modelBuilder.Entity<Barem>()
                .Property(e => e.VKORG)
                .IsUnicode(false);

            modelBuilder.Entity<Barem>()
                .Property(e => e.MACTHEP)
                .IsUnicode(false);

            modelBuilder.Entity<Barem>()
                .Property(e => e.PHITHEP)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyModel>()
                .Property(e => e.DbName)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.MaNoiGiaoNhan)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.MaDonViVanChuyen)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.BienSo_SoHieu)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.SONumber)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.SOItems)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.MaHangHoa)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.SOBC)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.SOCAY)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.SOCAYLE)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.TRONGLUONG)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.SOLUONGBEBO)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.Instance)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.BranchCode)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.SONumberBPM)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.NhaMaySanXuat)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.CreateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<DK_Edelivery>()
                .Property(e => e.CTLXH)
                .IsUnicode(false);

            modelBuilder.Entity<DriverRegister>()
                .Property(e => e.DriverCardNo)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.SONumber)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.SOItems)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.MADONVICUNGCAP)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.MAKHACHANG)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.MAHANGHOA)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.SOBC)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.SOCAY)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.SOCAYLE)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.TRONGLUONG)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.SOLUONGBEBO)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.BranchCode)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.UserLockId)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.SONumberBPM)
                .IsUnicode(false);

            modelBuilder.Entity<Edelivery_GNH>()
                .Property(e => e.NhaMaySanXuat)
                .IsUnicode(false);

            modelBuilder.Entity<LOG>()
                .Property(e => e.NAME)
                .IsFixedLength();

            modelBuilder.Entity<MSmerge_partition_groups>()
                .HasOptional(e => e.MSmerge_dynamic_snapshots)
                .WithRequired(e => e.MSmerge_partition_groups)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SOLineModel>()
                .Property(e => e.Qty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SOLineModel>()
                .Property(e => e.OverTolerance)
                .HasPrecision(3, 1);

            modelBuilder.Entity<SOLineModel>()
                .Property(e => e.UnderTolerance)
                .HasPrecision(3, 1);

            modelBuilder.Entity<SOLineModel>()
                .Property(e => e.SoLuongDaXuat)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SOLineModel>()
                .Property(e => e.NhaMaySanXuat)
                .IsUnicode(false);

            modelBuilder.Entity<UserModel>()
                .Property(e => e.UserBPMCode)
                .IsUnicode(false);

            modelBuilder.Entity<UserModel>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<VehicleModel>()
                .Property(e => e.VehicleWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<VehicleModel>()
                .Property(e => e.TempWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_articlehistory>()
                .Property(e => e.percent_complete)
                .HasPrecision(5, 2);

            modelBuilder.Entity<MSmerge_articlehistory>()
                .Property(e => e.relative_cost)
                .HasPrecision(12, 2);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.MaNoiGiaoNhan)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.MaDonViVanChuyen)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.BienSo_SoHieu)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.SONumber)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.SOItems)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.MaHangHoa)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.SOBC)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.SOCAY)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.SOCAYLE)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.TRONGLUONG)
                .HasPrecision(18, 3);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.SOLUONGBEBO)
                .HasPrecision(18, 3);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.Instance)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.BranchCode)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.SONumberBPM)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.NhaMaySanXuat)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.CreateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_DK_Edelivery>()
                .Property(e => e.CTLXH)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.SONumber)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.SOItems)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.MADONVICUNGCAP)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.MAKHACHANG)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.MAHANGHOA)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.SOBC)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.SOCAY)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.SOCAYLE)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.TRONGLUONG)
                .HasPrecision(18, 3);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.SOLUONGBEBO)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.BranchCode)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.UserLockId)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.SONumberBPM)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_Edelivery_GNH>()
                .Property(e => e.NhaMaySanXuat)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_SOLineModel>()
                .Property(e => e.Qty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_SOLineModel>()
                .Property(e => e.OverTolerance)
                .HasPrecision(3, 1);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_SOLineModel>()
                .Property(e => e.UnderTolerance)
                .HasPrecision(3, 1);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_SOLineModel>()
                .Property(e => e.SoLuongDaXuat)
                .HasPrecision(18, 3);

            modelBuilder.Entity<MSmerge_conflict_Edelivery_Replication_SOLineModel>()
                .Property(e => e.NhaMaySanXuat)
                .IsUnicode(false);

            modelBuilder.Entity<MSmerge_history>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<MSmerge_identity_range>()
                .Property(e => e.range_begin)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MSmerge_identity_range>()
                .Property(e => e.range_end)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MSmerge_identity_range>()
                .Property(e => e.next_range_begin)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MSmerge_identity_range>()
                .Property(e => e.next_range_end)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MSmerge_identity_range>()
                .Property(e => e.max_used)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MSmerge_replinfo>()
                .Property(e => e.merge_jobid)
                .IsFixedLength();

            modelBuilder.Entity<MSmerge_sessions>()
                .Property(e => e.delivery_rate)
                .HasPrecision(12, 2);

            modelBuilder.Entity<MSmerge_sessions>()
                .Property(e => e.percent_complete)
                .HasPrecision(5, 2);

            modelBuilder.Entity<MSmerge_sessions>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sysmergearticle>()
                .Property(e => e.schema_option)
                .IsFixedLength();

            modelBuilder.Entity<sysmergearticle>()
                .Property(e => e.procname_postfix)
                .IsFixedLength();

            modelBuilder.Entity<sysmergepublication>()
                .Property(e => e.snapshot_jobid)
                .IsFixedLength();

            modelBuilder.Entity<sysmergeschemaarticle>()
                .Property(e => e.schema_option)
                .IsFixedLength();

            modelBuilder.Entity<sysmergesubscription>()
                .Property(e => e.replnickname)
                .IsFixedLength();
        }
    }
}
