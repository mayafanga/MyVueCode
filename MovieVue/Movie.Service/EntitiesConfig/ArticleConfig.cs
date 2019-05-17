using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace Movie.Service.EntitiesConfig
{
    class ArticleConfig:EntityTypeConfiguration<Entities.ArticleEntity>
    {
        public ArticleConfig()
        {
            ToTable("Article");
            Property(u => u.ArticleContext).IsMaxLength().IsOptional();
            Property(u => u.ArticleTitle).HasMaxLength(32).IsOptional();
            Property(u => u.ArticleImg).HasMaxLength(64).IsOptional();
        }
    }
}
