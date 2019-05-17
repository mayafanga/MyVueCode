using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Movie.Service.Entities;

namespace Movie.Service.EntitiesConfig
{
    class UserConfig:EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            ToTable("User");
            Property(u => u.UserPhoto).HasMaxLength(256).IsOptional();
            Property(u => u.UserName).HasMaxLength(64);
            Property(u => u.Password).HasMaxLength(256);
            Property(u => u.UserMail).HasMaxLength(256);
            Property(u => u.UserPhone).HasMaxLength(11).IsRequired();
        }
    }
}
