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

using System.Collections.ObjectModel;
using DevExpress;
using System.Data.Entity;

namespace TestingProgram
{
    public class ThemeEditorViewModel : INotifyPropertyChanged, ISlideNavigationSubject
    {
        private readonly SlideNavigator _slideNavigator;
        private int _activeSlideIndex;
        private string _textTextBox;
        private string _textTimePicker;
        byte EditThemeId;

        public List<object> QuestionsSlides { get; set; }


        public ThemeEditorViewModel(string editThemeText, string editThemeTime, byte editThemeId)
        {
            TextTextBox = editThemeText;
            TextTimePicker = editThemeTime;
            EditThemeId = editThemeId;
            CommandManager.RegisterClassCommandBinding(typeof(ThemeEdit), new CommandBinding(NavigationCommands.GoBackQuestionCommand, GoBackQuestionExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(ThemeEdit), new CommandBinding(NavigationCommands.GoNextQuestionCommand, GoNextQuestionExecuted));
            QuestionsSlides = new List<object>();
            QuestionsSlides.Add(new QuestionCollection { });
            _slideNavigator = new SlideNavigator(this, QuestionsSlides); 
            _slideNavigator.GoTo(0);//Задается начальное окно 

        }

        private void GoBackQuestionExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(ActiveSlideIndex - 1);
        }

        private void GoNextQuestionExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoTo(ActiveSlideIndex + 1);

        }

        public string TextTextBox
        {
            get { return _textTextBox; }
            set { this.MutateVerbose(ref _textTextBox, value, RaisePropertyChanged()); }
        }

        public string TextTimePicker
        {
            get { return _textTimePicker; }
            set { this.MutateVerbose(ref _textTimePicker, value, RaisePropertyChanged()); }
        }

        public int ActiveSlideIndex
        {
            get { return _activeSlideIndex; }
            set { this.MutateVerbose(ref _activeSlideIndex, value, RaisePropertyChanged()); }
        }

        private int IndexOfSlide<TSlide>()
        {
            return QuestionsSlides.Select((o, i) => new { o, i }).First(a => a.o.GetType() == typeof(TSlide)).i;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }

    //public class ThemeEditorViewModel : INotifyPropertyChanged
    //{


    //public ThemeEditorViewModel(string editThemeText, string editThemeTime, byte editThemeId)
    //{
    //    TextTextBox = editThemeText;
    //    TextTimePicker = editThemeTime;
    //    EditThemeId = editThemeId;





    //        if (TypeAccount == "User")
    //        {
    //            QuestionsSlides = new object[] { ChoiceGroup, ChoiceChaphterNoEdit, Admin_Editor_TableChaphterEdit };
    //        }
    //        if (TypeAccount == "Admin")
    //        {
    //        
    //        }
    //    //        /*Admin_Editor_TableChaphterEdit, Admin_ListStudent_TableListStudentEdit, Admin_ListStudent_TableTestNoEdit, Admin_ListStudent_TablePassedTestNoEdit , User_ListStudent_TableTestNoEdit ,*/ /* 
    //            TestingWindowViewModel ,PreviewTestingWindowViewModel */
    //    }

    //    

    //    public TestingWindowViewModel TestingWindowViewModel { get; } = new TestingWindowViewModel();








    //}
}


