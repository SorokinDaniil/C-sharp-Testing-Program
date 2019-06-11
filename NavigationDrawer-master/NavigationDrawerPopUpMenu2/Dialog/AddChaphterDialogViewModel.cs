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
    class AddChaphterDialogViewModel : INotifyPropertyChanged
    {
        private bool _isCheck;

        private string _addtext;
        private string _addtextbox;
        private string _addtimepicker;
        byte IdSelectedValue;

        public AddChaphterDialogViewModel (byte idSelectedValue)
        {
            IdSelectedValue = idSelectedValue;
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

        public string AddTimePicker
        {
            get { return _addtimepicker; }
            set
            {
                if (_addtimepicker != value)
                {
                    _addtimepicker = value;
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
                            using (testEntities db = new testEntities())
                            {
                            Тема тема = new Тема { Название = AddTextBox, Время_Прохождения = TimeSpan.Parse(AddTimePicker)  , Раздел_Id = IdSelectedValue };
                                db.Темы.Add(тема);
                                db.SaveChanges();
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