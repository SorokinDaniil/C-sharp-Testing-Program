using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Data;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Controls;

namespace TestingProgram.TESTFOLDER
{

    public class ProtocolSettingsLayout :ViewModelBase
    {
        public static readonly DependencyProperty MVVMHasErrorProperty = DependencyProperty.RegisterAttached("MVVMHasError",
                                                                        typeof(bool),
                                                                        typeof(ProtocolSettingsLayout),
                                                                        new FrameworkPropertyMetadata(false,
                                                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                                                      null,
                                                                                                      CoerceMVVMHasError));

        public static bool GetMVVMHasError(DependencyObject d)
        {
           
            return (bool)d.GetValue(MVVMHasErrorProperty); 
        }

        public static void SetMVVMHasError(DependencyObject d, bool value)
        {
            d.SetValue(MVVMHasErrorProperty, value);
        }

        private static object CoerceMVVMHasError(DependencyObject d, Object baseValue)
        {
            bool ret = (bool)baseValue;

            if (BindingOperations.IsDataBound(d, MVVMHasErrorProperty))
            {
                if (GetHasErrorDescriptor(d) == null)
                {
                    DependencyPropertyDescriptor desc = DependencyPropertyDescriptor.FromProperty(ValidationTest.HasContentProperty, d.GetType());
                    desc.AddValueChanged(d, OnHasErrorChanged);
                    SetHasErrorDescriptor(d, desc);
                    ret = System.Windows.Controls.Validation.GetHasError(d);
                }
            }
            else
            {
                if (GetHasErrorDescriptor(d) != null)
                {
                    DependencyPropertyDescriptor desc = GetHasErrorDescriptor(d);
                    desc.RemoveValueChanged(d, OnHasErrorChanged);
                    SetHasErrorDescriptor(d, null);
                }
            }

            return ret;
        }

        private static readonly DependencyProperty HasErrorDescriptorProperty = DependencyProperty.RegisterAttached("HasErrorDescriptor",
                                                                                typeof(DependencyPropertyDescriptor),
                                                                                typeof(ProtocolSettingsLayout));

        private static DependencyPropertyDescriptor GetHasErrorDescriptor(DependencyObject d)
        {
            var ret = d.GetValue(HasErrorDescriptorProperty);
            return ret as DependencyPropertyDescriptor;
        }

        private static void OnHasErrorChanged(object sender, EventArgs e)
        {
            DependencyObject d = sender as DependencyObject;

            if (d != null)
            {
                d.SetValue(MVVMHasErrorProperty, d.GetValue(ValidationTest.HasContentProperty));
            }
        }

        private static void SetHasErrorDescriptor(DependencyObject d, DependencyPropertyDescriptor value)
        {
            var ret = d.GetValue(HasErrorDescriptorProperty);
            d.SetValue(HasErrorDescriptorProperty, value);
        }

        public ICommand Click
        {
            get
            {
                return new DelegateCommand(() =>
                {
                   
                });

            }
        }
    }
}
