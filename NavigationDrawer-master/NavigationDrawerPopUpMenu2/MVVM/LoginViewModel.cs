using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows;

namespace TestingProgram
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _signinUsername;
        private string _signinPassword;
        private string _signupUsername;
        private string _signupPassword;
        private string _signupFullName;
        private string _choiceGroup;

        void SignIn()
        {
            using (testEntities db = new testEntities())
            {
                if()
            }
        }

        void SignUp()
        {

        }

        //private RelayCommand checkValidation;
        //public RelayCommand CheckValidation
        //{

        //    get
        //    {
        //        return checkValidation ??
        //            (checkValidation = new RelayCommand(obj =>
        //            {
        //                Validation.GetErrors((DependencyObject)(obj as Window).FindName("SignIn_Login"));
        //            }));
        //    }
        //}

        //public ICommand ClickAdd
        //{

        //    get
        //    {
        //        return new DelegateCommand(() =>
        //        {

        //        });

        //    }
        //}

        public LoginViewModel()
        {
            //DataTable dt_user = Select("SELECT * FROM [dbo].[Username]"); // получаем данные из таблицы

            ////for (int i = 0; i < dt_user.Rows.Count; i++)
            ////{ // перебираем данные
            ////    MessageBox.Show(dt_user.Rows[i][0] + "|" + dt_user.Rows[i][1] + dt_user.Rows[i][2]); // выводим данные
            ////}
            //LongListToTestComboVirtualization = new List<string>();
            //LongListToTestComboVirtualization.Add(dt_user.Rows[0][0].ToString());
            //LongListToTestComboVirtualization.Add(dt_user.Rows[0][1].ToString());

            //LongListToTestComboVirtualization.Add("T-694");
            //SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            //SelectedTextTwo = null;
        }



        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-O6G977H;Trusted_Connection=Yes;DataBase=Test;");
            //SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-JKOUM0H;Trusted_Connection=Yes;DataBase=test;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }

       

        public string SignInUsername
        {
            get { return _signinUsername; }
            set
            {
                this.MutateVerbose(ref _signinUsername, value, RaisePropertyChanged());
            }
        }

        public string LoginPassword
        {
            get { return _signinPassword; }
            set
            {
                this.MutateVerbose(ref _signinPassword, value, RaisePropertyChanged());
            }
        }

        public string SignupUsername
        {
            get { return _signupUsername; }
            set
            {
                this.MutateVerbose(ref _signupUsername, value, RaisePropertyChanged());
            }
        }

        public string SignupPassword
        {
            get { return _signupPassword; }
            set
            {
                this.MutateVerbose(ref _signupPassword, value, RaisePropertyChanged());
            }
        }

        public string SignupFullName
        {
            get { return _signupFullName; }
            set
            {
                this.MutateVerbose(ref _signupFullName, value, RaisePropertyChanged());
            }
        }

        public string ChoiceGroup
        {
            get { return _choiceGroup; }
            set
            {
                this.MutateVerbose(ref _choiceGroup, value, RaisePropertyChanged());
            }
        }

        public IList<string> LongListToTestComboVirtualization { get; }

        ObservableCollection<> ErrorList;
        //public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);

        }
    }
 
}
