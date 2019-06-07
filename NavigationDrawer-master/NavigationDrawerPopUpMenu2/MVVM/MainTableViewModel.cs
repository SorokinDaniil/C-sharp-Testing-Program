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
    public class MainTableViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<SelectableViewModel> _items1;
        private readonly ObservableCollection<SelectableViewModel> _items2;
        private readonly ObservableCollection<SelectableViewModel> _items3;
        private bool? _isAllItems3Selected;
        private string _selecteValueChoice;
        private string _headermaintable;
        private int _selectedTabIndex;
        private string _onecolumnname;
        private string _twocolumnname;
        private string _threecolumnname;
        private string _fourcolumnname;
        private Visibility _onecolumnvisability;
        private Visibility _twocolumnvisability;
        private Visibility _threecolumnvisability;
        private Visibility _fourcolumnvisability;



        public MainTableViewModel(string typeTable)
        {
            switch(typeTable)
            {
                case "Admin_Editor_TableChaphterEdit":
                    {
                        OneColumnName = "Название";
                        TwoColumnName = "Время";
                        ThreeColumnName = "Количество вопросов";
                        OneColumnVisability = Visibility.Visible;
                        TwoColumnVisability = Visibility.Visible;
                        ThreeColumnVisability = Visibility.Visible;

                        FourColumnVisability = Visibility.Hidden;
                    } break;
                case "Admin_ListStudent_TableListStudentEdit":
                    {
                        OneColumnName = "Номер";
                        TwoColumnName = "Имя и фамилия";
                        ThreeColumnName = "Пройденые тесты";
                        OneColumnVisability = Visibility.Visible;
                        TwoColumnVisability = Visibility.Visible;
                        ThreeColumnVisability = Visibility.Visible;

                        FourColumnVisability = Visibility.Hidden;
                    }
                    break;
                case "Admin_ListStudent_TableTestNoEdit":
                    {
                        OneColumnName = "Название";
                        OneColumnVisability = Visibility.Visible;

                        TwoColumnVisability = Visibility.Hidden;
                        ThreeColumnVisability = Visibility.Hidden;
                        FourColumnVisability = Visibility.Hidden;
                    }
                    break;
                case "Admin_ListStudent_TablePassedTestNoEdit":
                    {
                        OneColumnName = "Номер";
                        TwoColumnName = "Имя и фамилия";
                        ThreeColumnName = "Оценка";
                        FourColumnName = "Дата прохождения";
                        OneColumnVisability = Visibility.Visible;
                        TwoColumnVisability = Visibility.Visible;
                        ThreeColumnVisability = Visibility.Visible;
                        FourColumnVisability = Visibility.Visible;
                    }
                    break;
                case "User_ListStudent_TableTestNoEdit":
                    {
                        OneColumnName = "Название";
                        OneColumnVisability = Visibility.Visible;

                        TwoColumnVisability = Visibility.Hidden;
                        ThreeColumnVisability = Visibility.Hidden;
                        FourColumnVisability = Visibility.Hidden;
                    }
                    break;
                default: break;
            }   
            _items1 = CreateData();
            _items2 = CreateData();
            _items3 = CreateData();

            //Admin_Editor_TableChaphterEdit 

            //ListStudent_TableGroupEdit
            //ListStudent_TableChaphterNoEdit

            //Student_TableChaphterNoEdit
             
        }

        public void Show(string selectevaluechoice)
        {
            SelectedTabIndex = 0;
            SelecteValueChoice= selectevaluechoice;
            HeaderMainTable = SelecteValueChoice;//Присваивает название загаловка 
            //Console.WriteLine(IsCheckChoice);
            //Console.WriteLine(SelecteValueChoice);
        }
        #region Property
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { _selectedTabIndex = value;

                OnPropertyChanged();
            }
        }

      

        public string OneColumnName
        {
            get { return _onecolumnname; }
            set
            {
                _onecolumnname = value;

                OnPropertyChanged();
            }
        }

        public Visibility OneColumnVisability
        {
            get { return _onecolumnvisability; }
            set
            {
                _onecolumnvisability = value;

                OnPropertyChanged();
            }
        }

        public string TwoColumnName
        {
            get { return _twocolumnname; }
            set
            {
                _twocolumnname = value;

                OnPropertyChanged();
            }
        }

        public Visibility TwoColumnVisability
        {
            get { return _twocolumnvisability; }
            set
            {
               _twocolumnvisability = value;

                OnPropertyChanged("TwoColumnVisability");
            }
        }

        public string ThreeColumnName
        {
            get { return _threecolumnname; }
            set
            {
                _threecolumnname = value;

                OnPropertyChanged();
            }
        }

        public string FourColumnName
        {
            get { return _fourcolumnname; }
            set
            {
                _fourcolumnname = value;

                OnPropertyChanged();
            }
        }

        public Visibility ThreeColumnVisability
        {
            get { return _threecolumnvisability; }
            set
            {
                _threecolumnvisability = value;

                OnPropertyChanged();
            }
        }

        public Visibility FourColumnVisability
        {
            get { return _fourcolumnvisability; }
            set
            {
                _fourcolumnvisability = value;

                OnPropertyChanged();
            }
        }

        public string HeaderMainTable
        {
            get { return _headermaintable; }
            set
            {
                _headermaintable = value;

                OnPropertyChanged();
            }
        }

        public string SelecteValueChoice
        {
            get { return _selecteValueChoice; }
            set
            {
                _selecteValueChoice = value;

                OnPropertyChanged();
            }
        }

        //public bool? IsAllItems3Selected
        //{
        //    get { return _isAllItems3Selected; }
        //    set
        //    {
        //        if (_isAllItems3Selected == value) return;

        //        _isAllItems3Selected = value;

        //        if (_isAllItems3Selected.HasValue)
        //            SelectAll(_isAllItems3Selected.Value, Items3);
        //        OnPropertyChanged();
        //    }
        //}
#endregion


        //private static void SelectAll(bool select, IEnumerable<SelectableViewModel> models)
        //{
        //    foreach (var model in models)
        //    {
        //        model.IsSelected = select;
        //    }
        //}

        private static ObservableCollection<SelectableViewModel> CreateData()
        {
            return new ObservableCollection<SelectableViewModel>
            {
                new SelectableViewModel
                {
                    OneColumnContent = "M",
                    TwoColumnContent = "Material Design",
                    ThreeColumnContent = "Material Design in XAML Toolkit"
                },
                new SelectableViewModel
                {
                    OneColumnContent = "D",
                    TwoColumnContent = "Dragablz",
                    ThreeColumnContent = "Dragablz Tab Control",
                },
                new SelectableViewModel
                {
                    OneColumnContent = "P",
                    TwoColumnContent = "Predator",
                    ThreeColumnContent = "If it bleeds, we can kill it"
                },
               
                new SelectableViewModel
                {
                    OneColumnContent = "F",
                    TwoColumnContent = "Predator",
                    ThreeColumnContent = "If it bleeds, we can kill it"
                }
            };
        }

        public ObservableCollection<SelectableViewModel> Items1 => _items1;
        public ObservableCollection<SelectableViewModel> Items2 => _items2;

        public ObservableCollection<SelectableViewModel> Items3 => _items3;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
