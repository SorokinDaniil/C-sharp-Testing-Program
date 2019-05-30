using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DevExpress.Mvvm;
using System.Windows.Input;

namespace TestingProgram.TESTFOLDER
{
    #region Model

    public class TestData : INotifyPropertyChanged
    {
        private bool _hasError = false;

        public bool HasError
        {
            get
            {
                return _hasError;
            }

            set
            {
                _hasError = value;
                NotifyPropertyChanged("HasError");
            }
        }

        private string _testText = "0";

        public string TestText
        {
            get
            {
                return _testText;
            }

            set
            {
                _testText = value;
                NotifyPropertyChanged("TestText");
            }
        }

      


        #region PropertyChanged

        public event PropertyChangedEventHandler  PropertyChanged;

        protected void NotifyPropertyChanged(string sProp)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(sProp));
            }
        }

        #endregion
    }

    #endregion
}
