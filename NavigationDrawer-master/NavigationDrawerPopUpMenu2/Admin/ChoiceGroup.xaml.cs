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
using MaterialDesignColors;
using MaterialDesignThemes;

namespace TestingProgram
{
    /// <summary>
    /// Логика взаимодействия для ChoiceGroup.xaml
    /// </summary>
    public partial class ChoiceGroup : UserControl
    {
        string ChoiceTextBlock;
        string ChoiceHint;
        Visibility ChoicePopupBox;

        public ChoiceGroup(string type)
        {
            InitializeComponent();

            switch (type)
            {
                case "Group":
                  ChoiceTextBlock = "Выберите группу";
                  ChoiceHint = "Группа";
                  ChoicePopupBox= Visibility.Visible;break;
                case "Chaphter":
                    ChoiceTextBlock = "Выберите раздел";
                    ChoiceHint = "Раздел";
                    ChoicePopupBox = Visibility.Visible;break;
                case "ChaphterNoEdit":
                    ChoiceTextBlock = "Выберите раздел";
                    ChoiceHint = "Раздел";
                    ChoicePopupBox = Visibility.Hidden;break;
                default: break;
            }
        }
    }
}
