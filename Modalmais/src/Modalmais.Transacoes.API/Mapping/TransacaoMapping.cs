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

            builder.Property(e => e.Valor).IsRequired().HasColumnType("decimal(6,2)");
            builder.Property(e => e.Chave).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Descricao).HasMaxLength(30);


            builder.Ignore(e => e.Conta)
                .OwnsOne(e => e.Conta, v =>
                {
                    v.Property(vc => vc.Banco).IsRequired().HasMaxLength(3);
                    v.Property(vc => vc.Agencia).IsRequired().HasMaxLength(4);
                    v.Property(vc => vc.Numero).IsRequired().HasMaxLength(16);
                });


            builder.ToTable("Transacoes", "modalmais");
        }
    }
}
