using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;

namespace TestingProgram
{
   public class TabControlViewModel : INotifyPropertyChanged
    {
        private bool _ischeckedonetab;
        private bool _ischeckedtwotab;
        private int _selectedTabIndex;
        public MainTableViewModel Admin_ListStudent_TableListStudentEdit_ViewModel = new MainTableViewModel("Admin_ListStudent_TableListStudentEdit");
        public MainTableViewModel Admin_ListStudent_TableTestNoEdit_ViewModel = new MainTableViewModel("Admin_ListStudent_TableTestNoEdit");
        MainTable Admin_ListStudent_TableListStudentEdit;
        MainTable Admin_ListStudent_TableTestNoEdit;

      public TabControlViewModel()
        {
             Admin_ListStudent_TableListStudentEdit = new MainTable() { DataContext = Admin_ListStudent_TableListStudentEdit_ViewModel };
             Admin_ListStudent_TableTestNoEdit = new MainTable() { DataContext = Admin_ListStudent_TableTestNoEdit_ViewModel };
        }

        public void Show(string selectedvaluegroup, string selectedvaluechaphter)
        {
            SelectedTabIndex = 0;
            Admin_ListStudent_TableListStudentEdit_ViewModel.Show(selectedvaluegroup);
            Admin_ListStudent_TableTestNoEdit_ViewModel.Show(selectedvaluechaphter);
            //IsCheckChoice = ischeckchoice;
            //SelecteValueChoice = selectevaluechoice;
            //HeaderMainTable = SelecteValueChoice;
            //Console.WriteLine(IsCheckChoice);
            //Console.WriteLine(SelecteValueChoice);
        }

        private RelayCommand _tabcontrolcommand;
        public RelayCommand TabControlCommand
        {
            get
            {
                return _tabcontrolcommand ??
                    (_tabcontrolcommand = new RelayCommand(obj =>
                    {
                        if (IsCheckedOneTab == true)
                        {
                            (obj as Grid).Children.Clear();
                            (obj as Grid).Children.Add(Admin_ListStudent_TableListStudentEdit);
                        }
                        if(IsCheckedTwoTab == true)
                        {
                            (obj as Grid).Children.Clear();
                            (obj as Grid).Children.Add(Admin_ListStudent_TableTestNoEdit);
                        }
                    }));
            }
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;

                OnPropertyChanged();
            }
        }

        public bool IsCheckedTwoTab
        {
            get { return _ischeckedtwotab; }
            set
            {
                _ischeckedtwotab = value;

                OnPropertyChanged();
            }
        }

        public bool IsCheckedOneTab
        {
            get { return _ischeckedonetab; }
            set
            {
                _ischeckedonetab = value;

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
