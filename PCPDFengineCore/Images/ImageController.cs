using PCPDFengineCore.Persistence;

namespace PCPDFengineCore.Images
{
    public class ImageController
    {

        private PersistenceController? persistenceController;
        private PersistenceController PersistenceController
        {
            get
            {
                if (persistenceController == null)
                {
                    throw new NullReferenceException("PersistenceController called before it is set.");
                }
                return persistenceController;
            }
        }

        public ImageController() { }

        public void SetPersistenceController(PersistenceController persistenceController)
        {
            this.persistenceController = persistenceController;
        }

        public byte[] GetImage(string filename)
        {
            ImageInfo? savedImage = PersistenceController.State.EmbeddedImages.Where(x => x.Filename == filename && x.Bytes != null).ToList().FirstOrDefault();

            if (savedImage != null)
            {
                return savedImage.Bytes!;
            }

            throw new ArgumentException($"Image: {filename} is not found.");
        }

        public void AddImageToState(string filePath)
        {

            ImageInfo image = new ImageInfo(new FileInfo(filePath).Name);
            image.Bytes = File.ReadAllBytes(filePath);

            PersistenceController.State.EmbeddedImages.Add(image);
        }

        public void RemoveImageFromState(string filename)
        {
            PersistenceController.State.EmbeddedImages.RemoveAll(x => x.Filename == filename);
        }
    }
}
