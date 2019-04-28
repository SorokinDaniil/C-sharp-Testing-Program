﻿using System;
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
            DataContext = new TextFieldsViewModel();
            HeadLabelName.Children.Add(new AdminLabelName());
          LeftPanel.Children.Add(new AdminLeftPanel());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Head.Visibility = Visibility.Hidden;
            //StartPanel.Visibility = Visibility.Hidden;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LeftPanel.Children.Add(new UserLeftPanel());
        }
    }
}


