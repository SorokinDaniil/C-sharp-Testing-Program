﻿using System;
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
    class DeleteChaphterDialogViewModel : INotifyPropertyChanged
    {
        private bool _isCheck;
        private string _selectValue;
        private string _deletetext;

        public DeleteChaphterDialogViewModel(byte idSelectedChaphterValue, byte idSelectedThemeValue)
        {
            IsCheck = ischeck;
            SelectValue = selectvalue;
            if (IsCheck == true)
            {
                DeleteText = "Вы действительно хотите удалить группу ?";
            }
            else
            if (IsCheck == false)
            {
                DeleteText = "Вы действительно хотите удалить раздел?";
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

        public string SelectValue
        {
            get { return _selectValue; }
            set
            {
                if (_selectValue != value)
                {
                    _selectValue = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string DeleteText
        {
            get { return _deletetext; }
            set
            {
                if (_deletetext != value)
                {
                    _deletetext = value;
                    RaisePropertyChanged();
                }
            }
        }

        private RelayCommand deletecommand;
        public RelayCommand DeleteCommand

        {

            get
            {
                return deletecommand ??
                    (deletecommand = new RelayCommand(obj =>
                    {
                        if (IsCheck == true)
                        {
                            using (testEntities db = new testEntities())
                            {
                                Группа группа = db.Группы.Where(s => s.Название == SelectValue).SingleOrDefault();
                                db.Группы.Remove(группа);
                                db.SaveChanges();
                            }
                        }
                        else
                             if (IsCheck == false)
                        {
                            using (testEntities db = new testEntities())
                            {
                                Раздел раздел = db.Разделы.Where(s => s.Название == SelectValue).SingleOrDefault();
                                db.Разделы.Remove(раздел);
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
