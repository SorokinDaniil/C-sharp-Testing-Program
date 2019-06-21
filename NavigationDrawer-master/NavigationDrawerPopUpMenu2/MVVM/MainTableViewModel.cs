using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using DevExpress;
using System.Data.Entity;

namespace TestingProgram
{
    public class MainTableViewModel : INotifyPropertyChanged 
    {
     
        private readonly ObservableCollection<SelectableViewModel> _items3;
        private string _selecteValueChoice;
        private bool _isEnabledPopupBox;
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
        string TypeTable;
        byte IdSelectedChaphterValue;
        Visibility _visabilityPopupButton;
        string LoginName;



        public MainTableViewModel(string typeTable)
        {
            SelecteValueChoice = "";
            TypeTable = typeTable;
            switch (TypeTable)
            {
                case "Admin_Editor_TableChaphterEdit":
                    {
                        VisabilityPopupButton = Visibility.Visible;
                        OneColumnName = "Название";
                        TwoColumnName = "Время";
                        ThreeColumnName = "Количество вопросов";
                        OneColumnVisability = Visibility.Visible;
                        TwoColumnVisability = Visibility.Visible;
                        ThreeColumnVisability = Visibility.Visible;

                        FourColumnVisability = Visibility.Collapsed;
                    } break;
                case "Admin_ListStudent_TableListStudentEdit":
                    {
                        VisabilityPopupButton = Visibility.Visible;
                        OneColumnName = "Номер";
                        TwoColumnName = "Имя и фамилия";
                        ThreeColumnName = "Пройденые тесты";
                        OneColumnVisability = Visibility.Visible;
                        TwoColumnVisability = Visibility.Visible;
                        ThreeColumnVisability = Visibility.Visible;

                        FourColumnVisability = Visibility.Collapsed;
                    }
                    break;
                case "Admin_ListStudent_TableTestNoEdit":
                    {
                        VisabilityPopupButton = Visibility.Collapsed;
                        OneColumnName = "Название";
                        OneColumnVisability = Visibility.Visible;

                        TwoColumnVisability = Visibility.Collapsed;
                        ThreeColumnVisability = Visibility.Collapsed;
                        FourColumnVisability = Visibility.Collapsed;
                    }
                    break;
                case "Admin_ListStudent_TablePassedTestNoEdit":
                    {
                        VisabilityPopupButton = Visibility.Collapsed;
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
                        VisabilityPopupButton = Visibility.Collapsed;
                        OneColumnName = "Название";
                        OneColumnVisability = Visibility.Visible;

                        TwoColumnVisability = Visibility.Collapsed;
                        ThreeColumnVisability = Visibility.Collapsed;
                        FourColumnVisability = Visibility.Collapsed;
                    }
                    break;
                default: break;
            }   
            _items3 = CreateData();
        }

     private RelayCommand selectedItemGridCommand;
        public RelayCommand SelectedItemGridCommand
        {

            get
            {
                return selectedItemGridCommand ??
                    (selectedItemGridCommand = new RelayCommand(obj =>
                    {
                        if (SelectedTabIndex == -1)
                            IsEnabledPopupBox = false;
                        else
                        IsEnabledPopupBox = true;

                    }));
            }
        }


        private RelayCommand updateButtonCommand;
        public RelayCommand UpdateButtonCommand
        {

            get
            {
                return updateButtonCommand ??
                    (updateButtonCommand = new RelayCommand(obj =>
                    {
                        SelecteValueChoice = "";
                        switch (TypeTable)
                        {
                            case "Admin_Editor_TableChaphterEdit":
                                {
                                    (obj as Button).Command = NavigationCommands.ShowChoiceChaphterCommand; break;
                                }
                           
                            case "Admin_ListStudent_TableListStudentEdit":
                                {
                                  
                                }
                                break;
                            case "Admin_ListStudent_TableTestNoEdit":
                                {
                            
                                }
                                break;
                            case "Admin_ListStudent_TablePassedTestNoEdit":
                                {
                                
                                }
                                break;
                             case "User_ListStudent_TableTestNoEdit":
                                {
                                    (obj as Button).Command = NavigationCommands.ShowChoiceChaphterNoEditUserCommand; break;
                                }
                              
                            default: break;
                        }
                    }));
            }
        }

        #region ADD EDIT DELETE
        public ICommand RunDialogAddChaphterCommand => new AnotherCommandImplementation(AddChaphterCommand);
        public ICommand RunDialogEditChaphterCommand => new AnotherCommandImplementation(EditChaphterCommand);
        public ICommand RunDialogDeleteChaphterCommand => new AnotherCommandImplementation(DeleteChaphterCommand);
        public ICommand RunUserStartTestDialogCommand => new AnotherCommandImplementation(UserStartTestDialogCommand);

