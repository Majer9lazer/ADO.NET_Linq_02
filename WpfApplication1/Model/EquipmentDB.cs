namespace WpfApplication1.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EquipmentDB : DbContext
    {
        public EquipmentDB()
            : base("name=EquipmentDB")
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Timer> Timers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .Property(e => e.IP)
                .IsUnicode(false);
        }
    }
}
