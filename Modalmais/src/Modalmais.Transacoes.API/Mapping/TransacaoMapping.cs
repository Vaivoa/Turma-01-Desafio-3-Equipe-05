using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modalmais.Transacoes.API.Models;

namespace mercadolivre.Data.Mapping
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasMaxLength(36).IsRequired();
            builder.Property(e => e.Tipo).IsRequired().HasConversion<int>();
            builder.Property(e => e.StatusTransacao).IsRequired().HasConversion<int>();

            builder.Property(e => e.Valor).IsRequired().HasColumnType("decimal(8,2)");
            builder.Property(e => e.Chave).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Descricao).HasMaxLength(30);

            builder.ToTable("Transacoes", "modalmais");
        }
    }
}