        private async void AddChaphterCommand(object o)
        {
                var view = new AddChaphterDialog
                {
                    DataContext = new AddChaphterDialogViewModel(IdSelectedChaphterValue)
                };

                var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            if ((bool)result == true)
            {
                using (testEntities db = new testEntities())
                {
                    var lastShowPieceId = db.Темы.Max(x => x.Id);
                    var  LastTheme  = db.Темы.FirstOrDefault(x => x.Id == lastShowPieceId);
                    db.Вопросы.Where(p => p.Тема_Id == lastShowPieceId).Load();
                    _items3.Add(new SelectableViewModel { OneColumnContent = LastTheme.Название, TwoColumnContent = LastTheme.Время_Прохождения.ToString(), ThreeColumnContent = db.Вопросы.Local.Count.ToString() });
                }
            }
                Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
            }

        private void EditChaphterCommand(object o)
        {
            using (testEntities db = new testEntities())
            {
                var name = _items3[SelectedTabIndex].OneColumnContent;
                var SelectedThemeValue = db.Темы.SingleOrDefault(p => p.Название == name);

                ThemeEdit themeEdit = new ThemeEdit() { DataContext = new ThemeEditorViewModel(SelectedThemeValue.Название, SelectedThemeValue.Время_Прохождения.ToString(), SelectedThemeValue.Id) };
                themeEdit.Show();
            }
        }

        private async void DeleteChaphterCommand(object o)
        {
            using (testEntities db = new testEntities())
            {
                var name = _items3[SelectedTabIndex].OneColumnContent;
                var IdSelectedThemeValue = db.Темы.SingleOrDefault(p => p.Название == name);
                var view = new DeleteChaphterDialog
                {
                    DataContext = new DeleteChaphterDialogViewModel(IdSelectedChaphterValue, IdSelectedThemeValue.Id)
                };
                var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
                if ((bool)result == true)
                {
                    _items3.Remove(_items3[SelectedTabIndex]);
                }

                Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
            }
        }

        private async void UserStartTestDialogCommand(object o)
        {
            switch (TypeTable)
            {
                case "Admin_Editor_TableChaphterEdit":
                    {
                      
                    }
                    break;
                case "Admin_ListStudent_TableListStudentEdit":
                    {

                    }
                    break;
                case "Admin_ListStudent_TableTestNoEdit":
                    {

                    }
                    break;
                case "Admin_ListStudent_TablePassedTestNoEdit":
                    {

                    }
                    break;
                case "User_ListStudent_TableTestNoEdit":
                    {
                        if (_items3.Count > 0 ) {
                            using (testEntities db = new testEntities())
                            {
                                var name = _items3[SelectedTabIndex].OneColumnContent;
                                var theme = db.Темы.SingleOrDefault(p => p.Название == name);
                                var countquestion = db.Вопросы.Count(t => t.Тема_Id == theme.Id);

                                if (CheckTheme(theme.Id) == false)
                                {
                                    var view = new UserStartTestDialog
                                    {
                                        DataContext = new UserStartTestDialogViewModel(theme.Название, theme.Время_Прохождения.ToString(), countquestion)
                                    };
                                    var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
                                    if ((bool)result == true)
                                    {
                                        //ОТКРЫТИ НОВОГО ОКНА С ТЕСТАМИ
                                        TestingWindow themeEdit = new TestingWindow() { DataContext = new TestingWindowViewModel(theme.Название, theme.Время_Прохождения.ToString(), theme.Id, LoginName) };
                                        themeEdit.Show();
                                        Application.Current.Windows[0].Hide();
                                    }
                                    Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
                                }
                            }
                        }
                      
                    } break;

                default: break;
            }
        }


        bool CheckTheme(byte themeid)
        {
            using (testEntities db = new testEntities())
            {
                var student = db.Студенты.Where(p => p.Логин == LoginName).SingleOrDefault();
                var allteststudent = db.Студент_Результат.Where(p => p.Студент_Id == student.Id).ToList();
                if (allteststudent.Count == 0) return false;
                for (int i=0; i< allteststudent.Count;i++)
                {
                    var resaultid = allteststudent[i].Результат_Id;
                    var resault = db.Результаты.Where(s => s.Id == resaultid).SingleOrDefault();
                    if (resault.Раздел_Название == IdSelectedChaphterValue && resault.Тема_Название == themeid)
                    {
                        Finish(resault.Оценка);
                        return true;
                    }
                }
                return false;
            }
        }

