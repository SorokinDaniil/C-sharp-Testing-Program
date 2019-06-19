﻿using System;
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
using System.Windows;
using System.Collections.ObjectModel;
using DevExpress;
using System.Data.Entity;

namespace TestingProgram
{
    public class TestingWindowViewModel : INotifyPropertyChanged, ISlideNavigationSubject
    {
        private readonly SlideNavigator _slideNavigator;
        private int _activeSlideIndex;
        private string _textTextBox;
        private string _textTimePicker;
        private string _textShowCurrentSlide;
        private Visibility _visabilityLeftArrow;
        private Visibility _visabilityRightArrow;
        int NumberQuestion;
        byte EditThemeId;

        public List<object> QuestionsSlides { get; set; } = new List<object>();


        public TestingWindowViewModel(string editThemeText, string editThemeTime, byte editThemeId)
        {
            TextTimePicker = editThemeTime;
            EditThemeId = editThemeId;
            using (testEntities db = new testEntities())
            {
                NumberQuestion = db.Вопросы.Count(p => p.Тема_Id == EditThemeId);
            }

            CreateQuestionSlide(editThemeText);
            _slideNavigator = new SlideNavigator(this, QuestionsSlides);
            _slideNavigator.GoTo(0);//Задается начальное окно 
        }


        void CreateQuestionSlide(string editThemeText)
        {
            using (testEntities db = new testEntities())
            {
                TextShowCurrentSlide = $"{ActiveSlideIndex + 1}/{NumberQuestion}";
                var idtheme = db.Темы.SingleOrDefault(p => p.Название == editThemeText);//Получение id выбраного элемента
                IEnumerable<Вопрос> questions = db.Вопросы.Where(p => p.Тема_Id == idtheme.Id).Select(p => new { Текст = p.Текст, Код = p.Код, Тип_Ответа = p.Тип_Ответа, Id = p.Id })
.AsEnumerable()
.Select(an => new Вопрос
{
    Текст = an.Текст,
    Код = an.Код,
    Тип_Ответа = an.Тип_Ответа,
    Id = an.Id
});
                foreach (var question in questions)
                {
                    QuestionsSlides.Add(new QuestionsCollectionViewModel(question.Текст, question.Код, question.Тип_Ответа, question.Id));
                }
            }
        }

        #region Command

        private RelayCommand rightArrowCommand;
        public RelayCommand RightArrowCommand
        {

            get
            {
                return rightArrowCommand ??
                    (rightArrowCommand = new RelayCommand(obj =>
                    {
                        _slideNavigator.GoTo(ActiveSlideIndex + 1);
                        TextShowCurrentSlide = $"{ActiveSlideIndex + 1}/{NumberQuestion}";
                    }));
            }
        }

     
        #endregion
        #region Property
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

        public string TextShowCurrentSlide
        {
            get { return _textShowCurrentSlide; }
            set { this.MutateVerbose(ref _textShowCurrentSlide, value, RaisePropertyChanged()); }
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
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}


