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

namespace TestingProgram
{
    /// <summary>
    /// Логика взаимодействия для TestingEditor.xaml
    /// </summary>
    public partial class TestingEditor : Window
    {
        public TestingEditor()
        {
            InitializeComponent();
            DataContext = new TestingEditorViewModel();
            //CheckBox oneanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29} , MinHeight = 20, IsChecked = false , Margin = new Thickness(20, 0, 20, 6) };
            //CheckBox twoanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
            //CheckBox threeanswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
            //CheckBox fouranswer = new CheckBox { Content = new TextBox { FontSize = 15, MaxLength = 204, Width = 1017, Height = 29 }, MinHeight = 20, IsChecked = false, Margin = new Thickness(20, 0, 20, 6) };
            //AnswerBlock.Children.Add(oneanswer);
            //AnswerBlock.Children.Add(twoanswer);
            //AnswerBlock.Children.Add(threeanswer);
            //AnswerBlock.Children.Add(fouranswer);
        }

     
    }
}
