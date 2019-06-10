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
using MaterialDesignThemes.Wpf;

namespace TestingProgram
{
    class AddGroupDialogViewModel : INotifyPropertyChanged
    {
        private bool _isCheck;
  
        private string _addtext;
        private string _addtextbox;

        public AddGroupDialogViewModel(bool ischeck)
        {
            IsCheck = ischeck;
            if (IsCheck == true)
            {
                AddText = "Введите название группы:";
            }
            else
            if (IsCheck == false)
            {
                AddText = "Введите название раздела:";
            }
        }

        public bool IsCheck
        {
            get { return _isCheck; }
            set
            {
                if (_isCheck != value)
                {
                    _isCheck = value;
                    RaisePropertyChanged();
                }
            }
        }
         
        public string AddText
        {
            get { return _addtext; }
            set
            {
                if (_addtext != value)
                {
                    _addtext = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string AddTextBox
        {
            get { return _addtextbox; }
            set
            {
                if (_addtextbox != value)
                {
                    _addtextbox = value;
                    RaisePropertyChanged();
                }
            }
        }

        private RelayCommand editcommand;
        public RelayCommand AddCommand
        {

            get
            {
                return editcommand ??
                    (editcommand = new RelayCommand(obj =>
                    {
                        if (IsCheck == true)
                        {
                            using (testEntities db = new testEntities())
                            {
                                Группа группа = new Группа { Название = AddTextBox };
                                db.Группы.Add(группа);
                                db.SaveChanges();
                            }
                        }
                        else
                             if (IsCheck == false)
                        {
                            using (testEntities db = new testEntities())
                            {
                                Раздел раздел = new Раздел { Название = AddTextBox };
                                db.Разделы.Add(раздел);
                                db.SaveChanges();
                            }
                        }
                        
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}