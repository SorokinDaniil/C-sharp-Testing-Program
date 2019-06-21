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
    public class SlideWindowViewModel : INotifyPropertyChanged, ISlideNavigationSubject
    {
        private int _selectedIndexListView;
        private readonly SlideNavigator _slideNavigator;
        private int _activeSlideIndex;
        static string TypeAccount;

        public SlideWindowViewModel(string typeAccount)
        {
            TypeAccount = typeAccount;

            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowMainWindowCommand, ShowMainWindowExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowTestingEditorCommand,ShowTestingEditorExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowThemeEditorCommand,ShowThemeEditorExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowTestingWindowCommand, ShowTestingWindowExecuted));

            if (TypeAccount == "User")
            {
                AdminSlides = new List<object>{ MainWindowViewModel };
            }
            if (TypeAccount == "Admin")
            {
                AdminSlides = new List<object> { MainWindowViewModel };
            }
            _slideNavigator = new SlideNavigator(this, AdminSlides);
            _slideNavigator.GoTo(0);//Задается начальное окно 
    
        }

        public List<object> AdminSlides { get; }

        public MainWindowViewModel MainWindowViewModel { get; } = new MainWindowViewModel(TypeAccount);

        //public TestingEditorViewModel TestingEditorViewModel { get; } = new TestingEditorViewModel();

        //public TestingWindowViewModel TestingWindowViewModel { get; } = new TestingWindowViewModel();

        //public ThemeEditorViewModel ThemeEditorViewModel { get; } = new ThemeEditorViewModel();

        private void ShowMainWindowExecuted(object sender, ExecutedRoutedEventArgs e)
        {


        }

        private void ShowTestingEditorExecuted(object sender, ExecutedRoutedEventArgs e)
        {


        }

        private void ShowThemeEditorExecuted(object sender, ExecutedRoutedEventArgs e)
        {


        }

        private void ShowTestingWindowExecuted(object sender, ExecutedRoutedEventArgs e)
        {


        }








        //private void ShowAdmin_Editor_TableChaphterEditExecuted(object sender, ExecutedRoutedEventArgs e)
        //{
        //    string SelectedValue = "";
        //    if (AdminSlides[ActiveSlideIndex].GetType() == typeof(ChoiceViewModel))
        //    {
        //        SelectedValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
        //    }

        //    //var IsCheck = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).IsCheckCollection;
        //    //var SelectValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
        //    //Console.WriteLine(IsCheck);
        //    //Console.WriteLine(SelectValue);
        //    _slideNavigator.GoTo(
        //        IndexOfSlide<MainTableViewModel>(),
        //        () => Admin_Editor_TableChaphterEdit.Show(SelectedValue));

        //}
        //private void ShowChoiceChaphterNoEditExecuted(object sender, ExecutedRoutedEventArgs e)
        //{
        //    _slideNavigator.GoTo(1);
        //    //_slideNavigator.GoTo(
        //    //    IndexOfSlide<ChoiceViewModel>(),
        //    //    () => ChoiceChaphterNoEdit.Show());
        //}

        //private void ShowMainTableExecuted(object sender, ExecutedRoutedEventArgs e)
        //{
        //    string SelectedValue = "";
        //    if (AdminSlides[ActiveSlideIndex].GetType() == typeof(ChoiceViewModel))
        //    {
        //        SelectedValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
        //    }

        //    //var IsCheck = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).IsCheckCollection;
        //    //var SelectValue = (AdminSlides[ActiveSlideIndex] as ChoiceViewModel).ChoiceValue;
        //    //Console.WriteLine(IsCheck);
        //    //Console.WriteLine(SelectValue);
        //    _slideNavigator.GoTo(
        //        IndexOfSlide<MainTableViewModel>(),
        //        () => Admin_Editor_TableChaphterEdit.Show(SelectedValue));
        //}

        //private void ShowTabControlExecuted(object sender, ExecutedRoutedEventArgs e)
        //{
        //    _slideNavigator.GoTo(2);
        //}

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
