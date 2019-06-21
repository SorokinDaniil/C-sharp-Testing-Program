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
    class UserStartTestDialogViewModel : INotifyPropertyChanged
    {
       
        private string _startTestheader;
        private int _startTestcountquestion;
        private string _startTesttime;
     

        public UserStartTestDialogViewModel(string starttestheader , string starttesttime , int starttestcountquestion)
        {
            StartTestHeader = starttestheader;
            StartTestTime = starttesttime;
            StartTestCountQuestion = starttestcountquestion;

            //IdSelectedValue = idSelectedValue;
        }

        public string StartTestHeader
        {
            get { return _startTestheader; }
            set
            {
                if (_startTestheader != value)
                {
                    _startTestheader = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int StartTestCountQuestion
        {
            get { return _startTestcountquestion; }
            set
            {
                if (_startTestcountquestion != value)
                {
                    _startTestcountquestion = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string StartTestTime
        {
            get { return _startTesttime; }
            set
            {
                if (_startTesttime != value)
                {
                    _startTesttime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private RelayCommand startTestcommand;
        public RelayCommand StartTestCommand
        {

            get
            {
                return startTestcommand ??
                    (startTestcommand = new RelayCommand(obj =>
                    {
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