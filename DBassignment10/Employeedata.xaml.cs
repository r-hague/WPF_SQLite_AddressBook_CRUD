using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBassignment10
{
    /// <summary>
    /// Interaction logic for Employeedata.xaml
    /// </summary>
    public partial class Employeedata : Window
    {
        ViewModel vm;
        public Employeedata(ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            this.vm = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            vm.Save();
          this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region input controllers
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //name
            Regex check = new Regex("([0-9])+$");
            e.Handled = check.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            //position
            Regex check = new Regex("([0-9])+$");
            e.Handled = check.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            //position
            Regex check = new Regex("([0-9])+$");
            e.Handled = check.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_3(object sender, TextCompositionEventArgs e)
        {
            //hourlyrate
            Regex check = new Regex("([^0-9.])+$");
            e.Handled = check.IsMatch(e.Text);
        }
        #endregion
    }
}
