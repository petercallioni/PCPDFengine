using System.Windows.Input;

namespace PCPDFengine.Common
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> action;

        public DelegateCommand(Action<T> action)
        {
            this.action = action;
        }

        public void Execute(object? parameter)
        {
            if (action != null)
            {
                if (parameter != null)
                {
                    T castParameter = (T)parameter;
                    action(castParameter);
                }
            }
        }

        //Disables the command
        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public virtual bool CanExecute(object? parameter)
        {
            return IsEnabled;
        }

        //#pragma warning disable 67
        public event EventHandler? CanExecuteChanged;

        //#pragma warning restore 67
    }
}