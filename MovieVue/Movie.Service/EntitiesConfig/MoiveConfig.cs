using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Movie.Service.Entities;

namespace Movie.Service.EntitiesConfig
{
    class MoiveConfig:EntityTypeConfiguration<MovieEntity>
    {
        public MoiveConfig()
        {
            ToTable("Movie");
            //HasRequired(m => m.Comment).WithMany().HasForeignKey(m => m.CommentId).WillCascadeOnDelete(false);
            Property(m => m.MovieName).IsOptional().HasMaxLength(64);
            Property(m => m.MovieImg).IsOptional().HasMaxLength(256);
            Property(m => m.MoviePath).IsOptional().HasMaxLength(256);
            Property(m => m.MovieIntroduct).IsOptional().IsMaxLength();
            Property(m => m.MovieNumDowndload).IsOptional();
            Property(m => m.MovieNumSuppose).IsOptional();
            Property(m => m.MovieTime).IsOptional();

        }
    }
}
