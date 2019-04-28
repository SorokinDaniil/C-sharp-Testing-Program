﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestingProgram
{
    public class TextFieldsViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _name2;
        private int? _selectedValueOne;
        private string _selectedTextTwo;

        public TextFieldsViewModel()
        {
            LongListToTestComboVirtualization = new List<string>();
            LongListToTestComboVirtualization.Add("T-691");
            LongListToTestComboVirtualization.Add("T-692");
            LongListToTestComboVirtualization.Add("T-693");
            LongListToTestComboVirtualization.Add("T-694");
            //SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            SelectedTextTwo = null;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                this.MutateVerbose(ref _name, value, RaisePropertyChanged());
            }
        }

        public string Name2
        {
            get { return _name2; }
            set
            {
                this.MutateVerbose(ref _name2, value, RaisePropertyChanged());
            }
        }

        public int? SelectedValueOne
        {
            get { return _selectedValueOne; }
            set
            {
                this.MutateVerbose(ref _selectedValueOne, value, RaisePropertyChanged());
            }
        }        

        public string SelectedTextTwo
        {
            get { return _selectedTextTwo; }
            set
            {
                this.MutateVerbose(ref _selectedTextTwo, value, RaisePropertyChanged());
            }
        }

        public IList<string> LongListToTestComboVirtualization { get; }

        //public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
