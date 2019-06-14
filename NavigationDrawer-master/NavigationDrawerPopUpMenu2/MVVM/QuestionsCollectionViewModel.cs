﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;


namespace TestingProgram
{
  public  class QuestionsCollectionViewModel : INotifyPropertyChanged
    {
        string _textQuestion;
        string TypeAnswerQuestion;
        byte IdQuestion;
        StackPanel AnswerStackPanel;

        public QuestionsCollectionViewModel (string textQuestion,string codeQuestion,string typeAnswerQuestion,byte idQuestion)
        {
            TextQuestion = textQuestion;//Загрузка текста вопроса
            IdQuestion = idQuestion;
            TypeAnswerQuestion = typeAnswerQuestion;


        

         
        }

        void CreateAnswers ()
        {
            using (testEntities db = new testEntities())
            {  
                IEnumerable<Ответ> answers = db.Ответы.Where(p => p.Вопрос_Id == IdQuestion).Select(p => new { Текст = p.Текст, Значение = p.Значение, })
.AsEnumerable()
.Select(an => new Ответ
{
    Текст = an.Текст,
    Значение = an.Значение,
});
                if (TypeAnswerQuestion == "CheckBox")
                {
                    foreach (var answer in answers)
                    {
                        CheckBox radionanswer = new CheckBox { Content = new TextBlock { FontSize = 15, Width = 1017, Height = 29, Text = answer.Текст }, MinHeight = 20, IsChecked = answer.Значение, Margin = new System.Windows.Thickness(20, 0, 20, 6) };
                        AnswerStackPanel.Children.Add(radionanswer);
                    }
                }
                else
                if (TypeAnswerQuestion == "RadioButton")
                {
                    foreach (var answer in answers)
                    {
                        RadioButton radionanswer = new RadioButton { Content = new TextBlock { FontSize = 15, Width = 1017, Height = 29 ,Text=answer.Текст }, MinHeight = 20, IsChecked = answer.Значение, Margin = new System.Windows.Thickness(20, 0, 20, 6) };
                        AnswerStackPanel.Children.Add(radionanswer);
                    }
                }
                    //Тут закончил
                    //QuestionsSlides.Add(new QuestionsCollectionViewModel(question.Текст, question.Код, question.Тип_Ответа, question.Id));
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
                        CreateAnswers();//Загрузка вариантов ответа
                    }));
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
