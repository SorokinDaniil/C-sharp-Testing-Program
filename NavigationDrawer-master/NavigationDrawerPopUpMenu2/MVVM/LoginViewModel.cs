using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Security.Cryptography;

namespace TestingProgram
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _signinUsername;
        private string _signinPassword;
        private string _signupUsername;
        private string _signupPassword;
        private string _signupFullName;
        private string _signupGroup;
        private byte _signupGroupid;

        public List<Группа> GroupCollection { get; set; }

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
            using (TestEntities db = new TestEntities())
            {
               var группы = db.Группы.Select(c => c.Название);
                GroupCollection = db.Группы.ToList();
                
                //GroupCollection = new ObservableCollection<string>();
               
            }
        }

        public void SignIn()
        {
            using (TestEntities db = new TestEntities())
            {
                if (!(db.Студенты.SingleOrDefault(p => p.Логин == SignInUsername) == null))
                {
                    var checkpassword = ComputeSha256Hash(SignInPassword);
                    if (!(db.Студенты.Where(p => p.Логин == SignInUsername).SingleOrDefault(p => p.Пароль == checkpassword) == null))
                    {
                        MessageBox.Show("Вы успешно вошли");
                    }
                    else
                    MessageBox.Show("Пароль неверный");
                }
                else
                {
                    MessageBox.Show("Такой логин существует");
                }
            }
    
        }

        public void SignUp()
        {
           
            using (TestEntities db = new TestEntities())
            {
                if (db.Студенты.SingleOrDefault(p => p.Логин == SignUpUsername) == null)
                {
                    Студент студент = new Студент { ФИО = SignUpFullName, Группа_Id = SignUpGroupId, Логин = SignUpUsername, Пароль = ComputeSha256Hash(SignUpPassword) };
                    db.Студенты.Add(студент);
                    db.SaveChanges();
                    MessageBox.Show("Вы успешно зарегестрировались);");
                }
               else
                {
                    MessageBox.Show("Такой логин существует");
                }
            }
            //var a = ((object)SignUpGroup as Группа);
            MessageBox.Show(SignUpFullName  + SignUpGroupId + SignUpUsername + ComputeSha256Hash(SignUpPassword));
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string SignInUsername
        {
            get { return _signinUsername; }
            set
            {
                this.MutateVerbose(ref _signinUsername, value, RaisePropertyChanged());
            }
        }

        public string SignInPassword
        {
            get { return _signinPassword; }
            set
            {
                this.MutateVerbose(ref _signinPassword, value, RaisePropertyChanged());
            }
        }

        public string SignUpUsername
        {
            get { return _signupUsername; }
            set
            {
                this.MutateVerbose(ref _signupUsername, value, RaisePropertyChanged());
            }
        }

        public string SignUpPassword
        {
            get { return _signupPassword; }
            set
            {
                this.MutateVerbose(ref _signupPassword, value, RaisePropertyChanged());
            }
        }

        public string SignUpFullName
        {
            get { return _signupFullName; }
            set
            {
                this.MutateVerbose(ref _signupFullName, value, RaisePropertyChanged());
            }
        }

        public string SignUpGroup
        {
            get { return _signupGroup; }
            set
            {
                this.MutateVerbose(ref _signupGroup, value, RaisePropertyChanged());
            }
        }

        public byte SignUpGroupId
        {
            get { return _signupGroupid; }
            set
            {
                _signupGroupid = value;
            }
        }


        //public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);

        }
    }
 
}
