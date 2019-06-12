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
   //public class ThemeEditorViewModel : INotifyPropertyChanged
   // {
   //     byte EditThemeId;
   //     public ThemeEditorViewModel(string editThemeText, string editThemeTime , byte editThemeId)
   //     {
   //         TextTextBox = editThemeText;
   //         TextTimePicker = editThemeTime;
   //         EditThemeId = editThemeId;
   //     }


   //     private string _textTextBox;
   //     private string _textTimePicker;

   //     public string TextTextBox
   //     {
   //         get { return _textTextBox; }
   //         set
   //         {
   //             _textTextBox = value;

   //             OnPropertyChanged();
   //         }
   //     }

   //     public string TextTimePicker
   //     {
   //         get { return _textTimePicker; }
   //         set
   //         {
   //             _textTimePicker = value;

   //             OnPropertyChanged();
   //         }
   //     }

   //     public event PropertyChangedEventHandler PropertyChanged;

   //     protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
   //     {
   //         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   //     }
   // }

        public class ThemeEditorViewModel : INotifyPropertyChanged, ISlideNavigationSubject
        {
            private readonly SlideNavigator _slideNavigator;
            private int _activeSlideIndex;

        public ThemeEditorViewModel(string editThemeText, string editThemeTime, byte editThemeId)
        {
            TextTextBox = editThemeText;
            TextTimePicker = editThemeTime;
            EditThemeId = editThemeId;



            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.GoBackCommand, GoBackExecuted));
                CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(NavigationCommands.GoForwardCommand, GoForwardExecuted));

                if (TypeAccount == "User")
                {
                    AdminSlides = new object[] { ChoiceGroup, ChoiceChaphterNoEdit, Admin_Editor_TableChaphterEdit };
                }
                if (TypeAccount == "Admin")
                {
                    AdminSlides = new object[] { ChoiceChaphter, Admin_Editor_TableChaphterEdit, ChoiceGroup, ChoiceChaphterNoEdit };
                }
                _slideNavigator = new SlideNavigator(this, AdminSlides);
                //_slideNavigator.GoTo(1);//Задается начальное окно 
                /*Admin_Editor_TableChaphterEdit, Admin_ListStudent_TableListStudentEdit, Admin_ListStudent_TableTestNoEdit, Admin_ListStudent_TablePassedTestNoEdit , User_ListStudent_TableTestNoEdit ,*/ /* 
                    TestingWindowViewModel ,PreviewTestingWindowViewModel */
            }


        

            public object[] AdminSlides { get; }

            public TestingWindowViewModel TestingWindowViewModel { get; } = new TestingWindowViewModel();


            private void ShowChoiceGroupExecuted(object sender, ExecutedRoutedEventArgs e)
            {


            }

            private void ShowChoiceChaphterExecuted(object sender, ExecutedRoutedEventArgs e)
            {


            }

            private void ShowAdmin_Editor_TableChaphterEditExecuted(object sender, ExecutedRoutedEventArgs e)
            {
                string SelectedValue = "";
                if (AdminSlides[ActiveSlideIndex].GetType() == typeof(ChoiceViewModel))
                {
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
                if (AdminSlides[ActiveSlideIndex].GetType() == typeof(ChoiceViewModel))
                {
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

            private void GoBackExecuted(object sender, ExecutedRoutedEventArgs e)
            {
                _slideNavigator.GoTo(0);
                //_slideNavigator.GoBack();
            }

            private void GoForwardExecuted(object sender, ExecutedRoutedEventArgs e)
            {
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


