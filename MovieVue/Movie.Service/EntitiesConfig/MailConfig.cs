using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Movie.Service.Entities;

namespace Movie.Service.EntitiesConfig
{
    class MailConfig:EntityTypeConfiguration<MailEntity>
    {
        public MailConfig()
        {
            ToTable("Mail");
            Property(m => m.MailContext).IsMaxLength();
            Property(m => m.MailToUser).HasMaxLength(64);
            Property(m => m.MailFromUser).HasMaxLength(64);
            Property(m => m.MailTitle).HasMaxLength(64);
        }
    }
}
