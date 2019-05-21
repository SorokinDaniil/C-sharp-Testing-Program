using System;
using System.ComponentModel;

namespace TestingProgram
{
    public class TestingWindowViewModel : INotifyPropertyChanged
    {
       
        private int _selectedTabIndex;

        public void Show()
        {
        

            SelectedTabIndex = 0;
        
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { this.MutateVerbose(ref _selectedTabIndex, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
