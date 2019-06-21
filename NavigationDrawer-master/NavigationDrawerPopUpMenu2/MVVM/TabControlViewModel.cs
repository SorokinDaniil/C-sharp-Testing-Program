using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;


namespace TestingProgram
{
    public class TabControlViewModel : INotifyPropertyChanged, ISlideNavigationSubject
    {
     
        private readonly SlideNavigator _slideNavigator;
        private int _activeSlideIndex;
        private bool _ischeckedonetab;
        private bool _ischeckedtwotab;
        string SelectedValueGroup;
        string SelectedValueChaphter;

        public TabControlViewModel()
        {
            //CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterCommand, ShowAdmin_ListStudent_TableListStudentEditExecuted));
            //CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowAdmin_Editor_TableChaphterEditCommand, ShowAdmin_ListStudent_TableTestNoEditExecuted));
            TabControlSlides = new List<object> { Admin_ListStudent_TableTestNoEdit  , Admin_ListStudent_TableListStudentEdit,  };
            _slideNavigator = new SlideNavigator(this, TabControlSlides);
            IsCheckedOneTab = true;
            _slideNavigator.GoTo(0);//Задается начальное окно 
        }

        public List<object> TabControlSlides { get; }

        public MainTableViewModel Admin_ListStudent_TableListStudentEdit { get; } = new MainTableViewModel("Admin_ListStudent_TableListStudentEdit");

        public MainTableViewModel Admin_ListStudent_TableTestNoEdit { get; } = new MainTableViewModel("Admin_ListStudent_TableTestNoEdit");
    
     
        private void ShowAdmin_ListStudent_TableListStudentEditExecuted()
        {
            _slideNavigator.GoTo(
                IndexOfSlide<MainTableViewModel>(),
                () => Admin_ListStudent_TableListStudentEdit.Show(SelectedValueGroup, ""));
        }

        private void ShowAdmin_ListStudent_TableTestNoEditExecuted()
        {
            _slideNavigator.GoTo(
               IndexOfSlide<MainTableViewModel>(),
               () => Admin_ListStudent_TableTestNoEdit.Show(SelectedValueChaphter, ""));
        }

        private RelayCommand _tabcontrolcommand;
        public RelayCommand TabControlCommand
        {
            get
            {
                return _tabcontrolcommand ??
                    (_tabcontrolcommand = new RelayCommand(obj =>
                    {
                        if (IsCheckedOneTab == true)
                        {
                            ShowAdmin_ListStudent_TableTestNoEditExecuted();
                        }
                        if (IsCheckedTwoTab == true)
                        {
                            ShowAdmin_ListStudent_TableListStudentEditExecuted();
                        }
                    }));
            }
        }

      public  void Show(string selectedvaluegroup, string selectedvaluechaphter)
        {
            SelectedValueGroup = selectedvaluegroup;
            SelectedValueChaphter = selectedvaluechaphter;

            ShowAdmin_ListStudent_TableTestNoEditExecuted();
            //IsCheckedOneTab = true;
            //ShowAdmin_ListStudent_TableListStudentEditExecuted(selectedvaluegroup);

        }



     

        public bool IsCheckedTwoTab
        {
            get { return _ischeckedtwotab; }
            set
            {
                _ischeckedtwotab = value;

                RaisePropertyChanged();
            }
        }

        public bool IsCheckedOneTab
        {
            get { return _ischeckedonetab; }
            set
            {
                _ischeckedonetab = value;

                RaisePropertyChanged();
            }
        }

        public int ActiveSlideIndex
        {
            get { return _activeSlideIndex; }
            set { this.MutateVerbose(ref _activeSlideIndex, value, RaisePropertyChanged()); }
        }

        private int IndexOfSlide<TSlide>()
        {
            return TabControlSlides.Select((o, i) => new { o, i }).First(a => a.o.GetType() == typeof(TSlide)).i;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}



//namespace TestingProgram
//{
//   public class TabControlViewModel : INotifyPropertyChanged
//    {

//        private int _selectedTabIndex;
//        public MainTableViewModel Admin_ListStudent_TableListStudentEdit_ViewModel = new MainTableViewModel("Admin_ListStudent_TableListStudentEdit");
//        public MainTableViewModel Admin_ListStudent_TableTestNoEdit_ViewModel = new MainTableViewModel("Admin_ListStudent_TableTestNoEdit");
//        MainTable Admin_ListStudent_TableListStudentEdit;
//        MainTable Admin_ListStudent_TableTestNoEdit;

//      public TabControlViewModel()
//        {
//             Admin_ListStudent_TableListStudentEdit = new MainTable() { DataContext = Admin_ListStudent_TableListStudentEdit_ViewModel };
//             Admin_ListStudent_TableTestNoEdit = new MainTable() { DataContext = Admin_ListStudent_TableTestNoEdit_ViewModel };
//        }

//        public void Show(string selectedvaluegroup, string selectedvaluechaphter)
//        {
//            SelectedTabIndex = 0;
//            Admin_ListStudent_TableListStudentEdit_ViewModel.Show(selectedvaluegroup,"");
//            Admin_ListStudent_TableTestNoEdit_ViewModel.Show(selectedvaluechaphter,"");
//            //IsCheckChoice = ischeckchoice;
//            //SelecteValueChoice = selectevaluechoice;
//            //HeaderMainTable = SelecteValueChoice;
//            //Console.WriteLine(IsCheckChoice);
//            //Console.WriteLine(SelecteValueChoice);
//        }



//        public int SelectedTabIndex
//        {
//            get { return _selectedTabIndex; }
//            set
//            {
//                _selectedTabIndex = value;

//                OnPropertyChanged();
//            }
//        }



//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
