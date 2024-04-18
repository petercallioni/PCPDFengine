namespace PCPDFengine.Common
{
    public abstract class BaseViewModel : ObservableObject
    {
        private bool isSelectable;

        public bool IsSelectable
        {
            get
            {
                return isSelectable;
            }
            set
            {
                isSelectable = value;
                RaisePropertyChangedEvent("IsSelectable");
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChangedEvent("IsSelected");
            }
        }
    }
}