using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;


namespace TestingProgram
{
    class Val 
    {
        private void Save_CanExecute(object sender)
        {
        Console.WriteLine(IsValid(sender as DependencyObject));
        }

        private bool IsValid(DependencyObject obj)
        {
            return !Validation.GetHasError(obj) && LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().All(IsValid);
        }
    }
}