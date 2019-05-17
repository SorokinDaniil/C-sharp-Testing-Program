using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace TestingProgram
{
    public class TextFieldsViewModel : INotifyPropertyChanged
    {
        private string _loginUsername;
        private string _loginPassword;
        private string _signupUsername;
        private string _signupPassword;
        private string _signupName;
   


        public TextFieldsViewModel()
        {
            DataTable dt_user = Select("SELECT * FROM [dbo].[Username]"); // получаем данные из таблицы

            //for (int i = 0; i < dt_user.Rows.Count; i++)
            //{ // перебираем данные
            //    MessageBox.Show(dt_user.Rows[i][0] + "|" + dt_user.Rows[i][1] + dt_user.Rows[i][2]); // выводим данные
            //}
            LongListToTestComboVirtualization = new List<string>();
            LongListToTestComboVirtualization.Add(dt_user.Rows[0][0].ToString());
            LongListToTestComboVirtualization.Add(dt_user.Rows[0][1].ToString());
            LongListToTestComboVirtualization.Add(dt_user.Rows[0][2].ToString());
            LongListToTestComboVirtualization.Add("T-694");
            //SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            //SelectedTextTwo = null;
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-JKOUM0H;Trusted_Connection=Yes;DataBase=test;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }

        public string LoginUsername
        {
            get { return _loginUsername; }
            set
            {
                this.MutateVerbose(ref _loginUsername, value, RaisePropertyChanged());
            }
        }

        public string LoginPassword
        {
            get { return _loginPassword; }
            set
            {
                this.MutateVerbose(ref _loginPassword, value, RaisePropertyChanged());
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

        public string SignupName
        {
            get { return _signupName; }
            set
            {
                this.MutateVerbose(ref _signupName, value, RaisePropertyChanged());
            }
        }

        public IList<string> LongListToTestComboVirtualization { get; }

        //public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);

        }
    }
}
