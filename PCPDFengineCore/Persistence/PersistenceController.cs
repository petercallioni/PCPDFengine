using System.Text.Json;

namespace PCPDFengineCore.Persistence
{

    public enum AccessMode
    {
        READ,
        WRITE,
        CLOSED
    }
    public class PersistenceController
    {
        public void SaveState(PersistanceState state, string filePath)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new RecordReaderInterfaceConverter()
                }
            };
            string json = JsonSerializer.Serialize(state, serializeOptions);
            File.WriteAllText(filePath, json);

        }

        public PersistanceState LoadState(string filePath)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new RecordReaderInterfaceConverter()
                }
            };

            // options.IncludeFields = true;
            //options.Converters.Add(new TypeMappingConverter<IRecordReader, TextFixedRecordReader>());
            //options.Converters.Add(new TypeMappingConverter<IRecordReader, TextDelimitedRecordReader>());
            string json = File.ReadAllText(filePath);
            PersistanceState state = JsonSerializer.Deserialize<PersistanceState>(json, serializeOptions);
            return state;
        }
    }


}
//    public class PersistenceController : IPersistenceController
//    {
//        private PersistenceFileDbContext? _dbContext;
//        private string _dataBasePath;
//        private AccessMode _accessMode;
//        public AccessMode AccessMode { get => _accessMode; }

//        public void UpdateFileInformation(FileInformation fileInformation)
//        {
//            if (_dbContext == null)
//            {
//                return;
//            }

//            FileInformation? existing = _dbContext.FileInformation.FirstOrDefault();

//            if (existing != null)
//            {
//                _dbContext.FileInformation.Remove(existing);
//            }

//            _dbContext.FileInformation.Add(fileInformation);
//        }

//        public void LoadDatabase(string dataBasePath, bool clearExisting = false)
//        {
//            List<Action> fileLoadMethods = new List<Action>
//            {
//                () => LoadFileWriteAccess(dataBasePath),
//                () => LoadFileReadAccess(dataBasePath)
//            };

//            foreach (Action loadMethod in fileLoadMethods)
//            {
//                try
//                {
//                    loadMethod();

//                    if (_accessMode == AccessMode.WRITE)
//                    {
//                        if (clearExisting)
//                        {
//                            _dbContext.Database.EnsureDeleted();
//                        }

//                        _dbContext.Database.EnsureCreated();
//                    }

//                    break; // We have sucessfully opened the file
//                }
//                catch (IOException ex) when (ex.Message.Contains("being used by another process"))
//                {
//                    if (_accessMode == AccessMode.READ)
//                    {
//                        _accessMode = AccessMode.CLOSED;
//                        throw; // If we still can't access the file in read mode, something is wrong.
//                    }
//                }
//                catch
//                {
//                    _accessMode = AccessMode.CLOSED;
//                    throw;
//                }
//            }
//        }

//        public void SaveDatabase()
//        {
//            if (_dbContext != null)
//            {
//                _dbContext.SaveChanges();
//            }
//            else
//            {
//                throw new NullReferenceException("Attempted to save on null dbContext");
//            }
//        }

//        public void CloseDatabase()
//        {
//            if (_dbContext != null)
//            {
//                _accessMode = AccessMode.CLOSED;
//                _dbContext.Database.CloseConnection();
//                _dbContext.Dispose();
//                _dbContext = null;
//            }
//        }

//        public FileInformation? GetFileInformation()
//        {
//            if (_dbContext != null)
//            {
//                return _dbContext.FileInformation.FirstOrDefault();
//            }

//            return null;
//        }
//        // Seperate value that can be toggle to public for development.
//        private PersistenceFileDbContext? DbContext { get => _dbContext; }
//        public string DataBasePath { get => _dataBasePath; set => _dataBasePath = value; }

//        public void LoadFileWriteAccess(string databasePath)
//        {
//            _dbContext = new PersistenceFileDbContext(databasePath, AccessMode.WRITE);
//            _accessMode = AccessMode.WRITE;
//        }

//        public void LoadFileReadAccess(string databasePath)
//        {
//            _dbContext = new PersistenceFileDbContext(databasePath, AccessMode.READ);
//            _accessMode = AccessMode.READ;
//        }

//        public void UpdateRecordReader(IRecordReader recordReader)
//        {
//            foreach (TextDelimitedRecordReader entity in _dbContext.TextDelimitedRecordReader)
//                _dbContext.TextDelimitedRecordReader.Remove(entity);

//            foreach (TextFixedRecordReader entity in _dbContext.TextFixedRecordReader)
//                _dbContext.TextFixedRecordReader.Remove(entity);

//            if (recordReader is TextFixedRecordReader)
//            {
//                TextFixedRecordReader? existing = _dbContext.TextFixedRecordReader.FirstOrDefault();
//                _dbContext.TextFixedRecordReader.Add((TextFixedRecordReader)recordReader);
//            }
//            else if (recordReader is TextDelimitedRecordReader)
//            {
//                TextDelimitedRecordReader? existing = _dbContext.TextDelimitedRecordReader.FirstOrDefault();
//                _dbContext.TextDelimitedRecordReader.Add((TextDelimitedRecordReader)recordReader);
//            }
//            else
//            {
//                throw new NotImplementedException($"Type of {recordReader.GetType().Name} is not implemented in UpadeRecordReader");
//            }
//        }

//        public IRecordReader? GetRecordReader()
//        {
//            if (_dbContext != null)
//            {
//                List<IRecordReader?> recordReaders = new List<IRecordReader?>();

//                recordReaders.Add(_dbContext.TextDelimitedRecordReader.ToList().FirstOrDefault());
//                recordReaders.Add(_dbContext.TextFixedRecordReader.ToList().FirstOrDefault());
//                recordReaders.RemoveAll(x => x == null);

//                if (recordReaders.Count > 1)
//                {
//                    throw new InvalidDataException("There are two data readers defined. This should not happen.");
//                }

//                IRecordReader? firstReader = recordReaders.First();
//                return recordReaders.FirstOrDefault();
//            }

//            return null;
//        }
//    }
//}
