using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;


namespace TestingProgram
{
  public  class QuestionsCollectionViewModel : INotifyPropertyChanged
    {
        string _textQuestion;
        string TypeAnswerQuestion;
      public  byte IdQuestion;
       public StackPanel AnswerStackPanel;
        private string _codeQuestion;
        private Visibility _visibilityCodeBlockQuestion;
        string TypeAccount;
       

        public QuestionsCollectionViewModel (string textQuestion,string codeQuestion,string typeAnswerQuestion,byte idQuestion,string typeaccount)
        {
            TypeAccount = typeaccount;
            TextQuestion = textQuestion;//Загрузка текста вопроса
            IdQuestion = idQuestion;
            TypeAnswerQuestion = typeAnswerQuestion;
            if(codeQuestion == null)
            {
                VisibilityCodeBlockQuestion = Visibility.Collapsed;
            }
            else
            {
                VisibilityCodeBlockQuestion = Visibility.Visible;
                CodeQuestion = codeQuestion;
            }
           
        }

        void CreateAnswers ()
        {
            using (testEntities db = new testEntities())
            {
                bool IsCheckedAnswer = false;
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
                        if (TypeAccount == "Admin") IsCheckedAnswer = answer.Значение;
                            CheckBox radionanswer = new CheckBox {  Content = new TextBlock { FontSize = 15, Width = 1017, Height = 29, Text = answer.Текст }, MinHeight = 20, IsChecked = IsCheckedAnswer, Margin = new System.Windows.Thickness(20, 0, 20, 6) };
                        AnswerStackPanel.Children.Add(radionanswer);
                    }
                }
                else
                if (TypeAnswerQuestion == "RadioButton")
                {
                    foreach (var answer in answers)
                    {
                        if (TypeAccount == "Admin") IsCheckedAnswer = answer.Значение;
                        RadioButton radionanswer = new RadioButton {  Content = new TextBlock { FontSize = 15, Width = 1017, Height = 29 ,Text=answer.Текст }, MinHeight = 20, IsChecked = IsCheckedAnswer, Margin = new System.Windows.Thickness(20, 0, 20, 6) };
                        AnswerStackPanel.Children.Add(radionanswer);
                    }
                }
                 
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

        public Visibility VisibilityCodeBlockQuestion
        {
            get { return _visibilityCodeBlockQuestion; }
            set
            {
                _visibilityCodeBlockQuestion = value;

                OnPropertyChanged();
            }
        }

      
        public string CodeQuestion
        {
            get { return _codeQuestion; }
            set { _codeQuestion = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
