using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace TestingProgram
{
    
    public class SelectableViewModel : INotifyPropertyChanged
    {
        private string _twoColumnContent;
        private string _oneColumnContent;
        private string _threeColumnContent;

        public string OneColumnContent
        {
            get { return _oneColumnContent; }
            set
            {
                if (_oneColumnContent == value) return;
                _oneColumnContent = value;
                OnPropertyChanged();
            }
        }

        public string TwoColumnContent
        {
            get { return _twoColumnContent; }
            set
            {
                if (_twoColumnContent == value) return;
                _twoColumnContent = value;
                OnPropertyChanged();
            }
        }

        public string ThreeColumnContent
        {
            get { return _threeColumnContent; }
            set
            {
                if (_threeColumnContent == value) return;
                _threeColumnContent = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
