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
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;


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
        private string _choicevalue;
        private bool _isCheckCollection;
        public List<Группа> _сhoiceGroupCollection { get; set; }
        public List<Раздел> _сhoiceChaphterCollection { get; set; }

        public ChoiceViewModel(string type)
        {
            
            switch (type)
                {
                    case "Chaphter":
                    LoadChaphterCollection();
                    IsCheckCollection = false;
                    ChoiceTextBlock = "Выберите раздел";
                        ChoiceHint = "Раздел";
                        ChoicePopupBox = Visibility.Visible; break;
                    case "ChaphterNoEdit":
                    IsCheckCollection = false;
                    LoadChaphterCollection();
                    ChoiceTextBlock = "Выберите раздел";
                        ChoiceHint = "Раздел";
                        ChoicePopupBox = Visibility.Hidden; break;
                    case "Group":
                    IsCheckCollection = true;
                    LoadGroupCollection();
                    ChoiceTextBlock = "Выберите группу";
                        ChoiceHint = "Группа";
                        ChoicePopupBox = Visibility.Visible; break;
                    default: break;
                }
           
        }

        private RelayCommand selectcommand;
        public RelayCommand SelectCommand
        {

            get
            {
                return selectcommand ??
                    (selectcommand = new RelayCommand(obj =>
                    {
                        //(obj as Button).SetBinding(Button.CommandProperty, new Binding("SaveReservationCommand"));
                       (obj as Button).Command = NavigationCommands.ShowMainTableCommand;
                        //Console.WriteLine(obj.GetType());
                    }));
            }
        }

      public void GetValues (out bool ischeckcollection ,out string choicevalue)
        {
            ischeckcollection = IsCheckCollection;
            choicevalue = ChoiceValue;
        }

        void LoadGroupCollection ()
        {
            using (TestEntities db = new TestEntities())
            {
                ChoiceGroupCollection = db.Группы.ToList();
            }
        }

        void LoadChaphterCollection ()
        {

            using (TestEntities db = new TestEntities())
            {
                ChoiceChaphterCollection = db.Разделы.ToList();
            }
        }

       

        #region Properties
        public bool IsCheckCollection
        {
            get { return _isCheckCollection; }
            set
            {
                if (_isCheckCollection != value)
                {
                    _isCheckCollection = value;
                    RaisePropertyChanged();
                }
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

 public string ChoiceValue
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

        public List<Группа> ChoiceGroupCollection
        {
            get { return _сhoiceGroupCollection; }
            set
            {
                if (_сhoiceGroupCollection != value)
                {
                    _сhoiceGroupCollection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public List<Раздел> ChoiceChaphterCollection
        {
            get { return _сhoiceChaphterCollection; }
            set
            {
                if (_сhoiceChaphterCollection != value)
                {
                    _сhoiceChaphterCollection = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SAMPLE 3

        public ICommand RunDialogAddGroupCommand => new AnotherCommandImplementation(AddGroupCommand);
        public ICommand RunDialogEditGroupCommand => new AnotherCommandImplementation(EditGroupCommand);
        public ICommand RunDialogDeleteGroupCommand => new AnotherCommandImplementation(DeleteGroupCommand);

        private async void AddGroupCommand(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:

            var view = new AddGroupDialog
            {
                DataContext = new AddGroupDialogViewModel( IsCheckCollection)
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void EditGroupCommand(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new EditGroupDialog
            {
                DataContext = new EditGroupDialogViewModel(ChoiceValue, IsCheckCollection)
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void DeleteGroupCommand(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new DeleteGroupDialog
            {
                DataContext = new DeleteGroupDialogViewModel(ChoiceValue, IsCheckCollection)
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            LoadGroupCollection();

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }






        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }




        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
