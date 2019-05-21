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
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace TestingProgram
{

    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
          InitializeComponent();

            DataContext = new MainWindowViewModel();
            //DataContext = new ListsAndGridsViewModel();
            HeadLabelName.Children.Add(new AdminLabelName());
            Panel.Children.Add(new AdminLeftPanel());

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
        }

      

    }
}


