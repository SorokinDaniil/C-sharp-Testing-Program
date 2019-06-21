using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;

namespace TestingProgram
{
    public class MainWindowViewModel : INotifyPropertyChanged, ISlideNavigationSubject
    {
        private int _selectedIndexListView;
        private readonly SlideNavigator _slideNavigator;
        private int _activeSlideIndex;
        string TypeAccount;
        string LoginName;

        public MainWindowViewModel(string typeAccount,string loginname)
        {
            TypeAccount = typeAccount;
            LoginName = loginname;
            //РЕДАКТОР
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterCommand, ShowChoiceChaphterExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowAdmin_Editor_TableChaphterEditCommand,ShowAdmin_Editor_TableChaphterEditExecuted));

           
            //ТЕСТЫ
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterNoEditUserCommand, ShowChoiceChaphterNoEditUserExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowUser_ListStudent_TableTestNoEditCommand, ShowUser_ListStudent_TableTestNoEditExecuted));

            //ЖУРНАЛ
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceGroupCommand, ShowChoiceGroupExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterNoEditAdminCommand, ShowChoiceChaphterNoEditAdminExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowTabControlCommand, ShowTabControlExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowAdmin_ListStudent_TablePassedTestNoEditCommand, ShowAdmin_ListStudent_TablePassedTestNoEditCommandExecuted));




            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowMainTableCommand, ShowMainTableExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.GoBackCommand, GoBackExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.GoForwardCommand, GoForwardExecuted));

            if (TypeAccount == "User")
            {
                AdminSlides = new List<object> { ChoiceChaphterNoEditUser, User_ListStudent_TableTestNoEdit };
            }
            if (TypeAccount == "Admin")
            {
                AdminSlides = new List<object> { ChoiceChaphter, Admin_Editor_TableChaphterEdit  , ChoiceGroup, ChoiceChaphterNoEditAdmin ,TabControl };
            }
            _slideNavigator = new SlideNavigator(this, AdminSlides);
            _slideNavigator.GoTo(0);//Задается начальное окно 
            SelectedIndexListView = 1;


            /*Admin_Editor_TableChaphterEdit, Admin_ListStudent_TableListStudentEdit, Admin_ListStudent_TableTestNoEdit, Admin_ListStudent_TablePassedTestNoEdit , User_ListStudent_TableTestNoEdit ,*/ /* 
                TestingWindowViewModel ,PreviewTestingWindowViewModel */
        }

        public List<object> AdminSlides { get; }

        //РЕДАКТОР
        public ChoiceViewModel ChoiceChaphter { get; } = new ChoiceViewModel("Chaphter");
        public MainTableViewModel Admin_Editor_TableChaphterEdit { get; } = new MainTableViewModel("Admin_Editor_TableChaphterEdit");

        //ТЕСТЫ
        public ChoiceViewModel ChoiceChaphterNoEditUser { get; } = new ChoiceViewModel("ChaphterNoEditUser");
        public MainTableViewModel User_ListStudent_TableTestNoEdit { get; } = new MainTableViewModel("User_ListStudent_TableTestNoEdit");

        //ЖУРНАЛ
        public ChoiceViewModel ChoiceGroup { get; } = new ChoiceViewModel("Group");
        public ChoiceViewModel ChoiceChaphterNoEditAdmin { get; } = new ChoiceViewModel("ChaphterNoEditAdmin");
        public TabControlViewModel TabControl { get; } = new TabControlViewModel();
        public MainTableViewModel Admin_ListStudent_TablePassedTestNoEdit { get; } = new MainTableViewModel("Admin_ListStudent_TablePassedTestNoEdit");



        //public PreviewTestingWindowViewModel PreviewTestingWindowViewModel { get; } = new PreviewTestingWindowViewModel();

        //public MainTableViewModel Admin_ListStudent_TableListStudentEdit { get; } = new MainTableViewModel("Admin_ListStudent_TableListStudentEdit");

        //public MainTableViewModel Admin_ListStudent_TableTestNoEdit { get; } = new MainTableViewModel("Admin_ListStudent_TableTestNoEdit");



        //public TabControlViewModel User_ListStudent_TableTestNoEdit { get; } = new TabControlViewModel("User_ListStudent_TableTestNoEdit");

        #region Редакор
        private void ShowChoiceChaphterExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(0);
        }

        private void ShowAdmin_Editor_TableChaphterEditExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string SelectedValue = "";
            if (AdminSlides[ActiveSlideIndex].GetType() == typeof(ChoiceViewModel))
            {
                SelectedValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
            }
            _slideNavigator.GoTo(
                IndexOfSlide<MainTableViewModel>(),
                () => Admin_Editor_TableChaphterEdit.Show(SelectedValue, LoginName));

        }
        #endregion

        #region Тесты
        private void ShowChoiceChaphterNoEditUserExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(0);
        }

        private void ShowUser_ListStudent_TableTestNoEditExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string SelectedValue = "";
            if (AdminSlides[ActiveSlideIndex].GetType() == typeof(ChoiceViewModel))
            {
                SelectedValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
            }
            _slideNavigator.GoTo(
                IndexOfSlide<MainTableViewModel>(),
                () => User_ListStudent_TableTestNoEdit.Show(SelectedValue,LoginName));
        }
        #endregion

        #region Журнал 
        private void ShowChoiceChaphterNoEditAdminExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(3);
        }

        private void ShowTabControlExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(
                IndexOfSlide<TabControlViewModel>(),
                () => TabControl.Show((AdminSlides[2] as ChoiceViewModel).ChoiceValue, (AdminSlides[3] as ChoiceViewModel).ChoiceValue));
        }

        private void ShowAdmin_ListStudent_TablePassedTestNoEditCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(
                IndexOfSlide<TabControlViewModel>(),
                () => Admin_ListStudent_TablePassedTestNoEdit.Show((AdminSlides[2] as ChoiceViewModel).ChoiceValue, (AdminSlides[3] as ChoiceViewModel).ChoiceValue),"");
        }

        #endregion












        private void ShowMainTableExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //string SelectedValue = "";
            //if (AdminSlides[ActiveSlideIndex].GetType() == typeof(ChoiceViewModel))
            //{
            //    SelectedValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
            //}
            //_slideNavigator.GoTo(
            //    IndexOfSlide<MainTableViewModel>(),
            //    () => Admin_Editor_TableChaphterEdit.Show(SelectedValue));
        }



        private void ShowChoiceGroupExecuted(object sender, ExecutedRoutedEventArgs e)
        {


        }
      

    

        //private void ShowSeasonExecuted(object sender, ExecutedRoutedEventArgs e)
        //{
        //    _slideNavigator.GoTo(
        //        IndexOfSlide<TestingWindowViewModel>(),
        //        () => TestingWindowViewModel.Show());
        //}

        //private void ShowRaceExecuted(object sender, ExecutedRoutedEventArgs e)
        //{
        //    _slideNavigator.GoTo(
        //        IndexOfSlide<PreviewTestingWindowViewModel>(),
        //        () => PreviewTestingWindowViewModel.Show());
        //}
        private void GoBackExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(0);
            //_slideNavigator.GoBack();
        }

        private void GoForwardExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //if (AdminSlides[ActiveSlideIndex + 1].GetType() == typeof(TabControl))
            //{
            //    string SelectedValueGroup;
            //    string SelectedValueChaphter;
            //    (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).GetValues(out SelectedValueGroup, out SelectedValueChaphter);
            //    //Console.WriteLine(SelectedValueGroup);
            //    //Console.WriteLine(SelectedValueChaphter);
            //    _slideNavigator.GoTo(
            //        IndexOfSlide<TabControlViewModel>(),
            //        () => TabControl.Show(SelectedValueGroup, SelectedValueChaphter));
            //}
            //else
            _slideNavigator.GoTo(ActiveSlideIndex + 1);
            //_slideNavigator.GoForward();//Не работает
        }

        private RelayCommand _selectedItemChangedCommand;
        public RelayCommand SelectedItemChangedCommand
        {
            get
            {
                return _selectedItemChangedCommand ??
                    (_selectedItemChangedCommand = new RelayCommand(obj =>
                    {
                        (obj as ToggleButton).IsChecked = false;
                        if (SelectedIndexListView == 0)
                        {
                            if (TypeAccount == "Admin")
                            {
                                _slideNavigator.GoTo(2);
                                //if ((AdminSlides[2] as ChoiceViewModel).ChoiceValue == null)
                                //{
                                //    _slideNavigator.GoTo(2);
                                //}
                                //else
                                //if ((AdminSlides[3] as ChoiceViewModel).ChoiceValue == null)
                                //{
                                //    _slideNavigator.GoTo(3);
                                //}
                                //else
                                //    _slideNavigator.GoTo(4);
                            }
                        }

                        if (SelectedIndexListView == 1)
                        {
                            if ((AdminSlides[1] as MainTableViewModel).SelecteValueChoice == "")
                            {
                                _slideNavigator.GoTo(0);
                            }
                            else
                                _slideNavigator.GoTo(1);
                        }

                        //Console.WriteLine(obj.GetType());
                        //if (SelectedIndexListView == 0)
                        //{
                        //    _slideNavigator.GoTo(0);
                        //}
                        //if (SelectedIndexListView == 1)
                        //{
                        //    _slideNavigator.GoTo(0);
                        //}
                        //if (SelectedIndexListView == 1) obj = false;
                        //Console.WriteLine(SelectedIndexListView);
                    }));
            }
        }

        #region Exit
        public ICommand RunDialogExitAppCommand => new AnotherCommandImplementation(ExitAppCommand);

        private async void ExitAppCommand(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            Console.WriteLine(o.GetType());
            var view = new ExitAppDialog
            {
                DataContext = new ExitAppDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            Console.WriteLine(result);
            if ((bool)result == true)
            {
                Login login = new Login();
                login.Show();
                (o as MainWindow).Close();
            }

        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }
        #endregion

        public int ActiveSlideIndex
        {
            get { return _activeSlideIndex; }
            set { this.MutateVerbose(ref _activeSlideIndex, value, RaisePropertyChanged()); }
        }

        public int SelectedIndexListView
        {
            get { return _selectedIndexListView; }
            set { this.MutateVerbose(ref _selectedIndexListView, value, RaisePropertyChanged()); }
        }

        private int IndexOfSlide<TSlide>()
        {
            return AdminSlides.Select((o, i) => new { o, i }).First(a => a.o.GetType() == typeof(TSlide)).i;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
