using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace TestingProgram
{
  public  class QuestionsCollectionViewModel : INotifyPropertyChanged
    {
        string _textQuestion;

        public QuestionsCollectionViewModel (string textQuestion,string codeQuestion,string typeAnswerQuestion,byte idQuestion)
        {
            TextQuestion = textQuestion;

            CreateAnswers(idQuestion);

            if(typeAnswerQuestion == "CheckBox")
            {

            }
            else
            if (typeAnswerQuestion == "RadioButton")
            {

            }
        }

        void CreateAnswers (byte idQuestion)
        {
            using (testEntities db = new testEntities())
            {  
                IEnumerable<Ответ> answers = db.Ответы.Where(p => p.Вопрос_Id == idQuestion).Select(p => new { Текст = p.Текст, Значение = p.Значение, })
.AsEnumerable()
.Select(an => new Ответ
{
    Текст = an.Текст,
    Значение = an.Значение,
});
                foreach (var answer in answers)
                {
                    
                    //Тут закончил
                    //QuestionsSlides.Add(new QuestionsCollectionViewModel(question.Текст, question.Код, question.Тип_Ответа, question.Id));
                }
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
