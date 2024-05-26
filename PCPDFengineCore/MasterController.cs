using PCPDFengineCore.Composition;
using PCPDFengineCore.Fonts;
using PCPDFengineCore.Images;
using PCPDFengineCore.Persistence;

namespace PCPDFengineCore
{
    public class MasterController
    {
        PersistenceController persistenceController = new PersistenceController();
        FontController fontController = new FontController();
        ImageController imageController = new ImageController();
        PdfController pdfController = new PdfController();

        public MasterController()
        {
            persistenceController.SetFontController(fontController);
            persistenceController.SetImageController(imageController);

            fontController.SetPersistenceController(persistenceController);
            imageController.SetPersistenceController(persistenceController);
            pdfController.SetPersistenceController(persistenceController);

            fontController.LoadInstalledTtfFonts();
        }

        public FontController FontController { get => fontController; }
        public PersistenceController PersistenceController { get => persistenceController; }
        public ImageController ImageController { get => imageController; }
        public PdfController PdfController { get => pdfController; }
    }
}
