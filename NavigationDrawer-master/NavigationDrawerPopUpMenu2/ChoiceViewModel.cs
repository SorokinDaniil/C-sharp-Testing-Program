using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


namespace TestingProgram
{
    /// <summary>
    /// Логика взаимодействия для ChoiceGroup.xaml
    /// </summary>
    public partial class ChoiceViewModel : INotifyPropertyChanged
    {
        string _choiceTextBlock;
        string _choiceHint;
        Visibility _choicePopupBox;

        public ChoiceViewModel(string type)
        {
            switch (type)
            {
                case "Group":
                    _choiceTextBlock = "Выберите группу";
                    _choiceHint = "Группа";
                    _choicePopupBox = Visibility.Visible; break;
                case "Chaphter":
                    _choiceTextBlock = "Выберите раздел";
                    _choiceHint = "Раздел";
                    _choicePopupBox = Visibility.Visible; break;
                case "ChaphterNoEdit":
                    _choiceTextBlock = "Выберите раздел";
                    _choiceHint = "Раздел";
                    _choicePopupBox = Visibility.Hidden; break;
                default: break;
            }

        }

        public string ChoiceTextBlock
        {
            get { return _choiceTextBlock; }
            set
            {
                if (_choiceTextBlock != value)
                {
                    _choiceTextBlock = value;
                    OnPropertyChanged();
                }
            }
        }

        public Visibility ChoicePopupBox
        {
            get { return _choicePopupBox; }
            set
            {
                if (_choicePopupBox != value)
                {
                    _choicePopupBox = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ChoiceHint
        {
            get { return _choiceHint; }
            set
            {
                if (_choiceHint != value)
                {
                    _choiceHint = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
