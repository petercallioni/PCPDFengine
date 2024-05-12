using PCPDFengineCore.Fonts;
using PCPDFengineCore.Persistence;

namespace PCPDFengineCore
{
    public class MasterController
    {
        PersistenceController persistenceController = new PersistenceController();
        FontController fontController = new FontController();

        public MasterController()
        {
            persistenceController.SetFontController(fontController);
            fontController.SetPersistenceController(persistenceController);
            fontController.LoadInstalledTtfFonts();
        }

        public FontController FontController { get => fontController; }
        public PersistenceController PersistenceController { get => persistenceController; }
    }
}
