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
    class FinishDialogViewModel : INotifyPropertyChanged
    {
        Visibility _visibilityClose;
        Visibility _visibilityCheck;
        string _finishText;
        int _finishMark;

        public FinishDialogViewModel(int Mark)
        {
            FinishMark = Mark;
            if (FinishMark >= 4)
            {
                FinishText = "Тест пройден";
                VisibilityCheck = Visibility.Visible;
                VisibilityClose = Visibility.Collapsed;
            }
            else
            {
                FinishText = "Тест не пройден";
                VisibilityCheck = Visibility.Collapsed;
                VisibilityClose = Visibility.Visible;
            }
        
        }

        public Visibility VisibilityCheck
        {
            get { return _visibilityCheck; }
            set
            {
                if (_visibilityCheck != value)
                {
                    _visibilityCheck = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Visibility VisibilityClose
        {
            get { return _visibilityClose; }
            set
            {
                if (_visibilityClose != value)
                {
                    _visibilityClose = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string FinishText
        {
            get { return _finishText; }
            set
            {
                if (_finishText != value)
                {
                    _finishText = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int FinishMark
        {
            get { return _finishMark; }
            set
            {
                if (_finishMark != value)
                {
                    _finishMark = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
