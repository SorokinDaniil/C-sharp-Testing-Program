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
        private int _selectedTabIndex;
        private string _choiceTextBlock;
        private string _choiceHint;
        private Visibility _choicePopupBox;
        private byte _choicevalue;
        public List<Группа> ChoiceGroupCollection { get; set; }
        public List<Раздел> ChoiceChaphterCollection { get; set; }

        public ChoiceViewModel(string type)
        {
            
            switch (type)
                {
                    case "Chaphter":
                    using (TestEntities db = new TestEntities())
                    {
                        ChoiceChaphterCollection = db.Разделы.ToList();
                    }
                    ChoiceTextBlock = "Выберите раздел";
                        ChoiceHint = "Раздел";
                        ChoicePopupBox = Visibility.Visible; break;
                    case "ChaphterNoEdit":
                    using (TestEntities db = new TestEntities())
                    {
                        ChoiceChaphterCollection = db.Разделы.ToList();
                    }
                    ChoiceTextBlock = "Выберите раздел";
                        ChoiceHint = "Раздел";
                        ChoicePopupBox = Visibility.Hidden; break;
                    case "Group":
                    using (TestEntities db = new TestEntities())
                    {
                        ChoiceGroupCollection = db.Группы.ToList();
                    }
                    ChoiceTextBlock = "Выберите группу";
                        ChoiceHint = "Группа";
                        ChoicePopupBox = Visibility.Visible; break;
                    default: break;
                }
           
        }

        public void Show()
        {
            SelectedTabIndex = 0;
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { this.MutateVerbose(ref _selectedTabIndex, value, RaisePropertyChanged()); }
        }

        public string ChoiceTextBlock
        {
            get { return _choiceTextBlock; }
            set
            {
                if (_choiceTextBlock != value)
                {
                    _choiceTextBlock = value;
                    RaisePropertyChanged();
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
                    RaisePropertyChanged();
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
                    RaisePropertyChanged();
                }
            }
        }

 public byte ChoiceValue
        {
            get { return _choicevalue; }
            set
            {
                if (_choicevalue != value)
                {
                    _choicevalue = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
