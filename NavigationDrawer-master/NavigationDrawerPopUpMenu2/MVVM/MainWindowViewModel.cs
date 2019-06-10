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

        public MainWindowViewModel(string typeAccount)
        {
            TypeAccount = typeAccount;
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceGroupCommand, ShowChoiceGroupExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterCommand, ShowChoiceChaphterExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterNoEditCommand, ShowChoiceChaphterNoEditExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowMainTableCommand, ShowMainTableExecuted));
     
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowTabControlCommand, ShowTabControlExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.GoBackCommand, GoBackExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.GoForwardCommand, GoForwardExecuted));

            if (TypeAccount == "User")
            {
                AdminSlides = new object[] { ChoiceGroup, ChoiceChaphterNoEdit, Admin_Editor_TableChaphterEdit };
            }
            if (TypeAccount == "Admin")
            {
                AdminSlides = new object[] { ChoiceGroup, ChoiceChaphterNoEdit, Admin_Editor_TableChaphterEdit };
            }

            _slideNavigator = new SlideNavigator(this, AdminSlides);
            _slideNavigator.GoTo(2);//Задается начальное окно 

            /*Admin_Editor_TableChaphterEdit, Admin_ListStudent_TableListStudentEdit, Admin_ListStudent_TableTestNoEdit, Admin_ListStudent_TablePassedTestNoEdit , User_ListStudent_TableTestNoEdit ,*/ /* 
                TestingWindowViewModel ,PreviewTestingWindowViewModel */
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
                                if ((AdminSlides[0] as ChoiceViewModel).ChoiceValue == null)
                                {
                                    _slideNavigator.GoTo(0);
                                }
                                else
                                if ((AdminSlides[1] as ChoiceViewModel).ChoiceValue == null)
                                {
                                    _slideNavigator.GoTo(1);
                                }
                                else
                                    _slideNavigator.GoTo(2);
                            }
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
            ////check the result...
            //Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }



        public object[] AdminSlides { get; }

        public TestingWindowViewModel TestingWindowViewModel { get; } = new TestingWindowViewModel();

        public PreviewTestingWindowViewModel PreviewTestingWindowViewModel { get; } = new PreviewTestingWindowViewModel();

        public ChoiceViewModel ChoiceChaphter { get; } = new ChoiceViewModel("Chaphter");

        public ChoiceViewModel ChoiceGroup { get; } = new ChoiceViewModel("Group");

        public ChoiceViewModel ChoiceChaphterNoEdit { get; } = new ChoiceViewModel("ChaphterNoEdit");

        public TabControlViewModel TabControl { get; } = new TabControlViewModel();

        public MainTableViewModel Admin_Editor_TableChaphterEdit { get; } = new MainTableViewModel("Admin_Editor_TableChaphterEdit");

        //public MainTableViewModel Admin_ListStudent_TableListStudentEdit { get; } = new MainTableViewModel("Admin_ListStudent_TableListStudentEdit");

        //public MainTableViewModel Admin_ListStudent_TableTestNoEdit { get; } = new MainTableViewModel("Admin_ListStudent_TableTestNoEdit");

        public MainTableViewModel Admin_ListStudent_TablePassedTestNoEdit { get; } = new MainTableViewModel("Admin_ListStudent_TablePassedTestNoEdit");

        public MainTableViewModel User_ListStudent_TableTestNoEdit { get; } = new MainTableViewModel("User_ListStudent_TableTestNoEdit");

        //public TabControlViewModel User_ListStudent_TableTestNoEdit { get; } = new TabControlViewModel("User_ListStudent_TableTestNoEdit");



        private void ShowChoiceGroupExecuted(object sender, ExecutedRoutedEventArgs e)
        {

         
        }

        private void ShowChoiceChaphterExecuted(object sender, ExecutedRoutedEventArgs e)
        {


        }

        private void ShowChoiceChaphterNoEditExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(1);
            //_slideNavigator.GoTo(
            //    IndexOfSlide<ChoiceViewModel>(),
            //    () => ChoiceChaphterNoEdit.Show());
        }

        private void ShowMainTableExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string SelectedValue = "";
            if (AdminSlides[ActiveSlideIndex].GetType()  ==  typeof(ChoiceViewModel)) {
                SelectedValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
            }
            
            //var IsCheck = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).IsCheckCollection;
            //var SelectValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
            //Console.WriteLine(IsCheck);
            //Console.WriteLine(SelectValue);
            _slideNavigator.GoTo(
                IndexOfSlide<MainTableViewModel>(),
                () => Admin_Editor_TableChaphterEdit.Show(SelectedValue));
        }

        private void ShowTabControlExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(2);
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

        private void GoBackExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoBack();
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
