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
    class ExitAppDialogViewModel : INotifyPropertyChanged
    {

        public ExitAppDialogViewModel ()
        {

        }

        private RelayCommand ExitAppcommand;
        public RelayCommand ExitAppCommand

        {

            get
            {
                return ExitAppcommand ??
                    (ExitAppcommand = new RelayCommand(obj =>
                    {
                        Application.Current.Shutdown();
                        //DialogHost.CloseDialogCommand.Execute(true , null);
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
