using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PCPDFengineCore.Persistence.Records;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceFileDbContext : DbContext
    {
        private string _databasePath;


        public DbSet<FileInformation> FileInformation { get; set; }

        public PersistenceFileDbContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = _databasePath,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Pooling = false,  // Disable connection pooling

            }.ToString();

            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RetrievedFile>().HasData(
            //    new RetrievedFile { DateUpdated = System.DateTime.Now, Name = "Cheese", Url = "TEST" },
            //    new RetrievedFile { DateUpdated = System.DateTime.Now, Name = "Cheese", Url = "TEST" });
        }
    }
}
