using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Movie.Service.Entities;

namespace Movie.Service.EntitiesConfig
{
    class CommentConfig:EntityTypeConfiguration<CommentEntity>
    {
        public CommentConfig()
        {
            ToTable("Comment");
            //HasRequired(c => c.Movies).WithMany().HasForeignKey(c => c.UserId).WillCascadeOnDelete(false);
            //HasRequired(c => c.Users).WithMany().HasForeignKey(c => c.MovieId).WillCascadeOnDelete(false);
            Property(c => c.Context).IsMaxLength();
            //Property(c => c.UserId).IsOptional();
            //Property(c => c.MovieId).IsOptional();
        }
    }
}
