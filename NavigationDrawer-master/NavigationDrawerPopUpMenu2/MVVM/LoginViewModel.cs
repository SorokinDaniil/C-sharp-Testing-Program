using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;


namespace TestingProgram
{
    class LoginViewModel : ViewModelBase
    {
        public ICommand ClickAdd
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Console.WriteLine("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                });

            }
        }
       

    }
}
