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
using System.Windows;
using System.Collections.ObjectModel;
using DevExpress;
using System.Data.Entity;
using System.Windows.Threading;

namespace TestingProgram
{
    public class TestingWindowViewModel : INotifyPropertyChanged, ISlideNavigationSubject
    {
        private readonly SlideNavigator _slideNavigator;
        private int _activeSlideIndex;
        private string _textTextBox;
        private string _textTimePicker;
        private string _textShowCurrentSlide;
        double NumberQuestion;
        byte EditThemeId;
        DispatcherTimer _timer;
        TimeSpan _time;
        List<Вопрос> questions;
        double ValueScoreRadioButton;
        double ValueScoreCheckbox;
        double Mark;
        string LoginName;
        string ThemeText;

        public List<object> QuestionsSlides { get; set; } = new List<object>();


        public TestingWindowViewModel(string editThemeText, string editThemeTime, byte editThemeId,string loginname)
        {
            ThemeText = editThemeText;
            LoginName = loginname;
            TextTimePicker = editThemeTime;
            EditThemeId = editThemeId;
            using (testEntities db = new testEntities())
            {
                questions = db.Вопросы.Where(s => s.Тема_Id == EditThemeId).ToList();
                NumberQuestion = db.Вопросы.Count(p => p.Тема_Id == EditThemeId);
               ValueScoreRadioButton = 10 / NumberQuestion ;
                ValueScoreRadioButton =  Math.Round(ValueScoreRadioButton, 1);
            }

            CreateQuestionSlide(editThemeText);
             
            _slideNavigator = new SlideNavigator(this, QuestionsSlides);
            _slideNavigator.GoTo(0);//Задается начальное окно 

            _time = TimeSpan.Parse(editThemeTime);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TextTimePicker = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
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
                    QuestionsSlides.Add(new QuestionsCollectionViewModel(question.Текст, question.Код, question.Тип_Ответа, question.Id,"User"));
                }
            }
        }

        #region Command

        public ICommand RunFinishDialogCommand => new AnotherCommandImplementation(Finish);

        private RelayCommand rightArrowCommand;
        public RelayCommand RightArrowCommand
        {

            get
            {
                return rightArrowCommand ??
                    (rightArrowCommand = new RelayCommand(obj =>
                    {
                        using (testEntities db = new testEntities())
                        {
                            var IdQuestion = questions[ActiveSlideIndex].Id;
                            List<Ответ> answers = db.Ответы.Where(s => s.Вопрос_Id == IdQuestion).ToList();
                            ValueScoreCheckbox = ValueScoreRadioButton / answers.Where(p => p.Значение == true).Count();
                            ValueScoreCheckbox = Math.Round(ValueScoreCheckbox, 1);

                            var children = (QuestionsSlides[ActiveSlideIndex] as QuestionsCollectionViewModel).AnswerStackPanel.Children.OfType<UIElement>().ToList();
                            for (int i = 0; i < children.Count; i++)
                            {
                               
                                if (answers[i].Значение == true)
                                {
                                    if (children[i].GetType() == typeof(RadioButton))
                                    {
                                        if((children[i] as RadioButton).IsChecked == true)
                                        {
                                            Mark += ValueScoreRadioButton;
                                        }
                                        //Console.WriteLine((children[i] as RadioButton).IsChecked);
                                    }
                                    else
                                    if (children[i].GetType() == typeof(CheckBox))
                                    {
                                        if ((children[i] as CheckBox).IsChecked == true)
                                        {
                                            Mark += ValueScoreCheckbox;
                                        }
                                 
                                    }
                                }
                                   
                            }
                            Console.WriteLine(Mark);
                        }
                        if (ActiveSlideIndex == QuestionsSlides.Count - 1)
                        {
                            _timer.Stop();
                            using (testEntities db = new testEntities())
                            {
                                var студент = db.Студенты.Where(s => s.Логин == LoginName).SingleOrDefault();
                                var chaphter = db.Темы.Where(s => s.Id == EditThemeId).SingleOrDefault();
                                Результат результат = new Результат() { Тема_Название = EditThemeId , Раздел_Название = chaphter.Раздел_Id, Оценка =(byte)Mark, Дата_Прохождения = DateTime.Now };
                                Студент_Результат студент_Результат = new Студент_Результат() { Результат_Id = результат.Id, Студент_Id = студент.Id };
                                db.Результаты.Add(результат);
                                db.Студент_Результат.Add(студент_Результат);
                                db.SaveChanges();
                            }
                            Console.WriteLine("ИТОГОВАЯ ОЦЕНКА " + Mark);
                            if (RunFinishDialogCommand.CanExecute(null))
                            {
                                RunFinishDialogCommand.Execute(null);
                            } 

                        }
                        else
                        {
                            _slideNavigator.GoTo(ActiveSlideIndex + 1);
                            TextShowCurrentSlide = $"{ActiveSlideIndex + 1}/{NumberQuestion}";
                        }
                      
                        
                    }));
              
            }
        }



        private async void Finish(object o)
        {
            var view = new FinishDialog
            {
                DataContext = new FinishDialogViewModel((int)Math.Round(Mark,0))
            };

            var result = await DialogHost.Show(view, "TestingDialog", ClosingEventHandler);
            if ((bool)result == true)
            {

                Application.Current.Windows[0].ShowDialog();
                Application.Current.Windows[1].Hide();
            }
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
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


