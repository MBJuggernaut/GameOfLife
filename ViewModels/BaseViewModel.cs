using System.ComponentModel;

namespace GameOfLife.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private BaseViewModel _selectedViewModel;
        private BaseViewModel _parentViewModel;

        public BaseViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                if (_parentViewModel != null)
                {
                    _parentViewModel.SelectedViewModel = value;
                }
                _selectedViewModel = value;

                OnPropertyChanged("SelectedViewModel");
            }
        }       
        public BaseViewModel ParentViewModel
        {
            get
            {
                return _parentViewModel;
            }
            set
            {
                _parentViewModel = value;
            }
        }

        public BaseViewModel(BaseViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

