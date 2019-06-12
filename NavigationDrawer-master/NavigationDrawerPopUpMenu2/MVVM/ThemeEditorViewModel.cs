using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using DevExpress;
using System.Data.Entity;

namespace TestingProgram
{
   public class ThemeEditorViewModel : INotifyPropertyChanged
    {
        public ThemeEditorViewModel(string editThemeText , string editThemeTime)
        {
            TextTextBox = editThemeText;
            TextTimePicker = editThemeTime;
        }


        private string _textTextBox;
        private string _textTimePicker;

        public string TextTextBox
        {
            get { return _textTextBox; }
            set
            {
                _textTextBox = value;

                OnPropertyChanged();
            }
        }

        public string TextTimePicker
        {
            get { return _textTimePicker; }
            set
            {
                _textTimePicker = value;

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
