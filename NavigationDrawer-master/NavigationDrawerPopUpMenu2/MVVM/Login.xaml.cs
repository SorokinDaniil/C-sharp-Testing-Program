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
using System.Security.Cryptography;



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
            //string plainData = "Mahesh";
            //Console.WriteLine("Raw data: {0}", plainData);
            //string hashedData = ComputeSha256Hash(plainData);
            //Console.WriteLine("Hash {0}", hashedData);
            //Console.WriteLine(ComputeSha256Hash("Mahesh"));
            //Console.ReadLine();
        }




        //static string ComputeSha256Hash(string rawData)
        //{
        //    // Create a SHA256   
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {
        //        // ComputeHash - returns byte array  
        //        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        //        // Convert byte array to a string   
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            builder.Append(bytes[i].ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}






        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            SignIn_Login.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            SignIn_Password.GetBindingExpression(PasswordHelper.PasswordProperty).UpdateSource();
            if (!IsValid(SignIn_Login) && !IsValid(SignIn_Password))
                ((LoginViewModel)DataContext).SignIn(this);
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp_Login.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            SignUp_Password.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            SignUp_FullName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            SignUp_Group.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
            if (!IsValid(SignUp_Login) && !IsValid(SignUp_Password) && !IsValid(SignUp_FullName) && !IsValid(SignUp_Group))
                ((LoginViewModel)DataContext).SignUp();
            else
                MessageBox.Show("No");


            //var g = SignUp_Group.SelectedItem as Группа;
            //MessageBox.Show(g.Id.ToString());
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