        private async void Finish(byte Mark)
        {
            var view = new FinishDialog
            {
                DataContext = new FinishDialogViewModel(Mark)
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

     
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }
        #endregion

        public void Show(string selectevaluechoice,string loginname)
        {
            SelecteValueChoice = selectevaluechoice;
            HeaderMainTable = SelecteValueChoice;//Присваивает название загаловка 
            LoginName = loginname;
            CreateTableContent();
            //Console.WriteLine(IsCheckChoice);
            //Console.WriteLine(SelecteValueChoice);
        }

       public void CreateTableContent ()
        {
            _items3.Clear();
            using (testEntities db = new testEntities())
            {
                switch (TypeTable)
                {
                    case "Admin_Editor_TableChaphterEdit"://Готов
                        {
                            var idchaphter = db.Разделы.SingleOrDefault(p => p.Название == SelecteValueChoice);//Получение id выбраного элемента
                            IdSelectedChaphterValue = idchaphter.Id;
                            IEnumerable<Тема> chaphters = db.Темы.Where(p => p.Раздел_Id == IdSelectedChaphterValue).Select(p => new { Название = p.Название, Время_Прохождения = p.Время_Прохождения , Id = p.Id})
          .AsEnumerable()
          .Select(an => new Тема
          {
              Название = an.Название,
              Время_Прохождения = an.Время_Прохождения,
              Id = an.Id
          });
                            foreach (var chaphter in chaphters)
                            {
                                var countquestion = db.Вопросы.Count(p => p.Тема_Id == chaphter.Id);
                                //db.Вопросы.Where(p => p.Тема_Id == chaphter.Id).Load();
                                _items3.Add(new SelectableViewModel
                                {
                                    OneColumnContent = chaphter.Название,
                                    TwoColumnContent = chaphter.Время_Прохождения.ToString(),
                                    ThreeColumnContent = countquestion.ToString()//Количество вопросов
                                });
                            }
                        }
                        break;
                    case "Admin_ListStudent_TableListStudentEdit"://Редактировать
                        {
                            int index = 1;
                            var idgroup = db.Группы.SingleOrDefault(p => p.Название == SelecteValueChoice);//Получение id выбраного элемента
                            IdSelectedChaphterValue = idgroup.Id;
                            IEnumerable<Студент> groups = db.Студенты.Where(p => p.Группа_Id == IdSelectedChaphterValue).Select(p => new {  ФИО = p.ФИО, Id = p.Id })
          .AsEnumerable()
          .Select(an => new Студент
          {
       
              ФИО = an.ФИО,
              Id = an.Id
          });
                            foreach (var group in groups)
                            {
                                var count = db.Студент_Результат.Count(p => p.Студент_Id == group.Id);
                                _items3.Add(new SelectableViewModel
                                {
                                    OneColumnContent = index++.ToString(),
                                    TwoColumnContent = group.ФИО,
                                    ThreeColumnContent = count.ToString()//Количество пройденых тестов в разделе
                                });
                            }
                        }
                        break;
                    case "Admin_ListStudent_TableTestNoEdit":
                        {
                         
                        }
                        break;
                    case "Admin_ListStudent_TablePassedTestNoEdit":
                        {
                          
                        }
                        break;
                    case "User_ListStudent_TableTestNoEdit":
                        {
                            var idchaphter = db.Разделы.SingleOrDefault(p => p.Название == SelecteValueChoice);//Получение id выбраного раздела
                            IdSelectedChaphterValue = idchaphter.Id;
                            IEnumerable<Тема> themes = db.Темы.Where(p => p.Раздел_Id == IdSelectedChaphterValue).Select(p => new { Название = p.Название, Id = p.Id })
          .AsEnumerable()
          .Select(an => new Тема
          {
              Название = an.Название,
              Id = an.Id
          });
                            foreach (var theme in themes)
                            {
                                if (db.Вопросы.Count(t => t.Тема_Id == theme.Id) >= 5) {
                                    _items3.Add(new SelectableViewModel
                                    {
                                        OneColumnContent = theme.Название,
                                    });
                                }
                            }
                        }
                        break;
                    default: break;
                }
            }
       
        }

        #region Property
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { _selectedTabIndex = value;

                OnPropertyChanged();
            }
        }

       public bool IsEnabledPopupBox
        {
            get { return _isEnabledPopupBox; }
            set {
                _isEnabledPopupBox = value;

                OnPropertyChanged();
            }
        }

         public Visibility VisabilityPopupButton
        {
            get { return _visabilityPopupButton; }
            set {
                _visabilityPopupButton = value;

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

        private static ObservableCollection<SelectableViewModel> CreateData()
        {
            return new ObservableCollection<SelectableViewModel>
            {
            };
        }

        public ObservableCollection<SelectableViewModel> Items3 => _items3;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
