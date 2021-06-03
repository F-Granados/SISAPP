using SISAP.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SISAP.Infrastructure.Data.Configuration
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            ToTable("Usuario", "dbo");
            HasKey(o => o.UsuarioId);
            Property(o => o.UsuarioId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
        }
       
    }
}
