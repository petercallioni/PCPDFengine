using PCPDFengine.Common;

namespace PCPDFengine.ViewModel
{
    public class DesignTimeViewModel : BaseViewModel
    {

        public DesignTimeViewModel()
        {
        }
    }

    public class MainViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel mainWindowView;

        public MainWindowViewModel MainWindowView { get => mainWindowView; }

        public MainViewModel(MainWindowViewModel mainWindowView)
        {
            this.mainWindowView = mainWindowView;
        }
    }
}
