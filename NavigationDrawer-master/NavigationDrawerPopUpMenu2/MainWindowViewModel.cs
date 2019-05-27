using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using System.Windows.Markup;

namespace TestingProgram
{
    public class MainWindowViewModel : INotifyPropertyChanged, ISlideNavigationSubject
    {
        private readonly SlideNavigator _slideNavigator;
        private int _activeSlideIndex;

        public MainWindowViewModel()
        {
          
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceGroupCommand, ShowChoiceGroupExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterCommand, ShowChoiceChaphterExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowChoiceChaphterNoEditCommand, ShowChoiceChaphterNoEditExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowRaceCommand, ShowRaceExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.ShowSeasonCommand, ShowSeasonExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.GoBackCommand, GoBackExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(System.Windows.Input.NavigationCommands.BrowseBack, GoBackExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(System.Windows.Input.NavigationCommands.BrowseForward, GoForwardExecuted));

            Slides = new object[] { ChoiceChaphter , ChoiceGroup , ChoiceChaphterNoEdit /* TestingWindowViewModel ,PreviewTestingWindowViewModel */};
            _slideNavigator = new SlideNavigator(this, Slides);
            _slideNavigator.GoTo(2);//Задается начальное окно 
        }

        public object[] Slides { get; }

        public TestingWindowViewModel TestingWindowViewModel { get; } = new TestingWindowViewModel();

        public PreviewTestingWindowViewModel PreviewTestingWindowViewModel { get; } = new PreviewTestingWindowViewModel();

        public ChoiceViewModel ChoiceChaphter { get; } = new ChoiceViewModel("Chaphter");

        public ChoiceViewModel ChoiceGroup { get; } = new ChoiceViewModel("Group");

        public ChoiceViewModel ChoiceChaphterNoEdit { get; } = new ChoiceViewModel("ChaphterNoEdit");

        private void ShowChoiceGroupExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(
                IndexOfSlide<ChoiceViewModel>(),
                () => ChoiceGroup.Show());
        }

        private void ShowChoiceChaphterExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(
                IndexOfSlide<ChoiceViewModel>(),
                () => ChoiceChaphter.Show());
        }

        private void ShowChoiceChaphterNoEditExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(
                IndexOfSlide<ChoiceViewModel>(),
                () => ChoiceChaphterNoEdit.Show());
        }

        private void ShowSeasonExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(
                IndexOfSlide<TestingWindowViewModel>(),
                () => TestingWindowViewModel.Show());
        }

        private void ShowRaceExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(
                IndexOfSlide<PreviewTestingWindowViewModel>(),
                () => PreviewTestingWindowViewModel.Show());
        }

        public int ActiveSlideIndex
        {
            get { return _activeSlideIndex; }
            set { this.MutateVerbose(ref _activeSlideIndex, value, RaisePropertyChanged()); }
        }

        private void GoBackExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoBack();
        }

        private void GoForwardExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoForward();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        private int IndexOfSlide<TSlide>()
        {
            return Slides.Select((o, i) => new { o, i }).First(a => a.o.GetType() == typeof(TSlide)).i;
        }
    }
}
