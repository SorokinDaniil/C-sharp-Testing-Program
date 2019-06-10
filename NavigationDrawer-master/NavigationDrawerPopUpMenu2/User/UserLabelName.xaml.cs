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

namespace TestingProgram
{
    /// <summary>
    /// Логика взаимодействия для StudentControl.xaml
    /// </summary>
    public partial class UserLabelName : UserControl
    {
        public UserLabelName(string logincurrentuser)
        {
            InitializeComponent();
            using (testEntities db = new testEntities())
            {
                Студент студент = db.Студенты.Where(p => p.Логин == logincurrentuser).FirstOrDefault();
                TextLabelUserPanel.Text = студент.ФИО;
            }
            TextBoxUserPanel.Text = TextLabelUserPanel.Text;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TextBoxUserPanel.Width = TextLabelUserPanel.Width;
            TextBoxUserPanel.Height = TextLabelUserPanel.Height;
        }
    }
}
