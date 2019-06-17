using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace TestingProgram
{
  public class TestingEditorViewModel : INotifyPropertyChanged
    {
        StackPanel AnswerStackPanel;
        byte EditThemeId;
        byte EditQuestionId;
        private string _codeQuestion;
        private string _textQuestion;
        private bool _isCheckCodeQuestion;
        private bool _isCheckedTestEditor;
        private int _selectedIndexChangeAnswer;
        private Visibility _codeBoxVisibility;
        string TypeQuestion;

        public TestingEditorViewModel (byte editthemeid , byte editquestionid)
        {
            EditThemeId = editthemeid;
            EditQuestionId = editquestionid;
            using (testEntities db = new testEntities())
            {
                var currentquestion = db.Вопросы.SingleOrDefault(p => p.Id == EditQuestionId);//Получение редактируемого вопроса
                TextQuestion = currentquestion.Текст;//Текст вопроса

                #region Код вопроса
                if (currentquestion.Код == null)
                {
                    IsCheckCodeQuestion = false;
                }
                else
                {
                    IsCheckCodeQuestion = true;
                    CodeQuestion = currentquestion.Код;
                }
                CheckVisabilityCodeBox();
                #endregion


                TypeQuestion  = currentquestion.Тип_Ответа;//Варианты ответов


                SelectedIndexChangeAnswer = -1;//Default


            }
        }

        public TestingEditorViewModel(byte editthemeid)
        {
            EditThemeId = editthemeid;

            TextQuestion = "";//Default
            IsCheckCodeQuestion = true;//Default
            CodeQuestion = "";//Default


            SelectedIndexChangeAnswer = -1;//Default
            SelectedIndexChangeAnswer = 1;
         
        }

        void CreateAnswers()
        {
            using (testEntities db = new testEntities())
            {
                IEnumerable<Ответ> answers = db.Ответы.Where(p => p.Вопрос_Id == EditQuestionId).Select(p => new { Текст = p.Текст, Значение = p.Значение, })
.AsEnumerable()
.Select(an => new Ответ
{
    Текст = an.Текст,
    Значение = an.Значение,
});

                if (TypeQuestion == "CheckBox")
                {
                    foreach (var answer in answers)
                    {
                        CheckAnswer(answer.Текст, answer.Значение);
                    }
                    SelectedIndexChangeAnswer = 0;
                }
                else
                if (TypeQuestion == "RadioButton")
                {
                    foreach (var answer in answers)
                    {
                        RadioAnswer(answer.Текст, answer.Значение);
                    }
                    SelectedIndexChangeAnswer = 1;
                }
            }
        }

        #region Property
        public bool IsCheckedTestEditor
        {
            get { return _isCheckedTestEditor; }
            set
            {
                _isCheckedTestEditor = value;
                
                OnPropertyChanged("IsCheckedTestEditor");
            }
        }

        public Visibility CodeBoxVisibility
        {
            get { return _codeBoxVisibility; }
            set
            {
                _codeBoxVisibility = value;

                OnPropertyChanged();
            }
        }

        public string CodeQuestion
        {
            get { return _codeQuestion; }
            set { _codeQuestion = value; }
        }

        public int SelectedIndexChangeAnswer
        {
            get { return _selectedIndexChangeAnswer; }
            set
            {
                _selectedIndexChangeAnswer = value;

                OnPropertyChanged();
            }
        }

        public bool IsCheckCodeQuestion
        {
            get { return _isCheckCodeQuestion; }
            set
            {
                _isCheckCodeQuestion = value;

                OnPropertyChanged();
            }
        }

      public string TextQuestion
        {
            get { return _textQuestion; }
            set
            {
                _textQuestion = value;

                OnPropertyChanged();
            }
        }
        #endregion

        private RelayCommand changeVisabilityCodeBoxCommand;
        public RelayCommand ChangeVisabilityCodeBoxCommand
        {

            get
            {
                return changeVisabilityCodeBoxCommand ??
                    (changeVisabilityCodeBoxCommand = new RelayCommand(obj =>
                    {
                        CheckVisabilityCodeBox();
                    }));
            }
        }

        private RelayCommand loadedStackPanel;
        public RelayCommand LoadedStackPanel
        {

            get
            {
                return loadedStackPanel ??
                    (loadedStackPanel = new RelayCommand(obj =>
                    {
                        AnswerStackPanel = (obj as StackPanel);
                        if (TextQuestion == "")
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (i == 0) RadioAnswer("", true);
                                else
                                    RadioAnswer("", false);
                            }
                        }
                        else
                         if (TextQuestion != "")
                        {
                            CreateAnswers();
                        }

                           
                 
                    }));
            }
        }

        private RelayCommand saveQuestion;
        public RelayCommand SaveQuestion
        {

            get
            {
                return saveQuestion ??
                    (saveQuestion = new RelayCommand(obj =>
                    {
                        using (testEntities db = new testEntities())
                        {
                            var children = AnswerStackPanel.Children.OfType<UIElement>().ToList();
                            if (IsCheckCodeQuestion == false) CodeQuestion = null;
                            if (EditQuestionId == 0)//Добавление нового элемента 
                            {
                                Вопрос вопрос = new Вопрос { Текст = TextQuestion, Код = CodeQuestion, Тип_Ответа = (obj as string), Тема_Id = EditThemeId };
                                db.Вопросы.Add(вопрос);
                            
                                foreach (var ans in children)
                                {
                                    if((string)obj == "RadioButton")
                                    {
                                        db.Ответы.Add(new Ответ { Текст = ((ans as RadioButton).Content as TextBox).Text, Значение = (bool)(ans as RadioButton).IsChecked, Вопрос_Id = вопрос.Id });
                                    
                                    }
                                    else
                                    if ((string)obj == "CheckBox")
                                    {
                                        db.Ответы.Add(new Ответ { Текст = ((ans as CheckBox).Content as TextBox).Text, Значение = (bool)(ans as CheckBox).IsChecked, Вопрос_Id = вопрос.Id });
                                   
                                    }
                                    
                                }
                                db.SaveChanges();
                            }

                            if (EditQuestionId != 0)//Редактирование элемента
                            {
                                Вопрос вопрос = db.Вопросы.Where(s => s.Id == EditQuestionId).SingleOrDefault();
                                вопрос.Текст = TextQuestion;
                                вопрос.Код = CodeQuestion;
                                вопрос.Тип_Ответа = (obj as string);
                                List<Ответ> ответы = db.Ответы.Where(S => S.Вопрос_Id == EditQuestionId).ToList();
                                for (int i = 0; i < ответы.Count; i++)
                                {
                                    if ((string)obj == "RadioButton")
                                    {
                                        ответы[i].Текст = ((children[i] as RadioButton).Content as TextBox).Text;
                                        ответы[i].Значение = (bool)(children[i] as RadioButton).IsChecked;
                                    }
                                    else
                                   if ((string)obj == "CheckBox")
                                    {
                                        ответы[i].Текст = ((children[i] as CheckBox).Content as TextBox).Text;
                                        ответы[i].Значение = (bool)(children[i] as CheckBox).IsChecked;
                                    }




                                }
                                db.SaveChanges();

                            }
                        }
                    }));
            }
        }

        private RelayCommand deleteQuestion;
        public RelayCommand DeleteQuestion
        {

            get
            {
                return deleteQuestion ??
                    (deleteQuestion = new RelayCommand(obj =>
                    {


                        using (testEntities db = new testEntities())
                        {
                            var children = AnswerStackPanel.Children.OfType<UIElement>().ToList();
                            if (IsCheckCodeQuestion == false) CodeQuestion = null;
                            if (EditQuestionId == 0)//Добавление нового элемента 
                            {
                                Вопрос вопрос = new Вопрос { Текст = TextQuestion, Код = CodeQuestion, Тип_Ответа = (obj as string), Тема_Id = EditThemeId };
                                db.Вопросы.Add(вопрос);

                                foreach (var ans in children)
                                {
                                    if ((string)obj == "RadioButton")
                                    {
                                        db.Ответы.Add(new Ответ { Текст = ((ans as RadioButton).Content as TextBox).Text, Значение = (bool)(ans as RadioButton).IsChecked, Вопрос_Id = вопрос.Id });

                                    }
                                    else
                                    if ((string)obj == "CheckBox")
                                    {
                                        db.Ответы.Add(new Ответ { Текст = ((ans as CheckBox).Content as TextBox).Text, Значение = (bool)(ans as CheckBox).IsChecked, Вопрос_Id = вопрос.Id });

                                    }

                                }
                                db.SaveChanges();
                            }
                      

                            }
                    }));
            }
        }


        private RelayCommand changeTypeAnswerCommand;

        public RelayCommand ChangeTypeAnswerCommand
        {
            get
            {
                return changeTypeAnswerCommand ??
                    (changeTypeAnswerCommand = new RelayCommand(obj =>
                    {
                       if (SelectedIndexChangeAnswer == 0)
                        {
                            bool IsFirst = true;
                            var children = AnswerStackPanel.Children.OfType<UIElement>().ToList();
                            foreach (var ans in children)
                            {
                                if (ans.GetType() == typeof(RadioButton))
                                {
                                    if (IsFirst)
                                    {
                                        CheckAnswer(((ans as RadioButton).Content as TextBox).Text, true);
                                        IsFirst = false;
                                    }
                                    else
                                        CheckAnswer(((ans as RadioButton).Content as TextBox).Text, false);
                                    AnswerStackPanel.Children.Remove((RadioButton)ans);
                                }
                            }
                                

                        }
                        if (SelectedIndexChangeAnswer == 1)
                        {
                            bool  IsFirst= true;
                            var children = AnswerStackPanel.Children.OfType<UIElement>().ToList();
                            foreach (var ans in children)
                            {
                                if (ans.GetType() == typeof(CheckBox))
                                {
                                    if (IsFirst)
                                    {
                                        RadioAnswer(((ans as CheckBox).Content as TextBox).Text, true);
                                        IsFirst = false;
                                    }
                                    else
                                        RadioAnswer(((ans as CheckBox).Content as TextBox).Text, false);
                                    AnswerStackPanel.Children.Remove((CheckBox)ans);
                                }
                            }
                    
                        }   
                      
                    }));
            }
        }

        void RadioAnswer(string textanswer,bool valueanswer)
        { 
         RadioButton answer = new RadioButton { Content = new TextBox { FontSize = 15, Width = 1017, Height = 29, Text = textanswer }, MinHeight = 20, IsChecked = valueanswer, Margin = new Thickness(20, 0, 20, 6) };
            AnswerStackPanel.Children.Add(answer);
        }

        void CheckAnswer(string textanswer, bool valueanswer)
        {
            CheckBox answer = new CheckBox { Content = new TextBox { FontSize = 15, Width = 1017, Height = 29, Text = textanswer }, MinHeight = 20, IsChecked = valueanswer, Margin = new Thickness(20, 0, 20, 6) };
            AnswerStackPanel.Children.Add(answer);
        }

        void CheckVisabilityCodeBox()
        {
            if (IsCheckCodeQuestion == true)
            {
                CodeBoxVisibility = Visibility.Visible;
            }
            if (IsCheckCodeQuestion == false)
            {
                CodeBoxVisibility = Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}




//AnswerStackPanel = (obj as StackPanel);
//if (TextQuestion == "")
//{
//    for (int i = 0; i < 4; i++)
//    {
//        if (i == 0) RadioAnswer("", true);
//        else
//            RadioAnswer("", false);
//    }
//}
//else
// if (TextQuestion != "")
//{
//    CreateAnswers();
//}



//    if (ans == 0) CheckAnswer(ans.Content.ToString(), true);
//    else
//        CheckAnswer("", false);

//}
//AnswerStackPanel.Children.
//AnswerStackPanel.Children.Clear();
//for(int i =0; i< 4;i++)
//{
//    if (i == 0) CheckAnswer("", true);
//    else
//        CheckAnswer("", false);
// CheckBox oneanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//CheckBox twoanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//CheckBox threeanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//CheckBox fouranswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//(obj as StackPanel).Children.Add(oneanswer);
//(obj as StackPanel).Children.Add(twoanswer);
//(obj as StackPanel).Children.Add(threeanswer);
//(obj as StackPanel).Children.Add(fouranswer);

//RadioButton oneanswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//RadioButton twoanswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//RadioButton threeanswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//RadioButton fouranswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
//AnswerStackPanel.Children.Add(oneanswer);
//AnswerStackPanel.Children.Add(twoanswer);
//AnswerStackPanel.Children.Add(threeanswer);
//AnswerStackPanel.Children.Add(fouranswer);