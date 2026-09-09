using ItauChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ItauChallenge.Infra.Data;

public partial class ItauChallengeContext : DbContext
{
    public ItauChallengeContext()
    {
    }

    public ItauChallengeContext(DbContextOptions<ItauChallengeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ativo> Ativos { get; set; }

    public virtual DbSet<Cotacao> Cotacaos { get; set; }

    public virtual DbSet<Operacoes> Operacoes { get; set; }

    public virtual DbSet<Posicao> Posicaos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=itau_challenge;Username=postgres;Password=71321787");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ativo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ativo_pkey");

            entity.ToTable("ativo");

            entity.HasIndex(e => e.Codigo, "ativo_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Cotacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cotacao_pkey");

            entity.ToTable("cotacao");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AtivoId).HasColumnName("ativo_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DataHora)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("data_hora");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PrecoUnit)
                .HasPrecision(10, 3)
                .HasDefaultValue(0.00m)
                .HasColumnName("preco_unit");

            entity.HasOne(d => d.Ativo).WithMany(p => p.Cotacaos)
                .HasForeignKey(d => d.AtivoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("cotacao_ativo_id_fkey");
        });

        modelBuilder.Entity<Operacoes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("operacoes_pkey");

            entity.ToTable("operacoes");

            entity.HasIndex(e => new { e.UsuarioId, e.AtivoId, e.DataHora }, "idx_operacoes_user_ativo_data");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AtivoId).HasColumnName("ativo_id");
            entity.Property(e => e.Corretagem)
                .HasPrecision(10, 2)
                .HasDefaultValue(0.00m)
                .HasColumnName("corretagem");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DataHora)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("data_hora");
            entity.Property(e => e.PrecoUnit)
                .HasPrecision(10, 3)
                .HasDefaultValue(0.00m)
                .HasColumnName("preco_unit");
            entity.Property(e => e.QuantidadeB3)
                .HasDefaultValue(0)
                .HasColumnName("quantidade_b3");
            entity.Property(e => e.QuantidadeGlobal)
                .HasPrecision(18, 8)
                .HasDefaultValue(0.00m)
                .HasColumnName("quantidade_global");
            entity.Property(e => e.TipoOperacao)
                .HasMaxLength(1)
                .HasColumnName("tipo_operacao");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Ativo).WithMany(p => p.Operacos)
                .HasForeignKey(d => d.AtivoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("operacoes_ativo_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Operacos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("operacoes_usuario_id_fkey");
        });

        modelBuilder.Entity<Posicao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("posicao_pkey");

            entity.ToTable("posicao");

            entity.HasIndex(e => new { e.UsuarioId, e.AtivoId }, "posicao_usuario_id_ativo_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AtivoId).HasColumnName("ativo_id");
            entity.Property(e => e.Pl)
                .HasPrecision(10, 2)
                .HasDefaultValue(0.00m)
                .HasColumnName("pl");
            entity.Property(e => e.PrecoMedio)
                .HasPrecision(10, 2)
                .HasDefaultValue(0.00m)
                .HasColumnName("preco_medio");
            entity.Property(e => e.QuantidadeB3)
                .HasDefaultValue(0)
                .HasColumnName("quantidade_b3");
            entity.Property(e => e.QuantidadeGlobal)
                .HasPrecision(18, 8)
                .HasDefaultValue(0.00m)
                .HasColumnName("quantidade_global");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Ativo).WithMany(p => p.Posicaos)
                .HasForeignKey(d => d.AtivoId)
                .HasConstraintName("posicao_ativo_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Posicaos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("posicao_usuario_id_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "usuario_email_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Corretagem)
                .HasPrecision(10, 2)
                .HasDefaultValue(0.00m)
                .HasColumnName("corretagem");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .HasColumnName("nome");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
