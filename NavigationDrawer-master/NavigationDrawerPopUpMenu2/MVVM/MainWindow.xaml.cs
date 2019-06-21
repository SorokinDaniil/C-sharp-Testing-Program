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
    public partial class MainWindow : Window
    {
        public MainWindow(string typeAccount,string logincurrentuser)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(typeAccount,logincurrentuser);
            if (typeAccount == "Admin")
            {
   HeadLabelName.Children.Add(new AdminLabelName());
            TextOneItem.Text = "Журнал";
            }
            else
            if (typeAccount == "User")
            {
   HeadLabelName.Children.Add(new UserLabelName(logincurrentuser));
            TextOneItem.Text = "Тесты";
            TwoItem.Visibility = Visibility.Collapsed;
            }
         
        }










    }
}


