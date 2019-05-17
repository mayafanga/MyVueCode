using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Movie.Service.Entities;

namespace Movie.Service.EntitiesConfig
{
    class SettingConfig:EntityTypeConfiguration<SettingEntity>
    {
        public SettingConfig()
        {
            ToTable("Setting");
            Property(r => r.Name).HasMaxLength(32);
            Property(r => r.Value).IsMaxLength();
        }
    }
}
