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
    class DeleteChaphterDialogViewModel : INotifyPropertyChanged
    {
        private bool _isCheck;
        private string _selectValue;
        private string _deletetext;
        byte IdSelectedChaphterValue;
        byte IdSelectedThemeValue;

        public DeleteChaphterDialogViewModel(byte idSelectedChaphterValue, byte idSelectedThemeValue)
        {
            IdSelectedChaphterValue = idSelectedChaphterValue;
            IdSelectedThemeValue = idSelectedThemeValue;
            DeleteText = "Вы действительно хотите удалить тему  и все вопросы связаные с ней ?";
           
             
            
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
                       
                            using (testEntities db = new testEntities())
                            {
                               var тема = db.Темы.Where(s => s.Id == IdSelectedThemeValue).SingleOrDefault();
                            //var вопросы = db.Вопросы.Where(s => s.Тема_Id == IdSelectedThemeValue);

                                db.Темы.Remove(тема);

                                db.SaveChanges();
                            }
                   
                   
                        DialogHost.CloseDialogCommand.Execute(true, null);
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
