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
    /// Логика взаимодействия для AdminLeftPanel.xaml
    /// </summary>
    public partial class AdminLeftPanel : UserControl
    {
        public AdminLeftPanel()
        {
            InitializeComponent();
        }

        private void AdminListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = AdminListView.SelectedIndex;
            switch (index)
            {
                case 0:
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(Window1))
                        {
                            (window as Window1).MenuToggleButton.IsChecked = false;
                            (window as Window1).CenterGrid.Children.Clear();
                            (window as Window1).CenterGrid.Children.Add(new ChoiceGroup("Group"));
                        }
                    }
                    break;
                case 1:
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(Window1))
                        {
                            (window as Window1).MenuToggleButton.IsChecked = false;
                            (window as Window1).CenterGrid.Children.Clear();
                            (window as Window1).CenterGrid.Children.Add(new ChoiceGroup("ChaphterNoEdit"));
                        }
                    }
                    break;
                default:
                    break;
            }

          

        }

       
    }
}
