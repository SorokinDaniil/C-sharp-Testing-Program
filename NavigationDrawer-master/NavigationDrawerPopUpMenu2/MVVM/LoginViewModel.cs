﻿using System;
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
            using (testEntities db = new testEntities())
            {
               var группы = db.Группы.Select(c => c.Название);
                GroupCollection = db.Группы.ToList();
                
                //GroupCollection = new ObservableCollection<string>();
               
            }
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

        public void SignIn()
        {
          
        }

        public void SignUp()
        {
            MessageBox.Show(SignUpGroupId.ToString());
            using (testEntities db = new testEntities())
            {
                Студент студент = new Студент { Имя = SignUpFullName ,Группа_Id = SignUpGroupId };

            }
            var a = ((object)SignUpGroup as Группа);
            MessageBox.Show(a.Id.ToString());

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
