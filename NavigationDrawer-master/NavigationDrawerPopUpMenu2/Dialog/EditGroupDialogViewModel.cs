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
    class EditGroupDialogViewModel : INotifyPropertyChanged
    {
        private bool _isCheck;
        private string _selectValue;
        private string _edittext;
        private string _edittextbox;

        public EditGroupDialogViewModel(string selectvalue, bool ischeck)
        {
            IsCheck = ischeck;
            SelectValue = selectvalue;
            EditTextBox = SelectValue;
            if (IsCheck == true)
            {
                EditText = "Введите новое название группы:";
            }
            else
            if (IsCheck == false)
            {
                EditText = "Введите новое название раздела:";
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

        public string EditText
        {
            get { return _edittext; }
            set
            {
                if (_edittext != value)
                {
                    _edittext = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string EditTextBox
        {
            get { return _edittextbox; }
            set
            {
                if (_edittextbox != value)
                {
                    _edittextbox = value;
                    RaisePropertyChanged();
                }
            }
        }

        private RelayCommand editcommand;
        public RelayCommand EditCommand
        {

            get
            {
                return editcommand ??
                    (editcommand = new RelayCommand(obj =>
                    {
                        if (IsCheck == true)
                        {
                            using (TestEntities db = new TestEntities())
                            {
                                Группа группа = db.Группы.Where(s => s.Название == SelectValue).SingleOrDefault();
                                группа.Название = EditTextBox;
                                db.SaveChanges();
                            }
                        }
                        else
                             if (IsCheck == false)
                        {
                            using (TestEntities db = new TestEntities())
                            {
                                Раздел раздел = db.Разделы.Where(s => s.Название == SelectValue).SingleOrDefault();
                                раздел.Название = EditTextBox;
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
