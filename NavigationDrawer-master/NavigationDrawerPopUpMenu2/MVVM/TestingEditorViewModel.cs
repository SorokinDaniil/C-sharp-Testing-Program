using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace TestingProgram
{
    class TestingEditorViewModel : INotifyPropertyChanged
    {
        private bool _isCheckedTestEditor;
        private int _selectedIndexChangeAnswer;
        private Visibility _codeBoxVisibility;

        //RelayCommand ChangeVisabilityCodeBoxCommand

        public bool IsCheckedTestEditor
        {
            get { return _isCheckedTestEditor; }
            set
            {
                _isCheckedTestEditor = value;

                OnPropertyChanged("IsCheckedTestEditor");
            }
        }

        public Visibility CodeBoxVisibility
        {
            get { return _codeBoxVisibility; }
            set
            {
                _codeBoxVisibility = value;

                OnPropertyChanged();
            }
        }

        public int SelectedIndexChangeAnswer
        {
            get { return _selectedIndexChangeAnswer; }
            set
            {
                _selectedIndexChangeAnswer = value;

                OnPropertyChanged();
            }
        }

        private RelayCommand changeVisabilityCodeBoxCommand;
        public RelayCommand ChangeVisabilityCodeBoxCommand
        {

            get
            {
                return changeVisabilityCodeBoxCommand ??
                    (changeVisabilityCodeBoxCommand = new RelayCommand(obj =>
                    {
                       if ((bool)obj == false)
                        {
                            CodeBoxVisibility = Visibility.Visible;
                        }
                       if((bool)obj == true)
                        {
                            CodeBoxVisibility = Visibility.Collapsed;
                        }
                    }));
            }
        }

private RelayCommand changeTypeAnswerCommand;
        public RelayCommand ChangeTypeAnswerCommand
        {

            get
            {
                return changeTypeAnswerCommand ??
                    (changeTypeAnswerCommand = new RelayCommand(obj =>
                    {
                       if (SelectedIndexChangeAnswer == 0)
                        {
                            (obj as StackPanel).Children.Clear();
                            CheckBox oneanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            CheckBox twoanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            CheckBox threeanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            CheckBox fouranswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            (obj as StackPanel).Children.Add(oneanswer);
                            (obj as StackPanel).Children.Add(twoanswer);
                            (obj as StackPanel).Children.Add(threeanswer);
                            (obj as StackPanel).Children.Add(fouranswer);
                        }
                        if (SelectedIndexChangeAnswer == 1)
                        {
                            (obj as StackPanel).Children.Clear();
                            RadioButton oneanswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            RadioButton twoanswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            RadioButton threeanswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            RadioButton fouranswer = new RadioButton { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
                            (obj as StackPanel).Children.Add(oneanswer);
                            (obj as StackPanel).Children.Add(twoanswer);
                            (obj as StackPanel).Children.Add(threeanswer);
                            (obj as StackPanel).Children.Add(fouranswer);
                        }   
                      
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
