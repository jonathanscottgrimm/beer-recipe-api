using Microsoft.EntityFrameworkCore;

namespace BeerApiBackend.Models
{
    public partial class BeerAppDbContext : DbContext
    {
        public BeerAppDbContext(DbContextOptions<BeerAppDbContext> options) :
            base(options)
        { }

        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<IngredientsInBeers> IngredientsInBeers { get; set; }
        public virtual DbSet<Recipes> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=BeerAppDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<IngredientsInBeers>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId });
            });

            modelBuilder.Entity<Recipes>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<IngredientsInBeers>()
              .HasOne(e => e.Recipes)
              .WithMany(e => e.IngredientsInBeers)
              .HasForeignKey(e => e.RecipeId);

            modelBuilder.Entity<IngredientsInBeers>()
                .HasOne(e => e.Ingredients)
                .WithMany(e => e.IngredientsInBeers)
                .HasForeignKey(e => e.IngredientId);
        }
    }
}
