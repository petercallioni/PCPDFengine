using Microsoft.EntityFrameworkCore;
using PCPDFengineCore.Interfaces;
using PCPDFengineCore.Persistence.Records;

namespace PCPDFengineCore.Persistence
{
    public enum AccessMode
    {
        READ,
        WRITE,
        CLOSED
    }

    public class PersistenceController : IPersistenceController
    {
        private PersistenceFileDbContext? _dbContext;
        private string _dataBasePath;
        private AccessMode _accessMode;
        public AccessMode AccessMode { get => _accessMode; }

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
            List<Action> fileLoadMethods = new List<Action>
            {
                () => LoadFileWriteAccess(dataBasePath),
                () => LoadFileReadAccess(dataBasePath)
            };

            foreach (Action loadMethod in fileLoadMethods)
            {
                try
                {
                    loadMethod();

                    if (_accessMode == AccessMode.WRITE)
                    {
                        if (clearExisting)
                        {
                            _dbContext.Database.EnsureDeleted();
                        }

                        _dbContext.Database.EnsureCreated();
                    }

                    break; // We have sucessfully opened the file
                }
                catch (IOException ex) when (ex.Message.Contains("being used by another process"))
                {
                    if (_accessMode == AccessMode.READ)
                    {
                        _accessMode = AccessMode.CLOSED;
                        throw; // If we still can't access the file in read mode, something is wrong.
                    }
                }
                catch
                {
                    _accessMode = AccessMode.CLOSED;
                    throw;
                }
            }
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
                _accessMode = AccessMode.CLOSED;
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
        // Seperate value that can be toggle to public for development.
        private PersistenceFileDbContext? DbContext { get => _dbContext; }
        public string DataBasePath { get => _dataBasePath; set => _dataBasePath = value; }

        public void LoadFileWriteAccess(string databasePath)
        {
            _dbContext = new PersistenceFileDbContext(databasePath, AccessMode.WRITE);
            _accessMode = AccessMode.WRITE;
        }

        public void LoadFileReadAccess(string databasePath)
        {
            _dbContext = new PersistenceFileDbContext(databasePath, AccessMode.READ);
            _accessMode = AccessMode.READ;
        }
    }
}
