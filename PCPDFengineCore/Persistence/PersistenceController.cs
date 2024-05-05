using Microsoft.EntityFrameworkCore;
using PCPDFengineCore.Interfaces;
using PCPDFengineCore.Persistence.Records;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceController : IPersistenceController
    {
        private PersistenceFileDbContext? _dbContext;
        private string _dataBasePath;

        public void UpdateFileInformation(FileInformation fileInformation)
        {
            if (_dbContext == null)
            {
                return;
            }

            FileInformation? existing = _dbContext.FileInformation.FirstOrDefault();

            if (existing != null)
            {
                _dbContext.FileInformation.Remove(existing);
            }

            _dbContext.FileInformation.Add(fileInformation);
        }

        public void LoadDatabase(string dataBasePath, bool clearExisting = false)
        {
            _dbContext = new PersistenceFileDbContext(dataBasePath);

            if (clearExisting)
            {
                _dbContext.Database.EnsureDeleted();
            }

            _dbContext.Database.EnsureCreated();
        }

        public void SaveDatabase()
        {
            if (_dbContext != null)
            {
                _dbContext.SaveChanges();
            }
            else
            {
                throw new NullReferenceException("Attempted to save on null dbContext");
            }
        }

        public void CloseDatabase()
        {
            if (_dbContext != null)
            {
                _dbContext.Database.CloseConnection();
                _dbContext.Dispose();
                _dbContext = null;
            }
        }

        public FileInformation? GetFileInformation()
        {
            if (_dbContext != null)
            {
                return _dbContext.FileInformation.FirstOrDefault();
            }

            return null;
        }

        private PersistenceFileDbContext? DbContext { get => _dbContext; }
        public string DataBasePath { get => _dataBasePath; set => _dataBasePath = value; }
    }
}
