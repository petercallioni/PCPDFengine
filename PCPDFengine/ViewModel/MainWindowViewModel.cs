using PCPDFengine.Common;

namespace PCPDFengine.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainViewModel MainView;

        private object selectedViewModel;

        public object SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }

            set
            {
                selectedViewModel = value;
                RaisePropertyChangedEvent();
            }
        }

        public MainWindowViewModel()
        {
            MainView = new MainViewModel(this);
            selectedViewModel = MainView;
        }
    }
}