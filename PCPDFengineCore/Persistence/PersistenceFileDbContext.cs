using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PCPDFengineCore.Persistence.Records;
using PCPDFengineCore.RecordReader;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceFileDbContext : DbContext
    {
        private string _databasePath;
        private AccessMode _accessMode;

        public DbSet<FileInformation> FileInformation { get; set; }
        public DbSet<TextDelimitedRecordReader> TextDelimitedRecordReader { get; set; }
        public DbSet<TextFixedRecordReader> TextFixedRecordReader { get; set; }

        // TODO: Add the other record reader types

        public PersistenceFileDbContext(string databasePath, AccessMode accessMode)
        {
            _databasePath = databasePath;
            _accessMode = accessMode;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqliteOpenMode sqliteOpenMode = _accessMode == AccessMode.READ ? SqliteOpenMode.ReadOnly : SqliteOpenMode.ReadWriteCreate;

            string connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = _databasePath,
                Mode = sqliteOpenMode,
                Pooling = true,  // Disable connection pooling
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
