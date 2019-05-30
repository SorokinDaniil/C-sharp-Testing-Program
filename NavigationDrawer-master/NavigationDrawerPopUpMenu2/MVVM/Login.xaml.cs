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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            //using (testEntities context = new testEntities())
            //{
            //    Тема тема = new Тема { Название = "ООП" };
            //    context.Тема.Add(тема);
            //    context.SaveChanges();

            //}
            
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            //if(IsValid(SignIn_Login) && IsValid(SignIn_Password))

            //((LoginViewModel)DataContext).SignIn();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            //if (IsValid(SignUp_Login) && IsValid(SignUp_Password) && IsValid(SignUp_FullName) && IsValid(SignUp_Group))
            //    ((LoginViewModel)DataContext).SignUp();
        }

        bool IsValid (DependencyObject d)
        {
            return Validation.GetHasError(d);
        }

        //private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        //{

        //}




        //private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        //{

        //    if (e.Action == ValidationRe.Added)
        //    {
        //        Console.WriteLine("YES");
        //    }
        //}
    }
}
